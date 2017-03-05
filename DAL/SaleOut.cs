using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    /// <summary>
    /// 库存管理.销售出库
    /// </summary>
    public class SaleOut
    {
        /// <summary>
        /// 销售出库:保存单据
        /// </summary>
        /// <param name="user">操作员</param>
        /// <param name="rdRecord"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-26</remarks>
        public bool Save(User user, RdRecord rdRecord, out string errMsg)
        {
            //log message
            string logMsg = string.Format("操作员：{0},业务号：{1} ", user.UserName, rdRecord.cBusCode);
            errMsg = string.Empty;
            int result;
            bool flag = false;
            //创建数据对象
            SqlConnection conn = new SqlConnection(user.ConnectionString);
            SqlCommand comm = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            //打开连接
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return flag;
            }
            //开始事务
            SqlTransaction tran = conn.BeginTransaction();
            comm.Connection = conn;
            comm.Transaction = tran;

            string strSql = string.Empty;
            try
            {
                //操作时间
                DateTime now = rdRecord.dDate;
                DataTable dt = new DataTable();

                //log
                if (Common.flag)
                    Common.log.Info(string.Format("开始生成销售出库单！--{0}", logMsg));
                //单据类型编码及名称
                string cVouchType = "";//VouchType字典表
                string cVouchName = "销售出库单";
                comm.CommandText = string.Format("select cvouchtype from vouchtype where cvouchname='{0}'", cVouchName);
                cVouchType = comm.ExecuteScalar().ToString();

                //单据类型编码、模板编号
                string cardNumber;
                int vt_id;
                strSql = string.Format("select def_id,cardnumber from Vouchers where ccardname='{0}'", cVouchName);
                comm.CommandText = strSql;
                adp.SelectCommand = comm;
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    tran.Rollback();
                    errMsg = "单据类型编码、模板编号查询失败";
                    return flag;
                }
                cardNumber = Cast.ToString(dt.Rows[0]["cardnumber"]);
                vt_id = Cast.ToInteger(dt.Rows[0]["def_id"]);
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("单据类型编码：{0},模板号:{1}！", cardNumber, vt_id));

                //RdRecord,RdRecords 数据表ID
                int id, autoid;
                comm.CommandText = string.Format("select ifatherid,ichildid from UFSystem..UA_Identity where cVouchType ='rd' and cAcc_Id='{0}'", user.AccID);
                adp.SelectCommand = comm;
                dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    tran.Rollback();
                    errMsg = "查询UA_Identity数据失败！";
                    return flag;
                }
                id = Cast.ToInteger(dt.Rows[0]["ifatherid"]);
                autoid = Cast.ToInteger(dt.Rows[0]["ichildid"]);

                //插入主表
                id++;//主表ID加1
                strSql = string.Format(@"INSERT INTO rdrecord (id,brdflag,cvouchtype,cbustype,csource,cbuscode,cwhcode,ddate,ccode,crdcode,cdepcode,cpersoncode,cstcode,ccuscode,cdlcode,cmaker,cdefine1,cdefine2,vt_id,iarriveid,iswfcontrolled,dnmaketime,dnverifytime,cHandler ,dVeriDate,cBillCode,isalebillid )
VALUES(@id,@brdflag,@cvouchtype,@cbustype,@csource,@cbuscode,@cwhcode,@ddate,@ccode,@crdcode,@cdepcode,@cpersoncode,@cstcode,@ccuscode,@cdlcode,@cmaker,@cdefine1,@cdefine2,@vt_id,@iarriveid,@iswfcontrolled,@dnmaketime,@dnverifytime,@cHandler ,@dVeriDate,@cBillCode,@isalebillid );");
                SqlParameter[] parms = 
                {
                    new SqlParameter("@id",SqlDbType.Int),//收发记录主表标识
                    new SqlParameter("@brdflag",SqlDbType.Bit),//收发标志 
                    new SqlParameter("@cvouchtype",SqlDbType.VarChar,2),//单据类型编码
                    new SqlParameter("@cbustype",SqlDbType.NVarChar,12),//业务类型 
                    new SqlParameter("@csource",SqlDbType.NVarChar,50),//单据来源 
                    new SqlParameter("@cbuscode",SqlDbType.VarChar,30),//对应业务单号 
                    new SqlParameter("@cwhcode",SqlDbType.VarChar,10),//仓库编码 
                    new SqlParameter("@ddate",SqlDbType.DateTime),//单据日期 
                    new SqlParameter("@ccode",SqlDbType.VarChar,30),//收发单据号 
                    new SqlParameter("@crdcode",SqlDbType.VarChar,5),//收发类别编码 
                    new SqlParameter("@cdepcode",SqlDbType.VarChar,12),//部门编码 
                    new SqlParameter("@cpersoncode",SqlDbType.VarChar,20),//业务员编码 
                    new SqlParameter("@cstcode",SqlDbType.VarChar,2),//销售类型编码 
                    new SqlParameter("@ccuscode",SqlDbType.VarChar,20),//客户编码 
                    new SqlParameter("@cdlcode",SqlDbType.Int),//发货退货单主表标识 
                    new SqlParameter("@cmaker",SqlDbType.NVarChar,20),//制单人 
                    new SqlParameter("@cdefine1",SqlDbType.NVarChar,20),//自定义项1
                    new SqlParameter("@cdefine2",SqlDbType.NVarChar,20),//自定义项2
                    new SqlParameter("@vt_id",SqlDbType.Int),//单据模版号
                    new SqlParameter("@iarriveid",SqlDbType.VarChar,30),//发货退货单号 
                    new SqlParameter("@iswfcontrolled",SqlDbType.Int),//是否工作流控制 
                    new SqlParameter("@dnmaketime",SqlDbType.DateTime),//制单时间 
                    new SqlParameter("@dnverifytime",SqlDbType.DateTime),//审核时间
                    new SqlParameter("@cHandler",SqlDbType.NVarChar,20),//审核人
                    new SqlParameter("@dVeriDate",SqlDbType.DateTime),//审核日期
                    new SqlParameter("@cBillCode",SqlDbType.Int),//发票主表标识
                    new SqlParameter("@isalebillid",SqlDbType.VarChar,30)//发票号
                };
                parms[0].Value = id;
                parms[1].Value = 0;//出
                parms[2].Value = cVouchType;
                parms[3].Value = rdRecord.cBusType;
                parms[4].Value = rdRecord.cSource;
                parms[5].Value = rdRecord.cBusCode;
                parms[6].Value = rdRecord.List[0].cWhCode;
                parms[7].Value = Convert.ToDateTime(now.ToShortDateString());
                parms[8].Value = id.ToString();//后面作修改 //rdRecord.cCode;
                parms[9].Value = rdRecord.cRdCode;
                parms[10].Value = rdRecord.cDepCode;
                parms[11].Value = string.IsNullOrEmpty(rdRecord.cPersonCode) ? DBNull.Value : (object)rdRecord.cPersonCode;
                parms[12].Value = rdRecord.cSTCode;
                parms[13].Value = rdRecord.cCusCode;
                parms[14].Value = rdRecord.DLID;
                parms[15].Value = user.UserName;
                parms[16].Value = rdRecord.cDefine1;
                parms[17].Value = rdRecord.cDefine2;
                parms[18].Value = vt_id;
                parms[19].Value = rdRecord.iarriveid;
                parms[20].Value = rdRecord.iswfcontrolled;
                parms[21].Value = now;
                parms[22].Value = now;
                parms[23].Value = user.UserName;
                parms[24].Value = Convert.ToDateTime(now.ToShortDateString());
                parms[25].Value = rdRecord.cBillCode == 0 ? DBNull.Value : (object)rdRecord.cBillCode;
                parms[26].Value = string.IsNullOrEmpty(rdRecord.isalebillid) ? DBNull.Value : (object)rdRecord.isalebillid;

                comm.CommandText = strSql;
                comm.Parameters.AddRange(parms);//添加参数
                if (comm.ExecuteNonQuery() < 1)
                {
                    tran.Rollback();
                    errMsg = "主表数据插入失败！";
                    return flag;
                }
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("销售出库单RdRecord主表插入完成,主表ID：{0}！", id));
                //清空命令中的参数
                comm.Parameters.Clear();
                strSql = "if exists (select 1 from tempdb.dbo.sysobjects where id = object_id(N'tempdb..#Ufida_WBBuffers') and type='U') drop table #Ufida_WBBuffers;";
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();

                strSql = "select a.id,autoid ,convert(decimal(30,2),iquantity) as iquantity,convert(decimal(30,2),inum) as iNum, a.Cinvcode,Corufts ,idlsid,iCheckIds, convert(smallint,0) as iOperate into #Ufida_WBBuffers from rdrecords a where 1=0";
                comm.CommandText = strSql;
                int i = comm.ExecuteNonQuery();

                strSql = string.Format("update rdrecords set corufts ='' where id ={0}", id);
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();


                //循环插入子表
                foreach (RdRecords rds in rdRecord.List)
                {
                    autoid++;
                    strSql = string.Format(@"Insert Into rdrecords(autoid,id,cinvcode,iquantity,iunitcost,iprice,cbatch,isoutquantity,dvdate,cposition,idlsid,isbsid,iensid,cbarcode,inquantity,dmadedate,imassdate,icheckids,cbvencode,cinvouchcode,bgsp,cgspstate,cmassunit,irefundinspectflag,iorderdid,bchecked,iordertype,bcosting,iinvexchrate,cbdlcode,iordercode,iexpiratdatecalcu,cexpirationdate,dexpirationdate,isotype,iorderseq)
VALUES(@autoid,@id,@cinvcode,@iquantity,@iunitcost,@iprice,@cbatch,@isoutquantity,@dvdate,@cposition,@idlsid,@isbsid,@iensid,@cbarcode,@inquantity,@dmadedate,@imassdate,@icheckids,@cbvencode,@cinvouchcode,@bgsp,@cgspstate,@cmassunit,@irefundinspectflag,@iorderdid,@bchecked,@iordertype,@bcosting,@iinvexchrate,@cbdlcode,@iordercode,@iexpiratdatecalcu,@cexpirationdate,@dexpirationdate,@isotype,@iorderseq);");
                    parms = new SqlParameter[]
                    {
                        new SqlParameter("@autoid",SqlDbType.Int),
                        new SqlParameter("@id",SqlDbType.Int),
                        new SqlParameter("@cinvcode",SqlDbType.VarChar,20),//存货编码 
                        new SqlParameter("@iquantity",SqlDbType.Float),//数量
                        new SqlParameter("@iunitcost",SqlDbType.Float),//单价 
                        new SqlParameter("@iprice",SqlDbType.Money),//金额 
                        new SqlParameter("@cbatch",SqlDbType.VarChar,30),//批号 
                        new SqlParameter("@isoutquantity",SqlDbType.Float),//累计出库数量 
                        new SqlParameter("@dvdate",SqlDbType.DateTime),//失效日期 
                        new SqlParameter("@cposition",SqlDbType.VarChar,20),//货位编码 
                        new SqlParameter("@idlsid",SqlDbType.Int),//发货退货单子表标识 
                        new SqlParameter("@isbsid",SqlDbType.Int),//发票子表标识 
                        new SqlParameter("@iensid",SqlDbType.Int),//委托代销发货单子表标识
                        new SqlParameter("@cbarcode",SqlDbType.VarChar,30),//对应条形码编码 
                        new SqlParameter("@inquantity",SqlDbType.Float),//应收应发数量 
                        new SqlParameter("@dmadedate",SqlDbType.DateTime),//生产日期 
                        new SqlParameter("@imassdate",SqlDbType.Int),//保质期天数 
                        new SqlParameter("@icheckids",SqlDbType.Int),//检验单子表标识
                        new SqlParameter("@cbvencode",SqlDbType.VarChar,20),//供应商编码
                        new SqlParameter("@cinvouchcode",SqlDbType.VarChar,30),//对应入库单号 
                        new SqlParameter("@bgsp",SqlDbType.Bit),//是否质检 
                        new SqlParameter("@cgspstate",SqlDbType.VarChar,20),//质检状态 
                        new SqlParameter("@cmassunit",SqlDbType.SmallInt),//保质期单位 
                        new SqlParameter("@irefundinspectflag",SqlDbType.Int),//是否已经生成退货报检单1：生成,0:没有生成
                        new SqlParameter("@iorderdid",SqlDbType.Int),//订单子表id
                        new SqlParameter("@bchecked",SqlDbType.Bit),//销售出库单是否报检 
                        new SqlParameter("@iordertype",SqlDbType.Int),//订单类型 
                        new SqlParameter("@bcosting",SqlDbType.Bit),//单据是否核算
                        new SqlParameter("@iinvexchrate",SqlDbType.Decimal),//换算率 
                        new SqlParameter("@cbdlcode",SqlDbType.VarChar,30),//发货单号 
                        new SqlParameter("@iordercode",SqlDbType.VarChar,30),//订单号 
                        new SqlParameter("@iexpiratdatecalcu",SqlDbType.SmallInt),//有效期推算方式 
                        new SqlParameter("@cexpirationdate",SqlDbType.VarChar,10),//有效期至 
                        new SqlParameter("@dexpirationdate",SqlDbType.DateTime),//有效期计算项 
                        new SqlParameter("@isotype",SqlDbType.Int),//订单类型 
                        new SqlParameter("@iorderseq",SqlDbType.Int)//销售订单行号 
                    };

                    //给参数赋值
                    parms[0].Value = autoid;
                    parms[1].Value = id;
                    parms[2].Value = rds.cInvCode;
                    parms[3].Value = rds.iScanQuantity;
                    parms[4].Value = DBNull.Value; //rds.iUnitCost;目前没有使用
                    parms[5].Value = DBNull.Value; //rds.iPrice;目前没有使用
                    parms[6].Value = rds.cBatch;
                    parms[7].Value = rds.iSOutQuantity;
                    parms[8].Value = rds.dVDate == DateTime.MinValue ? DBNull.Value : (object)rds.dVDate;
                    parms[9].Value = DBNull.Value;//rds.cPosition;
                    parms[10].Value = rds.iDLsID;
                    parms[11].Value = rds.iSBsID;
                    parms[12].Value = rds.iEnsID;
                    parms[13].Value = DBNull.Value;//rds.cBarCode;
                    parms[14].Value = rds.iNQuantity;
                    parms[15].Value = rds.dMadeDate == DateTime.MinValue ? DBNull.Value : (object)rds.dMadeDate;
                    parms[16].Value = rds.iMassDate;
                    parms[17].Value = DBNull.Value; //rds.iCheckIds;插入0导致客户U8无法删除单据
                    parms[18].Value = DBNull.Value; //rds.cBVencode;
                    parms[19].Value = DBNull.Value;
                    parms[20].Value = rds.bGsp;
                    parms[21].Value = DBNull.Value;//rds.cGspState;
                    parms[22].Value = rds.cMassUnit;
                    parms[23].Value = DBNull.Value;
                    parms[24].Value = rds.iorderdid;
                    parms[25].Value = DBNull.Value;
                    parms[26].Value = rds.iordertype;//没有查到数据
                    parms[27].Value = rds.bCosting;
                    parms[28].Value = rds.iInvExchRate;
                    parms[29].Value = rds.cbdlcode;
                    parms[30].Value = rds.iordercode;
                    parms[31].Value = rds.iExpiratDateCalcu;
                    parms[32].Value = string.IsNullOrEmpty(rds.cExpirationdate) ? DBNull.Value : (object)rds.cExpirationdate;
                    parms[33].Value = rds.dExpirationdate == DateTime.MinValue ? DBNull.Value : (object)rds.dExpirationdate;
                    parms[34].Value = rds.iSoType;
                    parms[35].Value = rds.iorderseq;

                    comm.CommandText = strSql;
                    comm.Parameters.Clear();
                    comm.Parameters.AddRange(parms);
                    if (comm.ExecuteNonQuery() < 1)
                    {
                        tran.Rollback();
                        errMsg = "子表数据插入失败";
                        return flag;
                    }

                    strSql = string.Empty;
                    //插入标签流水号关系表
                    foreach (string number in rds.SerialList)
                    {
                        strSql += string.Format("INSERT INTO UFSystem..RdRecordSN( RDID, RDSID, cInvCode, cBatch, Number, AddDate )VALUES  ({0},{1},'{2}','{3}',{4},'{5}');", id, autoid, rds.cInvCode, rds.cBatch, number, now);
                    }
                    comm.CommandText = strSql;
                    if (comm.ExecuteNonQuery() < 1)
                    {
                        tran.Rollback();
                        errMsg = "标签流水号插入失败！";
                        return flag;
                    }
                }
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("销售出库单RdRecords子表插入完成,共{0}条数据！", rdRecord.List.Count));

                //清空命令中的参数
                comm.Parameters.Clear();

                #region 临时表处理

                strSql = string.Format("Insert Into #Ufida_WBBuffers select a.id,autoid ,1 * convert (decimal(30,2),iquantity),1 * convert(decimal(30,2),inum), a.Cinvcode, Corufts  as Corufts, idlsid, iCheckIds,2 as iOperate   from rdrecords a  where id={0}", id);
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();
                strSql = @"if exists (select 1 from tempdb.dbo.sysobjects where id = object_id(N'tempdb..#Ufida_WBBuffers_ST') and type='U') drop table #Ufida_WBBuffers_ST;
if exists (select 1 from tempdb.dbo.sysobjects where id = object_id(N'tempdb..#Ufida_WBBuffers_Target') and type='U') drop table #Ufida_WBBuffers_Target;";
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();

                strSql = @"select max(id) as id,autoid,Sum(iquantity) as iquantity,sum(inum) as inum,max(cinvcode) as cinvcode ,Max(corufts) as corufts,  max(idlsid) as idlsid, sum(iOperate) as iOperate  into  #Ufida_WBBuffers_ST from #Ufida_WBBuffers group by autoid having (Sum(iquantity)<>0 or Sum(inum)<>0 );
update  #Ufida_WBBuffers_ST set corufts='' where iOperate<>2";
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();

                strSql = "select idlsid as idid,sum(iquantity) as iquantity,sum(inum) as inum,max(cinvcode) as cinvcode,Max(corufts) as corufts  into #Ufida_WBBuffers_Target from #Ufida_WBBuffers_ST group by idlsid ;";
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();

                strSql = "update dispatchlists with (UPDLOCK) set fOutQuantity=cast(isnull(fOutQuantity,0)+isnull(#Ufida_WBBuffers_Target.iquantity,0)  as decimal(30,2)), fOutNum=cast(isnull(fOutNum,0)+isnull(#Ufida_WBBuffers_Target.inum,0) as decimal(30,2)) from dispatchlists inner join #Ufida_WBBuffers_Target on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid where bsettleall=0";
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                if (result < 0)
                {
                    tran.Rollback();
                    errMsg = "dispatchlists fOutQuantity update error!";
                    return flag;
                }
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("临时表处理，修改发货单子表的待发货数量，影响行数：{0}！", result));

                /*
                 * 2013-11-14
                 * 此操作用来验证发货数量是否超过待发货数量
                 * (用于解决U8与终端同时操作同一个单据时出现两个相同的销售出库单)
                 * */
                strSql = @"select * from 
(
	select 
		cast((case when isnull(a.iquantity,0) >0 then 1 when isnull(a.iquantity,0)<0 then -1 else 0 end )*(isnull(a.iquantity,0)-isnull(foutquantity,0)) as decimal(30,2)) as iquantity, 
		cast((case when isnull(a.inum,0)>0 then 1 when isnull(a.inum,0)<0 then -1 else 0 end)*(isnull(a.inum,0)-isnull(foutnum,0)) as decimal(30,2)) as inum,
		inventory.igrouptype ,inventory.cinvcode ,inventory.cinvname  
		from 
			(
				Select 
					case when isnull(bqaneedcheck ,0)=0 or isnull(iquantity,0) <0 then iquantity else iqaquantity end as iquantity,
					case when isnull(bqaneedcheck ,0)=0 or isnull(iquantity,0) <0 then inum else iqanum end as inum,
					foutquantity,foutnum,idlsid ,cinvcode  from dispatchlists
			) as a 
	inner join inventory on a.cinvcode=inventory.cinvcode 
	inner join #Ufida_WBBuffers_Target on a.idlsid=#Ufida_WBBuffers_Target.idid
)
temp where temp.iquantity<0 or temp.inum<0";
                comm.CommandText = strSql;
                adp.SelectCommand = comm;
                dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)//如果有数量说明，这些存货的数量超过了待发货数量
                {
                    errMsg = "此单据可能已生成出库单！\r\n";
                    foreach (DataRow row in dt.Rows)
                    {
                        errMsg += string.Format("名称：[{0}] 编号：[{1}] 此存货的发货数量超过了待发货数量！\r\n", row["cinvname"], row["cinvcode"]);
                    }
                    if (Common.flag)
                        Common.log.Error(errMsg);
                    tran.Rollback();//开始回滚
                    return flag;
                }

                strSql = "if exists (select 1 from tempdb.dbo.sysobjects where id = object_id(N'tempdb..#tmpdlid') and type='U') drop table #tmpdlid;";
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();
                strSql = "select distinct dlid into #tmpdlid from dispatchlists inner join #Ufida_WBBuffers_Target on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid";
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();

                strSql = "update dispatchlist  set cSaleOut=N'' from dispatchlist inner join #tmpdlid b on dispatchlist.dlid=b.dlid where isnull(cSaleOut,'')='' or isnull(cSaleOut,'')='ST' ";
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("临时表处理，修改发货单主表的cSaleOut为‘’，影响行数：{0}！", result));

                strSql = "update dispatchlist  set cSaleOut=N'ST' from dispatchlist  inner join #tmpdlid b on dispatchlist.dlid=b.dlid inner join  dispatchlists c on c.dlid=dispatchlist.dlid inner join rdrecords on c.idlsid=rdrecords.idlsid  where isnull(cSaleOut,'')='' or isnull(cSaleOut,'')='ST' ";
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("临时表处理，修改发货单主表的cSaleOut为‘ST’，影响行数：{0}！", result));

                strSql = @"
drop table #Ufida_WBBuffers;
drop table #Ufida_WBBuffers_ST;
drop table #Ufida_WBBuffers_Target;
drop table #tmpdlid;";
                comm.CommandText = strSql;
                comm.ExecuteNonQuery();

                #endregion

                //保存
                //strSql = string.Format("exec ST_SaveForStock N'{0}',N'{1}',1 ,0 ,1", cVouchType, id);
                //comm.CommandText = strSql;
                //result = comm.ExecuteNonQuery();

                //审核
                strSql = string.Format("exec ST_VerForStock N'{0}',N'{1}',0,1,1", cVouchType, id);
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("审核销售出库单，影响行数：{0}！", result));


                strSql = "select @@spid";
                comm.CommandText = strSql;
                string spid = comm.ExecuteScalar().ToString();
                if (string.IsNullOrEmpty(spid))
                {
                    tran.Rollback();
                    return flag;
                }

                //strSql = "select transactionid,* from SCM_EntryLedgerBuffer";
                //dt = DBHelperSQL.QueryTable(user.ConnectionString, strSql);

                strSql = string.Format(@"insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)
 select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) 
 where a.transactionid=N'spid_{0}' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 
 and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 
 and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )", spid);
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                strSql = string.Format("exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,1,1,0,0,1,1,1,0,0,0,1,'spid_{0}'", spid);
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("执行Usp_SCM_CommitGeneralLedgerWithCheck存储过程，影响行数：{0}！", result));


                //单据号处理
                //voucherhistory
                strSql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent is NULL", cardNumber);
                comm.CommandText = strSql;
                object obj = comm.ExecuteScalar();
                int cNumber;
                if (obj == null || obj == DBNull.Value)
                {
                    cNumber = 1;
                    strSql = string.Format("INSERT INTO dbo.VoucherHistory( CardNumber ,iRdFlagSeed ,cContent ,cContentRule ,cSeed ,cNumber ,bEmpty) VALUES({0},NULL,NULL,NULL,NULL,{1},0);", cardNumber, cNumber);
                }
                else
                {
                    cNumber = Cast.ToInteger(obj) + 1;
                    strSql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='{1}' and cContent is NULL", cNumber, cardNumber);
                }
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                if (result < 1)
                {
                    tran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return flag;
                }
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("处理VoucherHistory表的单据号，影响行数：{0}！", result));


                //RdRecord
                string cCode = string.Format("{0}{1}", now.ToString("yyyy"), cNumber.ToString().PadLeft(8, '0'));
                strSql = string.Format("Select cCode  from RdRecord with (nolock) Where cCode=N'{0}' and id<>{1} AND cVouchType=N'{2}'", cCode, id, cVouchType);
                comm.CommandText = strSql;
                if (comm.ExecuteScalar() != null)
                {
                    tran.Rollback();
                    errMsg = "单据号重复！";
                    return flag;
                }
                strSql = string.Format("Update RdRecord Set cCode = N'{0}' Where Id = {1}", cCode, id);
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                if (result < 1)
                {
                    tran.Rollback();
                    errMsg = "RdRecord表更新出错";
                    return flag;
                }
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("修改销售出库单主表的cCode字段为{0}，影响行数：{1}！", cCode, result));


                //UA_identity
                strSql = string.Format("UPDATE UFSystem..UA_Identity SET iFatherId={0},iChildId={1} WHERE cVouchType='rd' AND cAcc_Id='{2}'", id, autoid, user.AccID);
                comm.CommandText = strSql;
                result = comm.ExecuteNonQuery();
                if (result < 0)
                {
                    tran.Rollback();
                    errMsg = "UA_Identity更新出错";
                    return flag;
                }
                tran.Commit();
                flag = true;
                //log
                if (Common.flag)
                    Common.log.Info(string.Format("销售出库单生成完成！"));

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                tran.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
    }
}
