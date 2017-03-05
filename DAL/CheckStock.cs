using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace DAL
{
    /// <summary>
    /// 仓库盘点
    /// </summary>
    public class CheckStock
    {
        /// <summary>
        /// 查询盘点单列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-02</remarks>
        public DataTable SelectCheckVouchList(string connectionString, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            string strSql = @"SELECT cCVCode,cv.cWhCode,ws.cWhName,dCVDate FROM 
(SELECT cCVCode,cWhCode,dCVDate FROM dbo.CheckVouch WHERE dveridate IS NULL AND dnverifytime IS NULL) cv
INNER JOIN dbo.Warehouse  ws ON cv.cWhCode = ws.cWhCode";
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
        /// 根据盘点单、存货编码、批次查询盘点信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="checkVouchs"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-03</remarks>
        public bool SelectCheckVouch(string connectionString,ref CheckVouchs checkVouchs,out string errMsg)
        {
            bool flag = false;
            errMsg = string.Empty;

            string strSql = string.Format(@"SELECT cv.* ,i.cInvName,i.cInvStd FROM 
(SELECT cCVCode,cInvCode,cCVBatch,iCVQuantity,iCVCQuantity,iMassDate,CASE cMassUnit WHEN 3 THEN '天' WHEN 2 THEN '月' WHEN 1 THEN '年' ELSE '' END AS cMassUnit,dMadeDate,dDisDate FROM dbo.CheckVouchs WHERE cCVCode='{0}' AND cInvCode='{1}' AND cCVBatch='{2}') cv
INNER JOIN dbo.Inventory i ON cv.cInvCode = i.cInvCode", checkVouchs.cCVCode,checkVouchs.cInvCode,checkVouchs.cCVBatch);
                        
            DataTable dt = DBHelperSQL.QueryTable(connectionString, strSql);
            if (dt == null || dt.Rows.Count == 0)
            {
                errMsg = "该存货不在此盘点单中！";
                return flag;
            }

            DataRow row = dt.Rows[0];
            checkVouchs.cInvName = Cast.ToString(row["cInvName"]);
            checkVouchs.cInvStd = Cast.ToString(row["cInvStd"]);
            checkVouchs.iMassDate = Cast.ToInteger(row["iMassDate"]);
            checkVouchs.cMassUnit = Cast.ToString(row["cMassUnit"]);
            checkVouchs.dDisDate = Cast.ToDateTime(row["dDisDate"]);
            checkVouchs.dMadeDate = Cast.ToDateTime(row["dMadeDate"]);
            checkVouchs.iCVQuantity = Cast.ToDouble(row["iCVQuantity"]);
            flag = true;
            return flag;
        }

        /// <summary>
        /// 保存盘点数量
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="list"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-03</remarks>
        public bool SaveCheckVouch(string connectionString, List<CheckVouchs> list, out string errMsg)
        {
            errMsg = string.Empty;
            bool flag = false;
            //修改盘点数量（iCVCQuantity）/ 盘点金额（iSjDJ）/盈亏金额（iSjJe）
            string strSql = "UPDATE dbo.CheckVouchs SET iCVCQuantity={0},iSjDJ= iJhdj*{0},iSjJe= CASE {0}- iCVQuantity WHEN 0 THEN NULL ELSE ({0}-iCVQuantity )*iJhdj END  WHERE cCVCode='{1}' AND cInvCode='{2}' AND cCVBatch='{3}';";
            StringBuilder tempSql = new StringBuilder();
            foreach (CheckVouchs cv in list)
            {
                tempSql.Append(string.Format(strSql, cv.iCVCQuantity, cv.cCVCode, cv.cInvCode, cv.cCVBatch));
            }
            try
            {
                int result = DBHelperSQL.ExecuteSql(connectionString, tempSql.ToString());
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
