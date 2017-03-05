using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 追溯
    /// </summary>
    public class FoodTrace
    {
        /// <summary>
        /// 食品追溯
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cInvCode"></param>
        /// <param name="cBatch"></param>
        /// <param name="Number"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-01</remarks>
        public DataTable Trace(string connectionString, string cInvCode, string cBatch, string Number, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            string strSql = string.Format(@"SELECT inventory.cInvName,inventory.cInvStd,arrChild.cBatch,arrChild.dMDate,arrChild.iAQuantity,cu.cComunitName,arrMain.cVenCode, vendor.cVenName, vendor.cVenAbbName,inventory.iMassDate,CASE inventory.cMassUnit WHEN 3 THEN '天' WHEN 2 THEN '月' WHEN 1 THEN '年' ELSE '' END AS cMassUnit,RdRecord.cMaker,RdRecord.dDate,RdRecord.cCode,RdRecord.cBusCode, RdRecord.iarriveid,customer.cCusAbbName,RdRecords.iSQuantity FROM 
--查询采购到货信息（cInvCode,cBatch,dMdate,iQuantity）
(SELECT ID,cInvCode,cBatch,dPDate AS dMDate,iQuantity as iAQuantity FROM dbo.PU_ArrivalVouchs WHERE cInvCode ='{0}' AND cBatch='{1}') arrChild
INNER JOIN (SELECT ID,cVenCode FROM dbo.PU_ArrivalVouch) arrMain ON arrChild.ID = arrMain.ID
INNER JOIN (SELECT cVenCode,cVenName,cVenAbbName FROM dbo.Vendor ) vendor ON arrMain.cVenCode = vendor.cVenCode
--查询存货信息(cInvName,cInvStd,iMassDate,cMassUnit)
INNER JOIN (SELECT cInvCode,cInvName,cInvStd,cComunitCode,iMassDate,cMassUnit FROM dbo.Inventory) inventory ON arrChild.cInvCode = inventory.cInvCode
INNER JOIN(SELECT cComunitCode,cComUnitName FROM  ComputationUnit) cu ON inventory.cComUnitCode = cu.cComunitCode
--查询销售信息(cCusAbbName,cMaker,dDate,iQuantity,cBusCode,cCode,iarriveid)
LEFT JOIN (SELECT TOP 1 RDID,RDSID,cInvCode,cBatch FROM UFSystem..RdRecordSN WHERE cInvCode ='{0}' AND cBatch='{1}' AND Number={2} ORDER BY ID DESC) sn ON arrChild.cInvCode = sn.cInvCode
LEFT JOIN (SELECT ID,cCusCode,cMaker,dDate,cCode,cBusCode,iarriveid FROM dbo.RdRecord) RdRecord ON sn.RDID = RdRecord.ID
LEFT JOIN (SELECT AutoID,iQuantity as iSQuantity FROM dbo.RdRecords) RdRecords ON sn.RDSID = RdRecords.AutoID
LEFT JOIN (SELECT cCusCode,cCusAbbName FROM dbo.Customer ) customer ON RdRecord.cCusCode = customer.cCusCode", cInvCode, cBatch, Model.Cast.ToInteger(Number));
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
        /// 查询供应商资质
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cVenCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable VendorQuanlification(string connectionString, string cVenCode, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            string strSql = string.Format(@"SELECT vls.* FROM 
(SELECT id FROM V_pl_gmp_vendorlicenseaudit WHERE cVendorCode='{0}') vl
INNER JOIN (SELECT autoid,id,cLicenseCode,cLicenseName,cLicenseType,cLicenseNum,dValidDate,dEndDate FROM V_pl_gmp_vendorlicenseaudits) vls ON vl.id = vls.id
ORDER BY vls.autoid ", cVenCode);
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
    }
}
