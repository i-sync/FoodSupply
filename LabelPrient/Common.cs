using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace LabelPrint
{
    public class Common
    {
        private Service.Service service;
        private static Common instance;
        private static object obj = new object();
        private string connectionString;

        /// <summary>
        /// 仓库列表
        /// </summary>
        private LabelPrint.Service.Warehouse[] whList;
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private Common()
        {
            //初始化web服务
            service = new LabelPrint.Service.Service();

            //读取配置文件 封装仓库列表
            string[] cwhcode=null;
            try
            {
                cwhcode = System.Configuration.ConfigurationManager.AppSettings["cWhCode"].ToString().Split(',');
            }
            catch (Exception ex)
            { 
                //
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }
            LabelPrint.Service.Warehouse wh;
            whList = new LabelPrint.Service.Warehouse[cwhcode.Length];
            int i = 0;
            foreach (string code in cwhcode)
            {
                wh = new LabelPrint.Service.Warehouse { cWhCode = code };
                whList[i++] = wh;
            }
        }

        /// <summary>
        /// 获取类的实例
        /// </summary>
        /// <returns></returns>
        public static Common Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new Common();
                        }
                        return instance;
                    }
                }
                return instance;
            }
        }

        public Service.Service Service
        {
            get
            {
                return service;
            }
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string URL
        {
            get
            {
                return service.Url;
            }
        }

        /// <summary>
        /// 仓库列表集合
        /// </summary>
        public LabelPrint.Service.Warehouse[] WHList
        {
            get
            {
                return whList;
            }
        }

        /// <summary>
        /// 连接字符串属性
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
        }


        /// <summary>
        /// 连接测试
        /// </summary>
        /// <returns></returns>
        public bool ConnectionTest(out string errMsg)
        {
            errMsg = string.Empty;
            bool flag = false;


            //首先判断webService.url是否可用 
            flag = IsWebServiceAvaiable(out errMsg);
            if (!flag)
                return flag;

            //读取配置信息
            string accid = System.Configuration.ConfigurationManager.AppSettings["Accid"].ToString();
            string year = System.Configuration.ConfigurationManager.AppSettings["Year"].ToString();

            //如果为空
            if (string.IsNullOrEmpty(accid) || string.IsNullOrEmpty(year))
            {
                errMsg = "客户端配置信息错误！";
                return flag ;
            }

            try
            {
                flag = service.ConnectionTest(accid, year, out connectionString, out errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        /// <summary>
        /// 判断Service.Url是否可用
        /// </summary>
        /// <returns></returns>
        private bool IsWebServiceAvaiable(out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                MSXML2.XMLHTTP60 xmlhttp = new MSXML2.XMLHTTP60();
                xmlhttp.open("GET", service.Url, null, null, null);
                xmlhttp.send(null);
                if (xmlhttp.status == 200)
                    return true;
                else
                {
                    errMsg = "WebService不可用！";
                    return false;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return false;
        }
    }
}
