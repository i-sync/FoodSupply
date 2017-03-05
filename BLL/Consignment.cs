using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace BLL
{
    /// <summary>
    /// 销售管理.销售发货
    /// </summary>
    public class Consignment
    {
        /// <summary>
        /// 销售发货：查询发货单列表
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<Receipt> SelectList(out string errMsg)
        {
            List<Receipt> list = null;
            //转换仓库列表集合对象
            BLL.Service.Warehouse[] whList = new BLL.Service.Warehouse[Common.WarehouseList.Count];
            BLL.Service.Warehouse tWh;//目标对象
            int i =0;
            foreach (Warehouse wh in Common.WarehouseList)
            {
                tWh = new BLL.Service.Warehouse();
                EntityConvert.ConvertClass<Warehouse, BLL.Service.Warehouse>(wh, tWh);
                whList[i++] = tWh;
            }

            DataTable dt = Common.Instance.Service.SelectSaleDeliveryList(Common.Instance.User.ConnectionString, whList, out errMsg);
            if (dt == null)
                return list;
            list = new List<Receipt>();
            Receipt receipt;
            foreach (DataRow row in dt.Rows)
            {
                receipt = new Receipt();
                /*
                 * tianzhenyun
                 * 2013-11-18   update 
                 * 根据客户要求如果是直销，终端上的发货单列表显示发票号（即零售日报号）
                 * **/
                receipt.Code = Cast.ToString(row["cDLCode"]);
                //判断发票号是否空，如果为空则显示发货单号，否则为发票号（即零售日报号）
                receipt.ShowCode = string.IsNullOrEmpty(Cast.ToString(row["cSBVCode"])) ? Cast.ToString(row["cDLCode"]) : Cast.ToString(row["cSBVCode"]);//采购到货单号
                receipt.dDate = Cast.ToDateTime(row["dDate"]);//日期
                receipt.Name = Cast.ToString(row["cCusAbbName"]);//供应商
                list.Add(receipt);
            }
            return list;
        }

        /// <summary>
        /// 销售发货：单据装载
        /// </summary>
        /// <param name="cDLCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DispatchList Load(string cDLCode, out string errMsg)
        {
            //来源主表
            BLL.Service.DispatchList sMain = Common.Instance.Service.SA_Consignment_Load(Common.Instance.User.ConnectionString, cDLCode, out errMsg);
            if (sMain == null)
                return null;

            //目标主表
            DispatchList tMain = new DispatchList();
            DispatchLists tChild;
            //转换主表
            EntityConvert.ConvertClass<BLL.Service.DispatchList, DispatchList>(sMain, tMain);
            //循环转换子表
            foreach (BLL.Service.DispatchLists sChild in sMain.List)
            {
                tChild = new DispatchLists();
                EntityConvert.ConvertClass<BLL.Service.DispatchLists, DispatchLists>(sChild, tChild);
                ////存货转换
                //tChild.Inventory = new Inventory();
                //EntityConvert.ConvertClass<BLL.Service.Inventory, Inventory>(sChild.Inventory, tChild.Inventory);
                tMain.List.Add(tChild);
            }
            return tMain;
        }
    }
}
