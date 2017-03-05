using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 盘点单子表
    /// </summary>
    public class CheckVouchs
    {
        /// <summary>
        /// 盘点单
        /// </summary>
        public string cCVCode
        {
            get;
            set;
        }
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode
        {
            get;
            set;
        }
        /// <summary>
        /// 存货名称
        /// </summary>
        public string cInvName
        {
            get;
            set;
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string cCVBatch
        {
            get;
            set;
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string cInvStd
        {
            get;
            set;
        }


        /// <summary>
        /// 账面数量
        /// </summary>
        public double iCVQuantity
        {
            get;
            set;
        }
        
        /// <summary>
        /// 盘点数量
        /// </summary>
        public double iCVCQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 盈亏数量
        /// </summary>
        public double cAlcQuantity
        {
            get
            {
                return iCVCQuantity - iCVQuantity;
            }
        }
        /// <summary>
        /// 保质期数量
        /// </summary>
        public int iMassDate
        {
            get;
            set;
        }
        /// <summary>
        /// 保质期单位
        /// </summary>
        public string cMassUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dMadeDate
        {
            get;
            set;
        }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime dDisDate
        {
            get;
            set;
        }
    }
}
