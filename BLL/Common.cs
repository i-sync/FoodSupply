using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Model;
using System.Data;

namespace BLL
{
    public class Common
    {
        private Service.Service service;
        private static Common instance;
        private static string serviceURL;//服务器URL地址
        private static object obj = new object();
        /// <summary>
        /// 登录用户信息
        /// </summary>
        private BLL.Service.User user;

        /// <summary>
        /// 仓库集合
        /// </summary>
        public static List<Warehouse> WarehouseList = new List<Warehouse>();
        /// <summary>
        /// 用户操作权限
        /// </summary>
        public static Competence Competence = new Competence ();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private Common() { 
            //初始化web服务
            service = new BLL.Service.Service();
            service.Url = serviceURL;// "http://192.168.3.236/U8APIService/Service.asmx";
        }

        /// <summary>
        /// 设置服务器URL地址
        /// </summary>
        public static string ServiceURL
        {
            set { serviceURL = value; }
        }

        /// <summary>
        /// 用户属性
        /// </summary>
        public BLL.Service.User User
        {
            get
            {
                return user;
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
        /// 登录前查询账套信息
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
        public List<KeyValuePair<string,string>> GetUAAcountInfo(string user_Id)
        {
            //读取账套信息
            DataTable dt =  service.GetUAAcountInfo(user_Id);
            if (dt == null)
                return null;
            //封装键值对对象
            List<KeyValuePair<string,string>> list = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kv;
            foreach (DataRow row in dt.Rows)
            {
                kv = new KeyValuePair<string, string>(row["code"].ToString(), string.Format("[{0}]{1}",row["code"],row["name"]));
                list.Add(kv);
            }
            return list;
        }
        
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Login(User user)
        {
            //登录成功存储登录者信息
            this.user = new BLL.Service.User();
            this.user = EntityConvert.ConvertClass<Model.User, BLL.Service.User>(user, this.user);
            bool flag = service.Login(ref this.user);
            if (flag)
            {
                GetWarehouse();//仓库
                GetCompetence();//权限
            }
            return flag;
        }

        /// <summary>
        /// 查询用户所有仓库
        /// </summary>
        /// <param name="dtWarehouse"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public void GetWarehouse()
        {
            DataTable dtWarehouse;
            string errMsg = string.Empty;
            bool flag  = service.GetWarehouse(user,out dtWarehouse, out errMsg);
            if (flag)
            {
                Warehouse warehouse;
                WarehouseList.Clear();
                foreach (DataRow dr in dtWarehouse.Rows)
                {
                    warehouse = new Warehouse();
                    warehouse.cWhCode = dr["cwhcode"].ToString();
                    warehouse.cWhName = dr["cwhname"].ToString();
                    warehouse.bwhpos = Convert.ToInt32(dr["bwhpos"]);
                    WarehouseList.Add(warehouse);
                }
            }
        }

        /// <summary>
        /// 获取用户操作权限
        /// </summary>
        public void GetCompetence()
        {
            string errMsg;
            DataTable dt;
            try
            {
                dt = service.Competence(user, out errMsg);
                if (dt != null && dt.Rows.Count == 1)
                {
                    Competence.XSCK = Cast.ToBoolean(dt.Rows[0]["XSCK"]);//销售出库
                    Competence.PD = Cast.ToBoolean(dt.Rows[0]["PD"]);//盘点
                    Competence.Admin = Cast.ToBoolean(dt.Rows[0]["Admin"]);//主管
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 自动升级

        /// <summary>
        /// 获取PC端服务版本信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        public string GetPCVersion()
        {
            return service.GetPCVersion();
        }

        /// <summary>
        /// 获取最新的更新文档
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        public string GetPCNewDocument()
        {
            return service.GetPCNewDocument();
        }

        /// <summary>
        /// 获取PC端服务版本信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        public string GetPPCVersion()
        {
            return service.GetPPCVersion();
        }

        /// <summary>
        /// 获取最新的更新文档
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        public string GetPPCNewDocument()
        {
            return service.GetPPCNewDocument();
        }
        #endregion
    }

}
