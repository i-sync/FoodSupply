using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 供应商资质
    /// </summary>
    public class VendorLicense
    {
        public string ID { get; set; }
        /// <summary>
        /// 证照类型
        /// </summary>
        public string cLicenseType { get; set; }
        /// <summary>
        /// 资料编码
        /// </summary>
        public string cLicenseCode { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public string cLicenseName { get; set; }
        /// <summary>
        /// 证照号码
        /// </summary>
        public string cLicenseNum { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime dValidDate { get; set; }
        /// <summary>
        /// 证照到期日
        /// </summary>
        public DateTime dEndDate { get; set; }
    }
}
