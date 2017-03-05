using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace BLL
{
    /// <summary>
    /// 仓库盘点
    /// </summary>
    public class CheckStock
    {
        /// <summary>
        /// 查询盘点单列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-02</remarks>
        public List<Receipt> SelectCheckVouchList( out string errMsg)
        {
            List<Receipt> list = null;
            //查询盘点列表
            DataTable dt = Common.Instance.Service.SelectCheckVouchList(Common.Instance.User.ConnectionString, out errMsg);
            if (dt == null || dt.Rows.Count == 0)
            {
                errMsg = "没有要盘点的单据！";
                return list;
            }
            //循环封装
            list = new List<Receipt>();
            Receipt receipt;
            foreach (DataRow row in dt.Rows)
            {
                receipt = new Receipt();
                receipt.Code = Cast.ToString(row["cCVCode"]);
                receipt.Name = Cast.ToString(row["cWhName"]);
                receipt.dDate = Cast.ToDateTime(row["dCVDate"]);
                list.Add(receipt);
            }
            //返回结果
            return list;
        }

        /// <summary>
        /// 根据盘点单、存货编码、批次查询盘点信息
        /// </summary>
        /// <param name="checkVouchs"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-03</remarks>
        public bool SelectCheckVouch(ref CheckVouchs checkVouchs, out string errMsg)
        {
            BLL.Service.CheckVouchs tCheckVouchs = new BLL.Service.CheckVouchs();
            tCheckVouchs = EntityConvert.ConvertClass<CheckVouchs, BLL.Service.CheckVouchs>(checkVouchs, tCheckVouchs);

            bool flag = BLL.Common.Instance.Service.SelectCheckVouch(Common.Instance.User.ConnectionString, ref tCheckVouchs, out errMsg);
            if (flag)//如果为真，则转换对象
                checkVouchs = EntityConvert.ConvertClass<BLL.Service.CheckVouchs, CheckVouchs>(tCheckVouchs, checkVouchs);
            return flag;
        }

        /// <summary>
        /// 保存盘点数量
        /// </summary>
        /// <param name="list"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-07-03</remarks>
        public bool SaveCheckVouch(List<CheckVouchs> list, out string errMsg)
        { 
            //循环转换集合列表对象
            BLL.Service.CheckVouchs[] cvArray = new BLL.Service.CheckVouchs[list.Count];
            int i = 0;
            //目标对象
            BLL.Service.CheckVouchs tcv;
            foreach (CheckVouchs cv in list)
            {
                tcv = new BLL.Service.CheckVouchs();
                EntityConvert.ConvertClass<CheckVouchs, BLL.Service.CheckVouchs>(cv, tcv);
                cvArray[i++] = tcv;
            }
            //保存盘点数据
            return Common.Instance.Service.SaveCheckVouch(Common.Instance.User.ConnectionString, cvArray, out errMsg);
        }
    }
}
