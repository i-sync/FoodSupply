using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class Competence
    {
        /// <summary>
        /// 销售出库(包括红字)(ASM0202:销售出库单录入,ASM0203:销售出库单审核)
        /// </summary>
        public bool XSCK
        {
            get;
            set;
        }
        /// <summary>
        /// 盘点(ST010202:盘点单录入)
        /// </summary>
        public bool PD
        {
            get;
            set;
        }

        /// <summary>
        /// 标识是否为账套主管
        /// </summary>
        public bool Admin
        {
            get;
            set;
        }
    }
}
