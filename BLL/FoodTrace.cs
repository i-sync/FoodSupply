using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace BLL
{
    /// <summary>
    /// 食品追溯
    /// </summary>
    public class FoodTrace
    {
        /// <summary>
        /// 食品追溯
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="cBatch"></param>
        /// <param name="Number"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-02</remarks>
        public DataTable Trace(string cInvCode, string cBatch, string Number, out string errMsg)
        {
            DataTable dt = Common.Instance.Service.Trace(Common.Instance.User.ConnectionString, cInvCode, cBatch, Number, out errMsg);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            return dt;
        }

        /// <summary>
        /// 查询供应商资质
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cVenCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-01</remarks>
        public List<VendorLicense> VendorQuanlification(string cVenCode, out string errMsg)
        {
            List<VendorLicense> list = null;
            DataTable dt = Common.Instance.Service.VendorQuanlification(Common.Instance.User.ConnectionString, cVenCode, out errMsg);
            if (dt == null || dt.Rows.Count == 0)
                return list;

            list = new List<VendorLicense>();
            VendorLicense vl;
            foreach (DataRow row in dt.Rows)
            {
                vl = new Model.VendorLicense();
                vl.cLicenseType = Cast.ToString(row["cLicenseType"]);
                vl.cLicenseCode = Cast.ToString(row["cLicenseCode"]);
                vl.cLicenseName = Cast.ToString(row["cLicenseName"]);
                vl.cLicenseNum = Cast.ToString(row["cLicenseNum"]);
                vl.dValidDate = Cast.ToDateTime(row["dValidDate"]);
                vl.dEndDate = Cast.ToDateTime(row["dEndDate"]);
                list.Add(vl);
            }
            return list;
        }
    }
}
