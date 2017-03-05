using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    [Serializable]
    public class User
    {
        public string SubId
        {
            get;
            set;
        }
        /// <summary>
        /// 账套名称
        /// </summary>
        public string AccID
        {
            get;
            set;
        }
        /// <summary>
        /// 年度
        /// </summary>
        public int Year
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server
        {
            get;
            set;
        }

        public string Serial
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
