using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class Common
    {
        /// <summary>
        /// 日志开关
        /// </summary>
        public static bool flag;
        /// <summary>
        /// DAL层日志对象
        /// </summary>
        public static log4net.ILog log = null;
       

        #region 查询账套信息

        /// <summary>
        /// 查询所有账套信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-09</remarks>
        public static DataTable GetUAAccountInfo(string connectionString, string user_Id)
        {
            string strSql = string.Empty;
            if (user_Id == null)
            {
                strSql = string.Format(@"select  cAcc_ID as code,cAcc_name as name ,cUnitAbbre as Abbre,'' as industrytype from ufsystem..ua_account with (nolock)");
            }
            else
            {
                strSql = string.Format(@"
select  cAcc_ID as code,cAcc_name as name ,cUnitAbbre as Abbre,'' as industrytype 
from ufsystem..ua_account with (nolock) 
where cAcc_id IN
	(
		--查询用户名为‘gq’的账套号
		select cacc_id from ufsystem..ua_holdauth with (nolock) where cuser_id=N'{0}' and iisuser=1 group by cacc_id 
		Union All  
		--查询用户名为‘gq’所在组的账套号
		select  cacc_id from ufsystem..ua_holdauth with (nolock) where cUser_id in(select  distinct cgroup_id from ufsystem..ua_role with (nolock) where cUser_id=N'{0}' ) and iIsuser=0 group by cacc_id 
	)
order by cacc_id", user_Id);
            }
            return DBHelperSQL.QueryTable(connectionString, strSql);
        }

        #endregion

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Login(User user, DBInfo info)
        {
            if (user == null)
                return false;

            user.ConnectionString = string.Format(@"user id={0};password={1};data source={2};persist security info=True;initial catalog=UFDATA_{3}_{4};Connection Timeout=30", info.SqlUser, info.SqlPassword, info.DBServer, user.AccID, user.Year);
            bool flag = false;
            try
            {
                UFSoft.U8.Framework.Login.UI.clsLogin netLogin = new UFSoft.U8.Framework.Login.UI.clsLogin();
                string strSql = string.Format("select top 1 cUser_Name from ufsystem..ua_user where cUser_id=N'{0}' and cPassword='{1}'", user.UserID, netLogin.EnPassWord(user.Password));
                //u8Login = new clsLogin();
                //isLogin = flag = u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial);                
                DataTable dt = DBHelperSQL.QueryTable(user.ConnectionString, strSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    user.UserName = dt.Rows[0][0].ToString();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return flag;
        }

        /// <summary>
        /// 登录成功后,查询该用户权限下所有的仓库
        /// </summary>
        /// <param name="dtWarehouse"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool GetWarehouse(User user, out DataTable dtWarehouse, out string errMsg)
        {
            dtWarehouse = null;
            errMsg = string.Empty;
            try
            {
                ///2012－10－24
                ///仓库权限：如果用户是账套主管或所在角色为账套主管UA_holdauth
                ///或者如果用户是权限主管或所有角色为权限主管AA_holdBusobject 
                ///那么显示所有的仓库
                ///如果不是那就进行普通权限分配查询，用户的仓库权限或所有角色仓库权限
                string strSql = string.Format(@"IF EXISTS(   
--查询用户是否为账套主管
SELECT 1 FROM  UFSystem.dbo.UA_HoldAuth WHERE cAuth_Id ='admin' AND cAcc_Id='{0}' AND iYear='{1}' AND cUser_Id ='{2}'
UNION ALL
--查询用户所有角色是否为账套主管
SELECT 1 FROM 
(SELECT cUser_Id FROM UFSystem.dbo.UA_HoldAuth WHERE cAuth_Id ='admin' AND cAcc_Id='{0}' AND iYear='{1}') h 
INNER JOIN (SELECT cGroup_Id FROM UFSystem.dbo.UA_Role WHERE cUser_Id='{2}') r ON h.cUser_Id= r.cGroup_Id

UNION ALL
--查询用户是否为仓库权限主管
SELECT 1 FROM AA_holdbusobject WHERE iAdmin =1 AND CBusObId ='warehouse' AND cUserId= '{2}'
UNION ALL
--查询用户所在角色是否仓库权限主管
SELECT 1 FROM 
(SELECT cUserId FROM  AA_holdbusobject WHERE iAdmin =1 AND CBusObId ='warehouse' ) h
INNER JOIN (SELECT cGroup_Id FROM UFSystem.dbo.UA_Role WHERE cUser_Id='{2}') r ON h.cUserId = r.cGroup_Id

)
--若是返回1
SELECT 1 AS flag
ELSE --否则返回0
SELECT 0 AS flag ",user.AccID,user.Year,user.UserID);

                int flag = Convert.ToInt32(DBHelperSQL.ExecuteScalar(user.ConnectionString, strSql));
                //如果是账套主管显示所有仓库
                if (flag == 1)
                {
                    strSql = "Select cwhname,cwhcode,bWhPos from Warehouse";
                }
                else
                {
                    ///根据用户名查询权限分配表 查找该用户有哪些仓库可用
                    ///cBusObId业务对象标识 这里为'仓库'
                    strSql = string.Format(@"SELECT wh.* FROM 
(Select cwhname,cwhcode,bWhPos from Warehouse) wh
INNER JOIN
(
--查询该用户的仓库权限
SELECT cACCode FROM aa_holdauth WHERE cBusObId='warehouse' AND cUserId='{0}'
UNION ALL
--查询该用户所在角色的仓库权限
SELECT ha.cACCode FROM 
(SELECT cACCode,cUserId FROM dbo.AA_HoldAuth WHERE cBusObId ='warehouse' AND isUserGroup=1 ) ha
INNER JOIN (SELECT cGroup_Id,cUser_Id FROM UFSystem.dbo.UA_Role WHERE cUser_Id='{0}') r ON ha.cUserId = r.cGroup_Id
) temp ON wh.cwhcode =temp.cACCode", user.UserID);
                }

                dtWarehouse = DBHelperSQL.QueryTable(user.ConnectionString, strSql);
                return true;
            }

            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            
        }


        /// <summary>
        /// 根据用户ID查询用户操作权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public  DataTable Competence(User user, out string errMsg)
        {
            errMsg = string.Empty;
            string strSql = string.Empty;
            strSql = string.Format(@"
--判断是否为账套主管:一个为账套主管组，一个为功能编码admin,功能名称：账套主管
SELECT CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='admin' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id", user.UserID, user.Year, user.AccID);
            
            //如果为1，说明为账套主管
            if (DBHelperSQL.ExecuteScalar(user.ConnectionString,strSql).ToString()== "1")
            {
                //销售出库、盘点
                strSql = @"SELECT 1 AS XSCK,1 AS PD,1 AS Admin";   
                
                return DBHelperSQL.QueryTable(user.ConnectionString,strSql);
            }
            //否则为普通操作人员
            else
            {
                strSql = string.Format(@"
--普通操作人员 
DECLARE 
@XSCK INT,--销售出库(红字)(ASM0202:销售出库单录入,ASM0203:销售出库单审核)
@PD INT --盘点(ST010202:盘点单录入)

--销售出库(包括红字)
SELECT @XSCK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT cUser_Id,cGroup_Id FROM UFSystem..UA_Role WHERE cUser_Id='{0}'
UNION ALL
SELECT  cUser_Id,NULL as cGroup_Id  FROM UFSystem..UA_User WHERE cUser_Id='{0}') r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN('ASM0202' ,'ASM0203') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--盘点
SELECT @PD = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT  cUser_Id,cGroup_Id  FROM UFSystem..UA_Role WHERE cUser_Id='{0}' 
UNION ALL
SELECT  cUser_Id,NULL as cGroup_Id  FROM UFSystem..UA_User WHERE cUser_Id='{0}') r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ST010202' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id

SELECT @XSCK AS XSCK,@PD AS PD,0 AS Admin", user.UserID, user.Year, user.AccID);

                return DBHelperSQL.QueryTable(user.ConnectionString, strSql);
            }
        }
       
        /// <summary>
        /// 根据存货编码查询存货
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <returns></returns>
        //public static Inventory GetInventory(string cInvCode)
        //{
        //    Inventory data = null;
        //    string strSql = string.Format("SELECT * FROM dbo.Inventory WHERE cInvCode='{0}'", cInvCode);
        //    DataSet ds = DBHelperSQL.Query(strSql);
        //    if (ds != null)
        //    {
        //        DataRow row = ds.Tables[0].Rows[0];
        //        data = new Inventory();
        //        data.BarCode = Cast.ToString(row["cBarCode"]);
        //        data.bBarCode = Cast.ToBoolean(row["bBarCode"]);
        //        data.bComsume = Cast.ToBoolean(row["bComsume"]);
        //        data.bInvBatch = Cast.ToBoolean(row["bInvBatch"]);
        //        data.bInvEntrust = Cast.ToBoolean(row["bInvEntrust"]);
        //        data.bInvOverStock = Cast.ToBoolean(row["bInvOverStock"]);
        //        data.bInvQuality = Cast.ToBoolean(row["bInvQuality"]);
        //        data.bPropertyCheck = Cast.ToBoolean(row["bPropertyCheck"]);
        //        data.bPurchase = Cast.ToBoolean(row["bPurchase"]);
        //        data.bSale = Cast.ToBoolean(row["bSale"]);
        //        data.bSelf = Cast.ToBoolean(row["bSelf"]);
        //        data.cAddress = Cast.ToString(row["cAddress"]);
        //        data.cComUnitCode = Cast.ToString(row["cComUnitCode"]);
        //        data.cCreatePerson = Cast.ToString(row["cCreatePerson"]);
        //        data.cCurrencyName = Cast.ToString(row["cCurrencyName"]);
        //        data.cEnterprise = Cast.ToString(row["cEnterprise"]);
        //        data.cFile = Cast.ToString(row["cFile"]);
        //        data.cGroupCode = Cast.ToString(row["cGroupCode"]);
        //        data.cInvCCode = Cast.ToString(row["cInvCCode"]);
        //        data.cInvCode = Cast.ToString(row["cInvCode"]);
        //        data.cInvDefine1 = Cast.ToString(row["cInvDefine1"]);
        //        data.cInvDefine4 = Cast.ToString(row["cInvDefine4"]);
        //        data.cInvDefine5 = Cast.ToString(row["cInvDefine5"]);
        //        data.cInvDefine6 = Cast.ToString(row["cInvDefine6"]);
        //        data.cInvDefine7 = Cast.ToString(row["cInvDefine7"]);
        //        data.cInvDefine8 = Cast.ToString(row["cInvDefine8"]);
        //        data.cInvDefine9 = Cast.ToString(row["cInvDefine9"]);
        //        data.cInvName = Cast.ToString(row["cInvName"]);
        //        data.cInvStd = Cast.ToString(row["cInvStd"]);
        //        data.cLabel = Cast.ToString(row["cLabel"]);
        //        data.cMassUnit = Cast.ToInteger(row["cMassUnit"]);
        //        data.cModifyPerson = Cast.ToString(row["cModifyPerson"]);
        //        data.cPosition = Cast.ToString(row["cPosition"]);
        //        data.cPreparationType = Cast.ToString(row["cPreparationType"]);
        //        data.cReplaceItem = Cast.ToString(row["cReplaceItem"]);
        //        data.cVenCode = Cast.ToString(row["cVenCode"]);
        //        data.dModifyDate = Cast.ToDateTime(row["dModifyDate"]);
        //        data.dSDate = Cast.ToDateTime(row["dSDate"]);
        //        data.fRetailPrice = Cast.ToDouble(row["fRetailPrice"]);
        //        data.iGroupType = Cast.ToBoolean(row["iGroupType"]);
        //        data.iInvLSCost = Cast.ToDouble(row["iInvLSCost"]);
        //        data.iInvNCost = Cast.ToDouble(row["iInvNCost"]);
        //        data.iInvSCost = Cast.ToDouble(row["iInvSCost"]);
        //        data.iLowSum = Cast.ToDouble(row["iLowSum"]);
        //        data.iMassDate = Cast.ToInteger(row["iMassDate"]);
        //        data.iTaxRate = Cast.ToDouble(row["iTaxRate"]);
        //        data.iWarnDays = Cast.ToInteger(row["iWarnDays"]);
        //    }
        //    return data;
        //}

        /// <summary>
        /// 根据单据类型编号，日期生成单据编号
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="dDate"></param>
        /// <returns></returns>
        //public string GetNewNumber(string cardNumber, DateTime dDate)
        //{
        //    string strSql = string.Format("Select * From VoucherNumber Where CardNumber='{0}'",cardNumber);
        //    DataSet ds = DBHelperSQL.Query(strSql);
        //    if (ds.Tables.Count == 0||ds.Tables[0].Rows.Count==0)
        //    {
        //        return "";
        //    }
        //    DataRow row = ds.Tables[0].Rows[0];
        //    VoucherNumber voucherNumber = new VoucherNumber();
        //    voucherNumber.cSub_id = Cast.ToString(row["cSub_id"]);
        //    voucherNumber.AppName = Cast.ToString(row["AppName"]);
        //    voucherNumber.CardName = Cast.ToString(row["CardName"]);
        //    voucherNumber.CardNumber = Cast.ToString(row["CardNumber"]);
        //    voucherNumber.iSize = Cast.ToInteger(row["iSize"]);
        //    voucherNumber.iStartNumber = Cast.ToInteger(row["iStartNumber"]);
        //    voucherNumber.Prefix1 = Cast.ToString(row["Prefix1"]);
        //    voucherNumber.Prefix1Len = Cast.ToInteger(row["Prefix1Len"]);
        //    voucherNumber.Prefix1Rule = Cast.ToString(row["Prefix1Rule"]);

        //    voucherNumber.Prefix2 = Cast.ToString(row["Prefix2"]);
        //    voucherNumber.Prefix2Len = Cast.ToInteger(row["Prefix2Len"]);
        //    voucherNumber.Prefix2Rule = Cast.ToString(row["Prefix2Rule"]);

        //    voucherNumber.Prefix3 = Cast.ToString(row["Prefix3"]);
        //    voucherNumber.Prefix3Len = Cast.ToInteger(row["Prefix3Len"]);
        //    voucherNumber.Prefix3Rule = Cast.ToString(row["Prefix3Rule"]);

        //    voucherNumber.Glide = Cast.ToString(row["Glide"]);
        //    voucherNumber.GlideLen = Cast.ToInteger(row["GlideLen"]);
        //    voucherNumber.GlideRule = Cast.ToString(row["GlideRule"]);

        //    string cSeed = string.Empty;
        //    //判断前缀1规则
        //    if (voucherNumber.Prefix1Rule == "年月")
        //    {
        //        cSeed = dDate.ToString("yyMM");
        //    }
        //    else
        //    {
        //        cSeed = dDate.ToString("yyyyMMdd");
        //    }
        //    int serialNumber;//流水号
        //    strSql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent='{1}' and cSeed='{2}'",cardNumber,voucherNumber.Glide,cSeed);
        //    object obj = DBHelperSQL.ExecuteScalar(strSql);
        //    if (obj == null || obj.Equals(DBNull.Value))
        //    {
        //        serialNumber = voucherNumber.iStartNumber;
        //    }
        //    else
        //    {
        //        serialNumber = Convert.ToInt32(obj) + 1;
        //    }

        //    if (voucherNumber.Prefix1Rule == "年月")
        //    {
        //        cSeed = dDate.ToString("yyyyMM");
        //    }
        //    return cSeed + serialNumber.ToString().PadLeft(4, '0');
        //}

        /// <summary>
        /// 获取单据类型编号，模板号
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cardNumber"></param>
        /// <param name="def_id"></param>
        /// <returns></returns>
        public bool GetCardNumber(string connectionString,string name,out string cardNumber,out int def_id)
        {
            cardNumber = string.Empty;
            def_id = 0;

            string strSql = string.Format("select def_id,cardnumber from Vouchers where ccardname='{0}'",name);
            DataSet ds = DBHelperSQL.Query(connectionString, strSql);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            cardNumber = Cast.ToString(ds.Tables[0].Rows[0]["cardNumber"]);
            def_id = Cast.ToInteger(ds.Tables[0].Rows[0]["def_id"]);
            return true;
        }


        /// <summary>
        /// 保质期单位转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int CastTocMassUnit(object obj)
        {
            int cMassUnit;
            switch (obj.ToString ())
            { 
                case "年":
                    cMassUnit = 1;
                    break;
                case "月":
                    cMassUnit = 2;
                    break;
                case "日":
                    cMassUnit = 3;
                    break;
                default:
                    cMassUnit = 0;
                    break;
            }
            return cMassUnit;
        }

        /// <summary>
        /// 有效期推算方式转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int CastToiExpiratDateCalcu(object obj)
        {
            int iExpiratDateCalcu;
            switch(obj.ToString())
            {
                case "月":
                    iExpiratDateCalcu = 1;
                    break;
                case "日":
                    iExpiratDateCalcu = 2;
                    break;
                default:
                    iExpiratDateCalcu = 0;
                    break;
            }
            return iExpiratDateCalcu;
        }


        #region 公供查询

        /// <summary>
        /// 查询采购到货列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-21</remarks>
        public DataTable SelectPurchaseList(string connectionString,List<Warehouse> whList, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            string condition = string.Empty;//条件
            ////判断仓库列表是否为空
            //if (whList == null || whList.Count == 0)
            //{
            //    errMsg = "没有仓库权限";
            //    return dt;
            //}
            ////封装仓库条件
            //condition = string.Format(" AND cWhCode IN(");
            //foreach (Warehouse wh in whList)
            //{
            //    condition += string.Format("'{0}',", wh.cWhCode);
            //}
            //condition = condition.Substring(0, condition.Length - 1);
            //condition += ")";

            string strSql = string.Format(@"SELECT DISTINCT pu_ArrHead.id,pu_ArrHead.ccode,pu_ArrHead.ddate,pu_ArrHead.cvenabbname 
FROM  pu_ArrHead  
inner join pu_ArrBody   on dbo.pu_ArrHead.id = dbo.pu_arrbody.id 
Where	
isnull(cbustype,'')<>'委外加工'
And isnull(cbcloser,N'')=N'' 
And isnull(cverifier,'')<>'' 
And iBillType =N'0' 
and isnull(bGsp,N'')=N'0' 
and	(
	isnull(iQuantity,0)-isnull(fRefuseQuantity,0)>isnull(fValidInQuan,0) 
	or	(igrouptype=2 and isnull(inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0))
)
 
ORDER BY dbo.pu_ArrHead.id DESC");

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
        /// 查询销售发货单列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="whList">仓库列表</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-21</remarks>
        public DataTable SelectSaleDeliveryList(string connectionString,List<Warehouse> whList, out string errMsg)
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
            condition= condition.Substring(0, condition.Length - 1);
            condition += ")";

            string strSql = string.Format(@"SELECT DISTINCT DispatchList.DLID, DispatchList.cDLCode,DispatchList.cSBVCode,DispatchList.dDate,customer.cCusAbbName
 from DispatchList inner join   DispatchLists ON DispatchList.DLID = DispatchLists.DLID  
  INNER JOIN  Inventory ON DispatchLists.cInvCode = Inventory.cInvCode LEFT OUTER JOIN  Customer ON DispatchList.cCusCode = Customer.cCusCode 
 WHERE  
 (DispatchList.cVouchType='05' OR DispatchList.cVouchType='06')  
 AND((ISNULL(DispatchList.cSaleOut,'') = '' OR isnull(bqaneedcheck,0)=1) OR DispatchList.cSaleOut ='ST')    
 AND not(isnull(DispatchList.bFirst,0) =1 And isnull(DispatchLists.bIsStQC,0) =0) 
 and (	
	(ABS(ISNULL((case when (isnull(DispatchLists.bQANeedCheck,0)=1 and DispatchLists.iquantity>0) then DispatchLists.iqaquantity else DispatchLists.iquantity end ),0))-ABS(ISNULL(DispatchLists.fOutQuantity,0))    ) >=0.01 
	or	( igrouptype=2 and  (  ABS(ISNULL((case when (isnull(DispatchLists.bQANeedCheck,0)=1 and DispatchLists.iquantity>0) then DispatchLists.iqanum else DispatchLists.inum end ),0))-ABS(ISNULL(DispatchLists.fOutnum,0)) ) >=0.01))
 AND (ISNULL(DispatchLists.cWhCode,'')<>'') 
 AND bInvType=0 
 AND bService=0  
 AND  (	(ISNULL(DispatchLists.bSettleAll,0)=0 and DispatchList.cvouchtype='05') or DispatchList.cvouchtype='06')  
 and  ISNULL(DispatchList.cVerifier,'')<>''  
 AND (	
	 (case when isnull(DispatchLists.bQANeedCheck,0)=1 and isnull(DispatchLists.iquantity,0)>0 then DispatchLists.iqaquantity else DispatchLists.iquantity end) <>0  
	 OR	(case when isnull(DispatchLists.bQANeedCheck,0)=1  and isnull(DispatchLists.inum,0)>0 then DispatchLists.iqanum else DispatchLists.inum end) <>0
	 )
{0}
ORDER BY dbo.DispatchList.DLID DESC",condition);

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

        #endregion

    }
}
