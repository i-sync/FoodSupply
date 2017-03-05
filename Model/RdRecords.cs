using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 库存子表
    /// </summary>
    public class RdRecords
    {
        #region 以下是必输字段 
        
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public int AutoID { get; set; }

        /// <summary>
        /// 主计量单位
        /// </summary>
        public string cinvm_unit { get; set; }
        /// <summary>
        /// 数量 
        /// </summary>
        public double iQuantity { get; set; }
        /// <summary>
        /// 扫描数量
        /// </summary>
        public double iScanQuantity { get; set; }

        /// <summary>
        /// 存货编码 
        /// </summary>
        public string cInvCode { get; set; }

        /// <summary>
        /// 编辑属性：A表新增，M表修改，D表删除，
        /// </summary>
        public string editprop { get; set; }

        #endregion

        /// <summary>
        ///  税率
        /// </summary>
        public double iTaxRate { get; set; }


        /// <summary>
        /// 仓库编码 
        /// </summary>
        public string cWhCode { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName { get; set; }

        #region 以下是非必输字段
        /// <summary>
        /// 存货名称 
        /// </summary>
        public string cInvName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string cInvStd { get; set; }
        /// <summary>
        /// 存货代码
        /// </summary>
        public string cInvAddCode { get; set; }
        /// <summary>
        /// 对应入库单号
        /// </summary>
        public string cInVouchCode { get; set; }

        /// <summary>
        /// 销售订单子表ID  
        /// </summary>
        public string iSoDID { get; set; }
        /// <summary>
        /// 供应商编码 
        /// </summary>
        public string cBVencode { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string cVenName { get; set; }
        /// <summary>
        /// 保质期单位 
        /// </summary>
        public int cMassUnit { get; set; }
        /// <summary>
        /// 保质期 
        /// </summary>
        public int iMassDate { get; set; }

        /// <summary>
        /// 是否质检 
        /// </summary>
        public bool bGsp { get; set; }

        /// <summary>
        /// 检验状态 
        /// </summary>
        public string cGspState { get; set; }
        /// <summary>
        /// 辅计量单位编码 
        /// </summary>
        public string cAssUnit { get; set; }
        /// <summary>
        /// 货位
        /// </summary>
        public string cPosition { get; set; }
        /// <summary>
        /// 对应单据时间戳
        /// </summary>
        public string corufts { get; set; }
        /// <summary>
        /// 不合格品时间戳
        /// </summary>
        public string scrapufts { get; set; }
         /// <summary>
        /// //销售单位
        /// </summary>
        public string cinva_unit { get; set; }
        /// <summary>
        /// 销售订单号 
        /// </summary>
        public string csocode { get; set; }
        /// <summary>
        /// 代管商编码 
        /// </summary>
        public string cvmivencode { get; set; }
        /// <summary>
        /// 代管商
        /// </summary>
        public string cvmivenname { get; set; }
        /// <summary>
        /// 代管消耗标识
        /// </summary>
        public string bVMIUsed { get; set; }
        /// <summary>
        /// 代管结算数量 
        /// </summary>
        public double iVMISettleQuantity { get; set; }

        /// <summary>
        /// 代管结算件数
        /// </summary>
        public double iVMISettleNum { get; set; }
        /// <summary>
        /// 换算率
        /// </summary>
        public double iInvExchRate { get; set; }

        /// <summary>
        /// 对应入库单子表标识 
        /// </summary>
        public string cVouchCode { get; set; }
        /// <summary>
        /// 累计出库数量 
        /// </summary>
        public double iSOutQuantity { get; set; }
        /// <summary>
        /// 累计出库辅计量数量(件数)
        /// </summary>
        public double iSOutNum { get; set; }
        /// <summary>
        /// 应收应发数量  
        /// </summary>
        public double iNQuantity { get; set; }
        /// <summary>
        /// 计划金额或售价金额 
        /// </summary>
        public double iPPrice { get; set; }
        /// <summary>
        /// 发货单号 
        /// </summary>
        public string cbdlcode { get; set; }
        /// <summary>
        /// iordertype
        /// </summary>
        public int iordertype { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string iordercode { get; set; }
        /// <summary>
        /// 订单子表id 
        /// </summary>
        public int iorderdid { get; set; }
        /// <summary>
        /// 销售订单行号 
        /// </summary>
        public long iorderseq { get; set; }
        /// <summary>
        /// 有效期推算方式 
        /// </summary>
        public int iExpiratDateCalcu { get; set; }
        /// <summary>
        /// 有效期至 
        /// </summary>
        public string cExpirationdate { get; set; }
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public DateTime dExpirationdate { get; set; }
        /// <summary>
        /// 累计保税处理抽取数量  
        /// </summary>
        public double iBondedSumQty { get; set; }
        /// <summary>
        /// 合同标标识 
        /// </summary>
        public string strContractId { get; set; }
        /// <summary>
        /// 合同号 
        /// </summary>
        public string strCode { get; set; }

        /// <summary>
        /// 客户存货编码
        /// </summary>
        public string cCusInvCode { get; set; }

        /// <summary>
        /// 客户存货名称
        /// </summary>
        public string cCusInvName { get; set; }
        /// <summary>
        /// 记账人
        /// </summary>
        public string cbaccounter { get; set; }
        /// <summary>
        /// 单据是否核算　  
        /// </summary>
        public bool bCosting { get; set; }
        /// <summary>
        /// 订单类型  
        /// </summary>
        public int iSoType { get; set; }

        /// <summary>
        /// 可用量
        /// </summary>
        public double iAvaQuantity { get; set; }

        /// <summary>
        /// 可用件数
        /// </summary>
        public double iAvaNum { get; set; }
        /// <summary>
        /// 现存量
        /// </summary>
        public double iPresent { get; set; }
        /// <summary>
        /// 现存件数
        /// </summary>
        public double iPresentNum { get; set; }
        /// <summary>
        /// 销售订单行号 
        /// </summary>
        public int isoseq { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dMadeDate { get; set; }
        /// <summary>
        /// 失败日期
        /// </summary>
        public DateTime dVDate { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string cBarCode { get; set; }

        /// <summary>
        /// 表体自定义项22 
        /// </summary>
        public string cDefine22 { get; set; }
        /// <summary>
        /// 表体自定义项23 
        /// </summary>
        public string cDefine23 { get; set; }
        /// <summary>
        /// 表体自定义项24 
        /// </summary>
        public string cDefine24 { get; set; }
        /// <summary>
        /// 表体自定义项25 
        /// </summary>
        public string cDefine25 { get; set; }
        /// <summary>
        /// 表体自定义项26 
        /// </summary>
        public double cDefine26 { get; set; }
        /// <summary>
        /// 表体自定义项27 
        /// </summary>
        public double cDefine27 { get; set; }
        /// <summary>
        /// 表体自定义项28
        /// </summary>
        public string cDefine28 { get; set; }
        /// <summary>
        /// 表体自定义项29
        /// </summary>
        public string cDefine29 { get; set; }
        /// <summary>
        /// 表体自定义项30
        /// </summary>
        public string cDefine30 { get; set; }
        /// <summary>
        /// 表体自定义项31 
        /// </summary>
        public string cDefine31 { get; set; }
        /// <summary>
        /// 表体自定义项32 
        /// </summary>
        public string cDefine32 { get; set; }
        /// <summary>
        /// 表体自定义项33
        /// </summary>
        public string cDefine33 { get; set; }
        /// <summary>
        /// 表体自定义项34
        /// </summary>
        public int cDefine34 { get; set; }
        /// <summary>
        /// 表体自定义项35 
        /// </summary>
        public int cDefine35 { get; set; }
        /// <summary>
        /// 表体自定义项36
        /// </summary>
        public DateTime cDefine36 { get; set; }
        /// <summary>
        /// 表体自定义项37
        /// </summary>
        public DateTime  cDefine37 { get; set; }

        /// <summary>
        /// 存货自定义项1
        /// </summary>
        public string cInvDefine1 { get; set; }
        /// <summary>
        /// 存货自定义项2
        /// </summary>
        public string cInvDefine2 { get; set; }
        /// <summary>
        /// 存货自定义项3
        /// </summary>
        public string cInvDefine3 { get; set; }
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
        /// 存货自定义项10
        /// </summary>
        public string cInvDefine10 { get; set; }
        /// <summary>
        /// 存货自定义项11
        /// </summary>
        public string cInvDefine11 { get; set; }
        /// <summary>
        /// 存货自定义项12
        /// </summary>
        public string cInvDefine12 { get; set; }
        /// <summary>
        /// 存货自定义项13
        /// </summary>
        public string cInvDefine13 { get; set; }
        /// <summary>
        /// 存货自定义项14
        /// </summary>
        public string cInvDefine14 { get; set; }
        /// <summary>
        /// 存货自定义项15
        /// </summary>
        public string cInvDefine15 { get; set; }
        /// <summary>
        /// 存货自定义项16
        /// </summary>
        public string cInvDefine16 { get; set; }


        /// <summary>
        /// 替换件
        /// </summary>
        public string cReplaceItem { get; set; }
        /// <summary>
        /// 辅计量数量(件数)      
        /// </summary>
        public double iNum { get; set; }

        /// <summary>
        /// 应收应发辅计量数量
        /// </summary>
        public double iNNum { get; set; }
        /// <summary>
        /// 项目编码 
        /// </summary>
        public string cItemCode { get; set; }
        /// <summary>
        /// 库存调拨单子表标识
        /// </summary>
        public double iTrIds { get; set; }

        /// <summary>
        /// 项目大类编码 
        /// </summary>
        public string cItem_class { get; set; }
        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public int iDLsID { get; set; }
        /// <summary>
        /// 发票子表标识 
        /// </summary>
        public int iSBsID { get; set; }
        /// <summary>
        /// 本次发货数量 
        /// </summary>
        public double iSendQuantity { get; set; }
        /// <summary>
        /// 本次发货辅计量数量
        /// </summary>
        public double iSendNum { get; set; }
        /// <summary>
        /// 项目大类名称  
        /// </summary>
        public string cItemCName { get; set; }
        /// <summary>
        /// 委托代销发货单子表标识
        /// </summary>
        public int iEnsID { get; set; }
        /// <summary>
        /// 存货自由项1
        /// </summary>
        public string cFree1 { get; set; }
        /// <summary>
        /// 存货自由项2
        /// </summary>
        public string cFree2 { get; set; }
        /// <summary>
        /// 存货自由项3
        /// </summary>
        public string cFree3 { get; set; }
        /// <summary>
        /// 存货自由项4
        /// </summary>
        public string cFree4 { get; set; }
        /// <summary>
        /// 存货自由项5
        /// </summary>
        public string cFree5 { get; set; }
        /// <summary>
        /// 存货自由项6
        /// </summary>
        public string cFree6 { get; set; }
        /// <summary>
        /// 存货自由项7
        /// </summary>
        public string cFree7 { get; set; }
        /// <summary>
        /// 存货自由项8
        /// </summary>
        public string cFree8 { get; set; }
        /// <summary>
        /// 存货自由项9
        /// </summary>
        public string cFree9 { get; set; }
        /// <summary>
        /// 存货自由项10
        /// </summary>
        public string cFree10 { get; set; }

        /// <summary>
        /// 属性1
        /// </summary>
        public double cBatchProperty1 { get; set; }
        /// <summary>
        /// 属性2
        /// </summary>
        public double cBatchProperty2 { get; set; }
        /// <summary>
        /// 属性3
        /// </summary>
        public double cBatchProperty3 { get; set; }
        /// <summary>
        /// 属性4
        /// </summary>
        public double cBatchProperty4 { get; set; }
        /// <summary>
        /// 属性5
        /// </summary>
        public double cBatchProperty5 { get; set; }
        /// <summary>
        /// 属性6
        /// </summary>
        public string cBatchProperty6 { get; set; }
        /// <summary>
        /// 属性7
        /// </summary>
        public string cBatchProperty7 { get; set; }
        /// <summary>
        /// 属性8
        /// </summary>
        public string cBatchProperty8 { get; set; }
        /// <summary>
        /// 属性9
        /// </summary>
        public string cBatchProperty9 { get; set; }
        /// <summary>
        /// 属性10
        /// </summary>
        public DateTime cBatchProperty10 { get; set; }
        /// <summary>
        /// 生产订单子表标识    
        /// </summary>
        public int iMPoIds { get; set; }
        /// <summary>
        /// 检验单子表标识
        /// </summary>
        public int iCheckIds { get; set; }
        /// <summary>
        /// 单价(原币含税单价)
        /// </summary>
        public double iUnitCost { get; set; }
        /// <summary>
        /// 金额(原币价税合计)    
        /// </summary>
        public double iPrice { get; set; }
        /// <summary>
        /// 计划价或售价  
        /// </summary>
        public double iPUnitCost { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public double iGrossWeight { get; set; }
        /// <summary>
        /// 净重
        /// </summary>
        public double iNetWeight { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string cBatch { get; set; }
        /// <summary>
        /// 序列号数量  
        /// </summary>
        public int iInvSNCount { get; set; }
        ///// <summary>
        ///// 存货
        ///// </summary>
        //public Inventory Inventory { get; set; }

        #endregion

        /// <summary>
        /// 序列号
        /// </summary>
        public List<string> SerialList = new List<string>();
    }
}
