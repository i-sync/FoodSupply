using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 数据库信息
    /// </summary>
    [Serializable]
    public class DBInfo
    {
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string DBServer { get; set; }
        /// <summary>
        /// ERP服务器
        /// </summary>
        public string ERPServer { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string SqlUser { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string SqlPassword { get; set; }
    }
}
