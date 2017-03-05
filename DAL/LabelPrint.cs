using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace DAL
{
    /// <summary>
    /// 标签打印
    /// </summary>
    public class LabelPrint
    {
        /// <summary>
        /// 标签打印数据库连接测试
        /// </summary>
        /// <param name="accid">账套</param>
        /// <param name="year">年度</param>
        /// <param name="info">数据库信息</param>
        /// <param name="connectionString">如何连接成功，返回连接字符串</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-14</remarks>
        public bool ConnectionTest(string accid, string year, DBInfo info, out string connectionString, out string errMsg)
        {
            errMsg = string.Empty;
            connectionString = string.Empty;
            bool flag = false;
            if (info == null)
            {
                errMsg = "数据库配置信息错误！";
                return flag;
            }

            //组装连接字符串
            connectionString = string.Format(@"user id={0};password={1};data source={2};persist security info=True;initial catalog=UFDATA_{3}_{4};Connection Timeout=30", info.SqlUser, info.SqlPassword, info.DBServer, accid, year);
            string strSql = "select count(1) from inventory where 1=2;";
            try
            {
                object obj = DBHelperSQL.ExecuteScalar(connectionString, strSql);
                if (obj == null && obj == DBNull.Value)
                {
                    errMsg = "测试连接错误！";
                    if (Common.flag)
                        Common.log.Error(errMsg + connectionString);
                    connectionString = string.Empty;
                    return flag;
                }
                if (obj.ToString().Equals("0"))
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        /// <summary>
        /// 根据存货名称或编码查询存货信息
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="whList">仓库列表</param>
        /// <param name="cInvName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable SearchInventory(string connectionString, List<Warehouse> whList, string cInvCode, string cInvName, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;

            string condition = string.Empty;//条件
            //判断仓库列表是否为空
            if (whList == null || whList.Count == 0)
            {
                errMsg = "没有仓库权限";
                return dt;
            }
            //封装仓库条件
            condition = string.Format(" AND cWhCode IN(");
            foreach (Warehouse wh in whList)
            {
                condition += string.Format("'{0}',", wh.cWhCode);
            }
            condition = condition.Substring(0, condition.Length - 1);
            condition += ")";

            //string strSql = "SELECT cInvCode,cInvName,cInvStd,cMassUnit,iMassDate FROM dbo.Inventory WHERE 1=1";
            string strSql = string.Format(@"SELECT CS.cInvCode,Inventory.cInvName ,Inventory.cInvStd,CS.iQuantity ,CS.cBatch,CS.dvdate,CS.dmdate,CS.imassdate,CASE Inventory.cMassUnit WHEN 3 THEN '天' WHEN 2 THEN '月' WHEN 1 THEN '年' ELSE '' END AS cMassUnit
FROM 
(SELECT * FROM  CurrentStock WHERE 1=1 {0})CS 
INNER JOIN Inventory ON CS.cInvCode = Inventory.cInvCode   
WHERE iQuantity>0 ", condition);
            //判断编码或者名称是否为空
            if (!string.IsNullOrEmpty(cInvCode))
            {
                strSql += string.Format(" AND Inventory.cInvCode LIKE '%{0}%'", cInvCode);
            }
            if (!string.IsNullOrEmpty(cInvName))
            {
                strSql += string.Format("AND Inventory.cInvName LIKE '%{0}%'", cInvName);
            }

            try
            {
                dt = DBHelperSQL.QueryTable(connectionString, strSql);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据采购到货单查询存货编码
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-18</remarks>
        public DataTable SearchInventoryByPUArrival(string connectionString, string cCode, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            string strSql = string.Format(@" select pu_ArrHead.id,pu_ArrBody.autoid,dbo.pu_arrbody.cbatch,dbo.pu_arrbody.dpdate as dMDate,dbo.pu_arrbody.iquantity,inventory.* 
 FROM  pu_ArrBody  
 inner join pu_ArrHead on pu_ArrHead.id=pu_ArrBody.id 
 inner join (SELECT cInvCode,cInvName,cInvStd,CASE cMassUnit WHEN 3 THEN '天' WHEN 2 THEN '月' WHEN 1 THEN '年' ELSE '' END AS cMassUnit,iMassDate FROM Inventory) inventory on pu_arrbody.cinvcode=inventory.cinvcode  
 Where  CCODE = N'{0}' 
 and isnull(cbustype,'')<>'委外加工' 
 And isnull(cbcloser,N'')=N'' 
 And isnull(cverifier,'')<>'' 
 And iBillType =N'0' 
 and isnull(bGsp,N'')=N'0' 
 and (isnull(iQuantity,0)-isnull(fRefuseQuantity,0)>isnull(fValidInQuan,0) or (igrouptype=2 and isnull(inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0)))", cCode);
            try
            {
                dt = DBHelperSQL.QueryTable(connectionString, strSql);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据销售发货单查询存货编码
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cDLCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-19</remarks>
        public DataTable SearchInventoryBySaleDelivery(string connectionString, string cDLCode, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            string strSql = string.Format(@" SELECT dbo.DispatchLists.cBatch,dbo.DispatchLists.dMDate,dbo.DispatchLists.iQuantity,Inventory.*
 from DispatchList 
 inner join   DispatchLists ON DispatchList.DLID = DispatchLists.DLID  
  INNER JOIN (SELECT cInvCode,cInvName,cInvStd,CASE cMassUnit WHEN 3 THEN '天' WHEN 2 THEN '月' WHEN 1 THEN '年' ELSE '' END AS cMassUnit,iMassDate,bInvType,bService,cComUnitCode,iGroupType FROM dbo.Inventory) Inventory ON DispatchLists.cInvCode = Inventory.cInvCode 
  left join ComputationUnit on Inventory.ccomunitcode=ComputationUnit.ccomunitcode 
 inner join Warehouse on DispatchLists.cwhcode=warehouse.cwhcode
 LEFT OUTER JOIN SaleType ON DispatchList.cSTCode = SaleType.cSTCode 
 LEFT OUTER JOIN (select cpersoncode as cpersoncode2,cpersonname from Person) person ON DispatchList.cPersonCode = Person.cPersonCode2 
 LEFT OUTER JOIN  Customer ON DispatchList.cCusCode = Customer.cCusCode 
 LEFT OUTER JOIN Department ON DispatchList.cDepCode = Department.cDepCode
 left join SaleBillVouch on SaleBillVouch.SBVID = dbo.DispatchList.SBVID  
 left join VouchType on VouchType.cVouchType = case when  salebillvouch.SBVID is null then dispatchlist.cVouchType else salebillvouch.cVouchType end  
 WHERE  (DispatchList.cVouchType='05' OR DispatchList.cVouchType='06')  
 AND  ((ISNULL(DispatchList.cSaleOut,'') = '' OR isnull(bqaneedcheck,0)=1) OR DispatchList.cSaleOut ='ST')    
 AND not (isnull(DispatchList.bFirst,0) =1 And isnull(DispatchLists.bIsStQC,0) =0) 
 and (
		 ( ABS(ISNULL( (case when (isnull(DispatchLists.bQANeedCheck,0)=1 and DispatchLists.iquantity>0) then DispatchLists.iqaquantity else DispatchLists.iquantity end ),0))
		 -ABS(ISNULL(DispatchLists.fOutQuantity,0))    ) >=0.01
		  or 
		  ( igrouptype=2 and  (  ABS(ISNULL((case when (isnull(DispatchLists.bQANeedCheck,0)=1 and DispatchLists.iquantity>0) then DispatchLists.iqanum else DispatchLists.inum end ),0))
		  -ABS(ISNULL(DispatchLists.fOutnum,0)) ) >=0.01)
	  )
AND (ISNULL(DispatchLists.cWhCode,'')<>'') 
AND bInvType=0
 AND bService=0  
 AND  ((ISNULL(DispatchLists.bSettleAll,0)=0 and DispatchList.cvouchtype='05') or DispatchList.cvouchtype='06')  
 and  ISNULL(DispatchList.cVerifier,'')<>''  
 AND DISPATCHLIST.CDLCODE = N'{0}'
 and  (
	(case when isnull(DispatchLists.bQANeedCheck,0)=1 and isnull(DispatchLists.iquantity,0)>0 then DispatchLists.iqaquantity else DispatchLists.iquantity end) <>0  
	OR
	(case when isnull(DispatchLists.bQANeedCheck,0)=1  and isnull(DispatchLists.inum,0)>0 then DispatchLists.iqanum else DispatchLists.inum end) <>0
	) ", cDLCode);
            try
            {
                dt = DBHelperSQL.QueryTable(connectionString, strSql);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据存货编码与批次查询标签流水号（打印小标签时需要流水号）
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="cBatch"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-17 20:10</remarks>
        public int GetLabelSerialNumber(string connectionString, string cInvCode, string cBatch, out string errMsg)
        {
            errMsg = string.Empty;
            int result;
            string strSql = string.Format("SELECT Number FROM UFSystem..SerialNumber WHERE cInvCode='{0}' AND cBatch ='{1}'", cInvCode, cBatch);
            try
            {
                object obj = DBHelperSQL.ExecuteScalar(connectionString, strSql);
                result = Cast.ToInteger(obj);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// 添加或修改标签流水号
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cBatch">批次</param>
        /// <param name="type">类型 0：添加,非0:修改</param>
        /// <param name="number">最新的流水号</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-17 20:11</remarks>
        public bool ModifySerialNumber(string connectionString, string cInvCode, string cBatch, int type, int number, out string errMsg)
        {
            errMsg = string.Empty;
            bool flag = false;
            string strSql = string.Empty;
            if (type == 0)//添加
            {
                strSql = string.Format("INSERT INTO UFSystem..SerialNumber ( cInvCode, cBatch, Number ) VALUES  ('{0}','{1}',{2})", cInvCode, cBatch, number);
            }
            else
            {
                strSql = string.Format("UPDATE UFSystem..SerialNumber SET Number ={0} WHERE cInvCode='{1}' AND cBatch='{2}'", number, cInvCode, cBatch);
            }

            try
            {
                int result = DBHelperSQL.ExecuteSql(connectionString, strSql);
                if (result > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

    }
}
