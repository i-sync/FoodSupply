using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Model
{
    /// <summary>
    /// 存货档案
    /// </summary>
    [Serializable]
    public class Inventory
    {
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode { get; set; }

        /// <summary>
        /// 存货名称
        /// </summary>
        public string cInvName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string cInvStd { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string cBatch { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dMDate { get; set; }

        /// <summary>
        /// 生产日期字符串
        /// </summary>
        public string dMDateShort
        {
            get
            {
                return dMDate.ToShortDateString();
            }
        }
        /// <summary>
        /// 打印数量
        /// </summary>
        public int iQuantity { get; set; }

        /// <summary>
        /// 存货大类编码 
        /// </summary>
        public string cInvCCode { get; set; }

        /// <summary>
        /// 供应商编码 
        /// </summary>
        public string cVenCode { get; set; }

        /// <summary>
        /// 替换件
        /// </summary>
        public string cReplaceItem { get; set; }

        /// <summary>
        /// 货位编码 
        /// </summary>
        public string cPosition { get; set; }

        /// <summary>
        /// 是否销售
        /// </summary>
        public bool bSale { get; set; }

        /// <summary>
        /// 是否外购 
        /// </summary>
        public bool bPurchase { get; set; }

        /// <summary>
        /// 是否自制 
        /// </summary>
        public bool bSelf { get; set; }

        /// <summary>
        /// 是否生产耗用
        /// </summary>
        public bool bComsume { get; set; }

        /// <summary>
        /// 税率 
        /// </summary>
        public double iTaxRate { get; set; }

        /// <summary>
        /// 参考售价 
        /// </summary>
        public double iInvSCost { get; set; }

        /// <summary>
        /// 最低售价
        /// </summary>
        public double iInvLSCost { get; set; }

        /// <summary>
        /// 最新成本
        /// </summary>
        public double iInvNCost { get; set; }

        /// <summary>
        /// 最低库存  
        /// </summary>
        public double iLowSum { get; set; }

        /// <summary>
        /// 是否保质期管理 
        /// </summary>
        public bool bInvQuality { get; set; }

        /// <summary>
        /// 是否批次管理
        /// </summary>
        public bool bInvBatch { get; set; }

        /// <summary>
        /// 是否受托代销
        /// </summary>
        public bool bInvEntrust { get; set; }

        /// <summary>
        /// 是否呆滞积压 
        /// </summary>
        public bool bInvOverStock { get; set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime dSDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cInvDefine1 { get; set; }

        /// <summary>
        /// 建档人  
        /// </summary>
        public string cCreatePerson { get; set; }

        /// <summary>
        /// 变更人
        /// </summary>
        public string cModifyPerson { get; set; }

        /// <summary>
        /// 变更日期 
        /// </summary>
        public DateTime dModifyDate { get; set; }

        /// <summary>
        /// 保质期天数 
        /// </summary>
        public int iMassDate { get; set; }

        /// <summary>
        /// 保质期单位
        /// </summary>
        public int cMassUnit { get; set; }

        /// <summary>
        /// 保持期单位名称
        /// </summary>
        public string cMassUnitName
        {
            get
            {
                string name = string.Empty;
                switch (cMassUnit)
                { 
                    case 1:
                        name = "年";
                        break;
                    case 2:
                        name = "月";
                        break;
                    case 3:
                        name = "日";
                        break;
                    default:
                        break;
                }
                return name;
            }
        }

        /// <summary>
        /// 保质期预警天数 
        /// </summary>
        public int iWarnDays { get; set; }

        /// <summary>
        /// 是否条形码管理
        /// </summary>
        public bool bBarCode { get; set; }

        /// <summary>
        /// 对应条形码编码 
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 存货自定义项4 
        /// </summary>
        public string cInvDefine4 { get; set; }

        /// <summary>
        /// 存货自定义项5
        /// </summary>
        public string cInvDefine5 { get; set; }
        /// <summary>
        /// 存货自定义项6 
        /// </summary>
        public string cInvDefine6 { get; set; }

        /// <summary>
        /// 存货自定义项7
        /// </summary>
        public string cInvDefine7 { get; set; }
        /// <summary>
        /// 存货自定义项8 
        /// </summary>
        public string cInvDefine8 { get; set; }

        /// <summary>
        /// 存货自定义项9
        /// </summary>
        public string cInvDefine9 { get; set; }


        /// <summary>
        /// 计量单位组类别
        /// </summary>
        public bool iGroupType { get; set; }

        /// <summary>
        /// 计量单位组编码 
        /// </summary>
        public string cGroupCode { get; set; }

        /// <summary>
        /// 主计量单位编码
        /// </summary>
        public string cComUnitCode { get; set; }

        /// <summary>
        /// 生产企业  
        /// </summary>
        public string cEnterprise { get; set; }

        /// <summary>
        /// 产地 
        /// </summary>
        public string cAddress { get; set; }

        /// <summary>
        /// 批准文号或注册证号
        /// </summary>
        public string cFile { get; set; }

        /// <summary>
        /// 注册商标 
        /// </summary>
        public string cLabel { get; set; }


        /// <summary>
        /// 通用名称
        /// </summary>
        public string cCurrencyName { get; set; }

        /// <summary>
        /// 是否质检
        /// </summary>
        public bool bPropertyCheck { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string cPreparationType { get; set; }


        /// <summary>
        /// 零售价格 
        /// </summary>
        public double fRetailPrice { get; set; }
    }
}
