using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 单据列表
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// 发货单
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 用于显示的单据号（如果是零售日报则显示零售日报号，如果是赊销则显示发货单号）
        /// </summary>
        public string ShowCode { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime dDate { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用于显示DropDownList
        /// </summary>
        public string Show
        {
            get {
                return string.Format("{0}|{1}|{2}", ShowCode, Name, dDate.ToShortDateString());
            }
        }
    }
}
