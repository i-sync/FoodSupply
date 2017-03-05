using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using Model;
using DAL;
using System.Data;
using System.Xml;
using System.IO;
using log4net;

namespace Service
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        //日志开关
        static bool flag = false;
        //日志对象
        static log4net.ILog log = null;
        //构造函数
        public Service()
        {
            if (log == null)
            {
                flag = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["log"]);

                log = log4net.LogManager.GetLogger("Service.Logging");
                DAL.Common.flag = flag;
                DAL.Common.log = log;
            }
        }

        [WebMethod(Description="HelloWorld")]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region 自动升级

        /// <summary>
        /// 获取PC端服务版本信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        [WebMethod(Description = "获取PC端服务版本信息")]
        public string GetPCVersion()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("PCupdate.xml"));
            XmlElement root = doc.DocumentElement;
            return root.SelectSingleNode("version").InnerText;
        }

        /// <summary>
        /// 获取最新的更新文档
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        [WebMethod(Description="获取最新的配置文档")]
        public string GetPCNewDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("PCupdate.xml"));
            return doc.InnerXml;
        }

        /// <summary>
        /// 获取PC端服务版本信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        [WebMethod(Description = "获取PC端服务版本信息")]
        public string GetPPCVersion()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("PPCupdate.xml"));
            XmlElement root = doc.DocumentElement;
            return root.SelectSingleNode("version").InnerText;
        }

        /// <summary>
        /// 获取最新的更新文档
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        [WebMethod(Description = "获取最新的配置文档")]
        public string GetPPCNewDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("PPCupdate.xml"));
            return doc.InnerXml ;
        }

        /// <summary>
        /// 更新对应文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [WebMethod(Description = "更新对应文件")]
        public byte[] GetUpdateFile(string filePath)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + filePath;
                //判断文件是否存在
                if (!File.Exists(path))
                {
                    log.Debug(string.Format("{0}文本不存在",filePath));
                    return null;
                }
                //读取文件
                if(flag)
                    log.Info(string.Format("读取文件：{0}",filePath));
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] array = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                //返回文件内容
                if(flag)
                    log.Info(string.Format("返回{0}文件内容!",filePath));
                return array;
            }
            catch (Exception ex)
            {
                log.Warn(ex.Message, ex);
                return null;
            }
        }

        #endregion

        #region 登录前的账套查询

        /// <summary>
        /// 根据用户名查询账套信息
        /// 此查询为登录前查询的主数据库，所以需要在这里拼装连接字符串
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description="登录前查询账套信息")]
        public DataTable GetUAAcountInfo(string user_Id)
        {
            string sqlUser = System.Configuration.ConfigurationManager.AppSettings.Get("SqlUser");
            string sqlPassword = System.Configuration.ConfigurationManager.AppSettings.Get("SqlPassword");
            string dbServer = System.Configuration.ConfigurationManager.AppSettings.Get("DBServer");

            string connectionString = string.Format(@"user id={0};password={1};data source={2};persist security info=True;initial catalog=UFSystem;Connection Timeout=30", sqlUser, sqlPassword, dbServer);

            return Common.GetUAAccountInfo(connectionString, user_Id);
        }

        #endregion

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [WebMethod(Description="用户登录")]
        public bool Login(ref User user)
        { 
            //封装DBInfo
            DBInfo dbInfo = new DBInfo ();
            dbInfo.DBServer = ConfigurationManager.AppSettings["DBServer"].ToString();
            dbInfo.ERPServer = ConfigurationManager.AppSettings["ERPServer"].ToString();
            dbInfo.SqlUser = ConfigurationManager.AppSettings["SqlUser"].ToString();
            dbInfo.SqlPassword = ConfigurationManager.AppSettings["SqlPassword"].ToString();
            return new DAL.Common().Login(user, dbInfo);
        }

        /// <summary>
        /// 登录成功后,查询该用户权限下所有的仓库
        /// </summary>
        /// <param name="dtWarehouse"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description="根据用户查询仓库信息")]
        public bool GetWarehouse(User user, out DataTable dtWarehouse, out string errMsg)
        {
            return new DAL.Common().GetWarehouse(user, out dtWarehouse, out errMsg);
        }


        /// <summary>
        /// 根据用户ID查询用户操作权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description = "根据用户ID查询用户操作权限")]
        public DataTable Competence(User user, out string errMsg)
        {
            return new DAL.Common().Competence(user, out errMsg);
        }

        #endregion

        #region 公共查询

        /// <summary>
        /// 查询采购到货列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="whList">仓库列表</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-21</remarks>
        [WebMethod(Description = "查询采购到货列表")]
        public DataTable SelectPurchaseList(string connectionString, List<Warehouse> whList, out string errMsg)
        {
            return new DAL.Common().SelectPurchaseList(connectionString, whList, out errMsg);
        }


        /// <summary>
        /// 查询销售发货单列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="whList">仓库列表</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-21</remarks>
        [WebMethod(Description="查询销售发货单列表")]
        public DataTable SelectSaleDeliveryList(string connectionString, List<Warehouse> whList, out string errMsg)
        {
            return new DAL.Common().SelectSaleDeliveryList(connectionString, whList, out errMsg);
        }
        #endregion

        #region 销售管理

        /// <summary>
        /// 销售发货:单据装载
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cDLCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-20</remarks>
        [WebMethod(Description="销售发货单装载")]
        public DispatchList SA_Consignment_Load(string connectionString, string cDLCode, out string errMsg)
        {
            return new DAL.Consignment().Load(connectionString, cDLCode, out errMsg);
        }
        #endregion

        #region 库存管理

        /// <summary>
        /// 销售出库:保存单据
        /// </summary>
        /// <param name="rdRecord"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-26</remarks>
        [WebMethod(Description="销售出库单保存")]
        public bool ST_SaleOut_Save(User user, RdRecord rdRecord, out string errMsg)
        {
            return new DAL.SaleOut().Save(user, rdRecord, out errMsg);
        }

        #endregion

        #region 标签打印模块

        /// <summary>
        /// 标签打印数据库连接测试
        /// </summary>
        /// <param name="accid">账套</param>
        /// <param name="year">年度</param>
        /// <param name="connectionString">如何连接成功，返回连接字符串</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-14</remarks>
        [WebMethod(Description="连接测试")]
        public bool ConnectionTest(string accid, string year, out string connectionString, out string errMsg)
        {
            //封装DBInfo
            DBInfo dbInfo = new DBInfo();
            dbInfo.DBServer = ConfigurationManager.AppSettings["DBServer"].ToString();
            dbInfo.ERPServer = ConfigurationManager.AppSettings["ERPServer"].ToString();
            dbInfo.SqlUser = ConfigurationManager.AppSettings["SqlUser"].ToString();
            dbInfo.SqlPassword = ConfigurationManager.AppSettings["SqlPassword"].ToString();

            return new LabelPrint().ConnectionTest(accid, year, dbInfo, out connectionString, out errMsg);
        }

        /// <summary>
        /// 根据存货名称或编码查询存货信息
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="cInvName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-14</remarks>
        [WebMethod(Description="查询存货信息")]
        public DataTable SearchInventory(string connectionString, List<Warehouse> whList, string cInvCode, string cInvName, out string errMsg)
        {
            return new DAL.LabelPrint().SearchInventory(connectionString, whList, cInvCode, cInvName, out errMsg);
        }

        /// <summary>
        /// 根据采购到货单查询存货编码
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-18</remarks>
        [WebMethod(Description="根据到货单打印条码")]
        public DataTable SearchInventoryByPUArrival(string connectionString, string cCode, out string errMsg)
        {
            return new DAL.LabelPrint().SearchInventoryByPUArrival(connectionString, cCode, out errMsg);
        }

        /// <summary>
        /// 根据销售发货单查询存货编码
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cDLCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-19</remarks>
        [WebMethod(Description="根据发货单打印条码")]
        public DataTable SearchInventoryBySaleDelivery(string connectionString, string cDLCode, out string errMsg)
        {
            return new DAL.LabelPrint().SearchInventoryBySaleDelivery(connectionString, cDLCode, out errMsg);
        }

        /// <summary>
        /// 根据存货编码与批次查询标签流水号（打印小标签时需要流水号）
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="cBatch"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-17 20:10</remarks>
        [WebMethod(Description="查询小标签流水号")]
        public int GetLabelSerialNumber(string connectionString, string cInvCode, string cBatch, out string errMsg)
        {
            return new DAL.LabelPrint().GetLabelSerialNumber(connectionString, cInvCode, cBatch, out errMsg);
        }

        /// <summary>
        /// 添加或修改标签流水号
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cBatch">批次</param>
        /// <param name="type">类型 0：添加,非0:修改</param>
        /// <param name="number">最新的流水号</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-17 20:11</remarks>
        [WebMethod(Description="修改小标签流水号")]
        public bool ModifySerialNumber(string connectionString, string cInvCode, string cBatch, int type, int number, out string errMsg)
        {
            return new DAL.LabelPrint().ModifySerialNumber(connectionString, cInvCode, cBatch, type, number, out errMsg);
        }

        #endregion

        #region 食品追溯
        
        /// <summary>
        /// 食品追溯
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cInvCode"></param>
        /// <param name="cBatch"></param>
        /// <param name="Number"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description="食品追溯")]
        public DataTable Trace(string connectionString, string cInvCode, string cBatch, string Number, out string errMsg)
        {
            return new FoodTrace().Trace(connectionString, cInvCode, cBatch, Number, out errMsg);
        }

        /// <summary>
        /// 查询供应商资质
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cVenCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description="供应商资质")]
        public DataTable VendorQuanlification(string connectionString, string cVenCode, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            string strSql = string.Format(@"SELECT vls.* FROM 
(SELECT id FROM V_pl_gmp_vendorlicenseaudit WHERE cVendorCode='{0}') vl
INNER JOIN (SELECT id,cLicenseCode,cLicenseName,cLicenseType,cLicenseNum,dValidDate,dEndDate FROM V_pl_gmp_vendorlicenseaudits) vls ON vl.id = vls.id", cVenCode);
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

        #endregion

        #region 仓库盘点

        /// <summary>
        /// 查询盘点单列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-02</remarks>
        [WebMethod(Description="查询盘点单列表")]
        public DataTable SelectCheckVouchList(string connectionString, out string errMsg)
        {
            return new DAL.CheckStock().SelectCheckVouchList(connectionString, out errMsg);
        }

        /// <summary>
        /// 根据盘点单、存货编码、批次查询盘点信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="checkVouchs"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-03</remarks>
        [WebMethod(Description = "根据盘点单、存货编码、批次查询盘点信息")]
        public bool SelectCheckVouch(string connectionString, ref CheckVouchs checkVouchs, out string errMsg)
        {
            return new DAL.CheckStock().SelectCheckVouch(connectionString, ref checkVouchs, out errMsg);
        }


        /// <summary>
        /// 保存盘点列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="list"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-03</remarks>
        [WebMethod(Description = "保存盘点列表")]
        public bool SaveCheckVouch(string connectionString, List<CheckVouchs> list, out string errMsg)
        {
            return new DAL.CheckStock().SaveCheckVouch(connectionString, list, out errMsg);
        }
        #endregion
    }
}
