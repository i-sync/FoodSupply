using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace LabelPrint
{
    public class PrintBusiness
    {
        /// <summary>
        /// 根据存货名称或编码查询存货信息
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="cInvName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable SearchInventory(string cInvCode, string cInvName, out string errMsg)
        {
            //List<Inventory> list = null;
            //查询存货列表
            DataTable dt = Common.Instance.Service.SearchInventory(Common.Instance.ConnectionString, Common.Instance.WHList, cInvCode, cInvName, out errMsg);
            //if (dt == null)
            //    return list;
            ////循环封装
            //list = new List<Inventory>();
            //Inventory inventory;
            //foreach (DataRow row in dt.Rows)
            //{
            //    inventory = new Inventory();
            //    inventory.cInvCode = row["cInvCode"].ToString();
            //    inventory.cInvName = row["cInvName"].ToString();
            //    inventory.cInvStd = row["cInvStd"].ToString();
            //    inventory.cMassUnit= Cast.ToInteger(row["cMassUnit"]);
            //    inventory.iMassDate = Cast.ToInteger(row["iMassDate"]);

            //    inventory.cBatch = Cast.ToString(row["cBatch"]);
            //    inventory.dMDate = Cast.ToDateTime(row["dMDate"]);
            //    inventory.iQuantity = (int)Cast.ToDouble(row["iQuantity"]);
            //    list.Add(inventory);
            //}
            return dt;
        }

        /// <summary>
        /// 查询采购到货单列表
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<Receipt> SelectPuchaseList(out string errMsg)
        {
            List<Receipt> list = null;
            DataTable dt = Common.Instance.Service.SelectPurchaseList(Common.Instance.ConnectionString,Common.Instance.WHList, out errMsg);
            if (dt == null)
                return list;
            list = new List<Receipt>();
            Receipt receipt;
            foreach (DataRow row in dt.Rows)
            {
                receipt = new Receipt();
                receipt.Code = Cast.ToString(row["ccode"]);//采购到货单号
                receipt.ShowCode = Cast.ToString(row["ccode"]);//采购到货单号
                receipt.dDate = Cast.ToDateTime(row["ddate"]);//日期
                receipt.Name = Cast.ToString(row["cvenabbname"]);//客户简称
                list.Add(receipt);
            }

            return list;
        }
        /// <summary>
        /// 根据采购到货单查询存货编码
        /// </summary>
        /// <param name="cCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-18</remarks>
        public DataTable SearchInventoryByPUArrival(string cCode, out string errMsg)
        {
            //List<Inventory> list = null;
            //查询存货列表
            DataTable dt = Common.Instance.Service.SearchInventoryByPUArrival(Common.Instance.ConnectionString, cCode, out errMsg);
            //if (dt == null)
            //    return list;
            ////循环封装
            //list = new List<Inventory>();
            //Inventory inventory;
            //foreach (DataRow row in dt.Rows)
            //{
            //    inventory = new Inventory();
            //    inventory.cInvCode = row["cInvCode"].ToString();
            //    inventory.cInvName = row["cInvName"].ToString();
            //    inventory.cInvStd = row["cInvStd"].ToString();
            //    inventory.cMassUnit = Cast.ToInteger(row["cMassUnit"]);
            //    inventory.iMassDate = Cast.ToInteger(row["iMassDate"]);

            //    inventory.cBatch = Cast.ToString(row["cBatch"]);
            //    inventory.dMDate = Cast.ToDateTime(row["dMDate"]);
            //    inventory.iQuantity = (int)Cast.ToDouble(row["iQuantity"]);
            //    list.Add(inventory);
            //}

            return dt;
        }

        /// <summary>
        /// 查询销售发货单列表
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<Receipt> SelectSaleDeliveryList(out string errMsg)
        {
            List<Receipt> list = null;
            DataTable dt = Common.Instance.Service.SelectSaleDeliveryList(Common.Instance.ConnectionString,Common.Instance.WHList, out errMsg);
            if (dt == null)
                return list;
            list = new List<Receipt>();
            Receipt receipt;
            foreach (DataRow row in dt.Rows)
            {
                receipt = new Receipt();
                receipt.Code = Cast.ToString(row["cDLCode"]);//采购到货单号
                receipt.ShowCode = string.IsNullOrEmpty(Cast.ToString(row["cSBVCode"])) ? Cast.ToString(row["cDLCode"]) : Cast.ToString(row["cSBVCode"]);
                receipt.dDate = Cast.ToDateTime(row["dDate"]);//日期
                receipt.Name = Cast.ToString(row["cCusAbbName"]);//供应商
                list.Add(receipt);
            }

            return list;
        }

        /// <summary>
        /// 根据销售发货单查询存货编码
        /// </summary>
        /// <param name="cDLCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-06-19</remarks>
        public DataTable SearchInventoryBySaleDelivery(string cDLCode, out string errMsg)
        {
            //List<Inventory> list = null;
            //查询存货列表
            DataTable dt = Common.Instance.Service.SearchInventoryBySaleDelivery(Common.Instance.ConnectionString, cDLCode, out errMsg);
            //if (dt == null)
            //    return list;
            ////循环封装
            //list = new List<Inventory>();
            //Inventory inventory;
            //foreach (DataRow row in dt.Rows)
            //{
            //    inventory = new Inventory();
            //    inventory.cInvCode = row["cInvCode"].ToString();
            //    inventory.cInvName = row["cInvName"].ToString();
            //    inventory.cInvStd = row["cInvStd"].ToString();
            //    inventory.cMassUnit = Cast.ToInteger(row["cMassUnit"]);
            //    inventory.iMassDate = Cast.ToInteger(row["iMassDate"]);

            //    inventory.cBatch = Cast.ToString(row["cBatch"]);
            //    inventory.dMDate = Cast.ToDateTime(row["dMDate"]);
            //    inventory.iQuantity = (int)Cast.ToDouble(row["iQuantity"]);
            //    list.Add(inventory);
            //}

            return dt;
        }
    }
}
