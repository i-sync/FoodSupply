using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 发货退货单子表
    /// </summary>
    [Serializable]
    public class DispatchLists
    {
        #region Model
        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public int AutoID { get; set; }
        /// <summary>
        /// 发货退货单主表标识 
        /// </summary>
        public int DLID { get; set; }
        /// <summary>
        /// 发货退货单号 
        /// </summary>
        public string cDLCode{get;set;}

        /// <summary>
        /// 原发货退货单子表标识
        /// </summary>
        public int iCorID { get; set; }
        /// <summary>
        /// 仓库编码 
        /// </summary>
        public string cWhCode { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName { get; set; }
        /// <summary>
        /// 条码码
        /// </summary>
        public string cBarCode { get; set; }
        /// <summary>
        /// 存货编码 
        /// </summary>
        public string cInvCode { get; set; }
        /// <summary>
        /// 数量 
        /// </summary>
        public double iQuantity { get; set; }

        /// <summary>
        /// 扫描数量
        /// </summary>
        public double iScanQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// 辅计量数量 
        /// </summary>
        public double iNum { get; set; }
        /// <summary>
        /// 报价 
        /// </summary>
        public double iQuotedPrice { get; set; }
        /// <summary>
        /// 原币无税单价 
        /// </summary>
        public double iUnitPrice { get; set; }
        /// <summary>
        /// 原币含税单价 
        /// </summary>
        public double iTaxUnitPrice { get; set; }
        /// <summary>
        /// 原币无税金额 
        /// </summary>
        public double iMoney { get; set; }
        /// <summary>
        /// 原币税额 
        /// </summary>
        public double iTax { get; set; }
        /// <summary>
        /// 原币价税合计
        /// </summary>
        public double iSum { get; set; }
        /// <summary>
        /// 原币折扣额 
        /// </summary>
        public double iDisCount { get; set; }
        /// <summary>
        /// 本币无税单价 
        /// </summary>
        public double iNatUnitPrice { get; set; }
        /// <summary>
        /// 本币无税金额 
        /// </summary>
        public double iNatMoney { get; set; }
        /// <summary>
        /// 本币税额 
        /// </summary>
        public double iNatTax { get; set; }
        /// <summary>
        /// 本币价税合计 
        /// </summary>
        public double iNatSum { get; set; }
        /// <summary>
        /// 本币折扣额 
        /// </summary>
        public double iNatDisCount { get; set; }
        /// <summary>
        /// 累计结算金额 
        /// </summary>
        public double iSettleNum { get; set; }
        /// <summary>
        /// 开票数量 
        /// </summary>
        public double iSettleQuantity { get; set; }
        /// <summary>
        /// 批次 
        /// </summary>
        public int iBatch { get; set; }
        /// <summary>
        /// 批号 
        /// </summary>
        public string cBatch { get; set; }
        /// <summary>
        /// 结算标志 
        /// </summary>
        public bool bSettleAll { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string cMemo { get; set; }
        /// <summary>
        /// 存货自由项1 
        /// </summary>
        public string cFree1 { get; set; }
        /// <summary>
        /// 存货自由项2
        /// </summary>
        public string cFree2 { get; set; }
        /// <summary>
        /// 退补标志 
        /// </summary>
        public int iTB { get; set; }
        /// <summary>
        /// 失效日期 
        /// </summary>
        public DateTime dVDate { get; set; }
        /// <summary>
        /// 退补数量 
        /// </summary>
        public double TBQuantity { get; set; }
        /// <summary>
        /// 退补辅计量数量 
        /// </summary>
        public double TBNum { get; set; }
        /// <summary>
        /// 销售订单子表标识
        /// </summary>
        public int iSOsID { get; set; }
        /// <summary>
        /// 发货退货单子表标识2
        /// </summary>
        public int iDLsID { get; set; }
        /// <summary>
        /// 扣率 
        /// </summary>
        public double KL { get; set; }
        /// <summary>
        /// 二次扣率 
        /// </summary>
        public double KL2 { get; set; }
        /// <summary>
        /// 存货名称 
        /// </summary>
        public string cInvName { get; set; }
        /// <summary>
        /// 税率 
        /// </summary>
        public double iTaxRate { get; set; }
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
        /// 累计出库数量 
        /// </summary>
        public double fOutQuantity { get; set; }
        /// <summary>
        /// 累计出库辅计量数量
        /// </summary>
        public double fOutNum { get; set; }
        /// <summary>
        /// 项目编码 
        /// </summary>
        public string cItemCode { get; set; }
        /// <summary>
        /// 项目大类编码 
        /// </summary>
        public string cItem_class { get; set; }
        /// <summary>
        /// 零售单价 
        /// </summary>
        public double fSaleCost { get; set; }
        /// <summary>
        /// 零售金额 
        /// </summary>
        public double fSalePrice { get; set; }
        /// <summary>
        /// 供应商简称 
        /// </summary>
        public string cVenAbbName { get; set; }
        /// <summary>
        /// 项目名称 
        /// </summary>
        public string cItemName { get; set; }
        /// <summary>
        /// 项目大类名称 
        /// </summary>
        public string cItem_CName { get; set; }
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
        /// 库存期初标记 
        /// </summary>
        public bool bIsSTQc { get; set; }
        /// <summary>
        /// 换算率 
        /// </summary>
        public double iInvExchRate { get; set; }
        /// <summary>
        /// 计量单位编码 
        /// </summary>
        public string cUnitID { get; set; }
        /// <summary>
        /// 出库单据号      
        /// </summary>
        public string cCode { get; set; }
        /// <summary>
        /// 退货数量
        /// </summary>
        public double iRetQuantity { get; set; }
        /// <summary>
        /// 委托结算数量
        /// </summary>
        public double fEnSettleQuan { get; set; }
        /// <summary>
        /// 委托结算辅计量数量
        /// </summary>
        public double fEnSettleSum { get; set; }
        /// <summary>
        /// 结算单价 
        /// </summary>
        public double iSettlePrice { get; set; }
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
        public DateTime cDefine37 { get; set; }
        /// <summary>
        /// 生产日期 
        /// </summary>
        public DateTime dMDate { get; set; }
        /// <summary>
        /// 是否质检 
        /// </summary>
        public bool bGsp { get; set; }
        /// <summary>
        /// 是否质检 
        /// </summary>
        public string cGspState { get; set; }
        /// <summary>
        /// 销售订单号 
        /// </summary>
        public string cSoCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cCorCode { get; set; }
        /// <summary>
        /// PTO母件顺序号 
        /// </summary>
        public int iPPartSeqID { get; set; }
        /// <summary>
        /// 母件物料ID
        /// </summary>
        public int iPPartID { get; set; }
        /// <summary>
        /// 母件物料ID
        /// </summary>
        public double iPPartQty { get; set; }
        /// <summary>
        /// 合同号 
        /// </summary>
        public string cContractID { get; set; }
        /// <summary>
        /// 合同标的 
        /// </summary>
        public string cContractTagCode { get; set; }
        /// <summary>
        /// 合同cGuid 
        /// </summary>
        public string cContractRowGuid { get; set; }
        /// <summary>
        /// 保质期 
        /// </summary>
        public int iMassDate { get; set; }
        /// <summary>
        /// 保质期单位 
        /// </summary>
        public int cMassUnit { get; set; }
        /// <summary>
        /// 是否质检 
        /// </summary>
        public bool bQANeedCheck { get; set; }
        /// <summary>
        /// 是否急料 
        /// </summary>
        public bool bQAUrgency { get; set; }
        /// <summary>
        /// 是否在检 
        /// </summary>
        public bool bQAChecking { get; set; }
        /// <summary>
        /// 是否报检 
        /// </summary>
        public bool bQAChecked { get; set; }
        /// <summary>
        /// 检验合格数量
        /// </summary>
        public double iQAQuantity { get; set; }
        /// <summary>
        /// 检验合格件数
        /// </summary>
        public double iQANum { get; set; }
        /// <summary>
        /// 客户存货编码
        /// </summary>
        public string cCusInvCode { get; set; }
        /// <summary>
        /// 客户存货名称
        /// </summary>
        public string cCusInvName { get; set; }
        /// <summary>
        /// 累计签回数量
        /// </summary>
        public double fsumsignquantity { get; set; }
        /// <summary>
        /// 签回件数 
        /// </summary>
        public double fsumsignnum { get; set; }
        /// <summary>
        /// 记账人
        /// </summary>
        public string cbaccounter { get; set; }
        /// <summary>
        /// 是否记账 
        /// </summary>
        public bool bCosting { get; set; }
        /// <summary>
        /// 订单号 
        /// </summary>
        public string cordercode { get; set; }
        /// <summary>
        /// 订单行号 
        /// </summary>
        public long iorderrowno { get; set; }
        /// <summary>
        /// 客户最低售价 
        /// </summary>
        public double fcusminprice { get; set; }
        /// <summary>
        /// 成本数量
        /// </summary>
        public double icostquantity { get; set; }
        /// <summary>
        /// 成本金额 
        /// </summary>
        public double icostsum { get; set; }
        /// <summary>
        /// 特价类型 
        /// </summary>
        public int ispecialtype { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string cvmivencode { get; set; }
        /// <summary>
        /// 原币收款金额 
        /// </summary>
        public double iexchsum { get; set; }
        /// <summary>
        /// 本币收款金额 
        /// </summary>
        public double imoneysum { get; set; }
        /// <summary>
        /// 行号 
        /// </summary>
        public long irowno { get; set; }
        /// <summary>
        /// 退补退货数量 
        /// </summary>
        public double frettbquantity { get; set; }
        /// <summary>
        /// 退货金额 
        /// </summary>
        public double fretsum { get; set; }
        /// <summary>
        /// 有效期推算方式 
        /// </summary>
        public int iExpiratDateCalcu { get; set; }
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public DateTime dExpirationdate { get; set; }
        /// <summary>
        /// 有效期至 
        /// </summary>
        public string cExpirationdate { get; set; }
        /// <summary>
        /// 批次属性1 
        /// </summary>
        public double cBatchProperty1 { get; set; }
        /// <summary>
        /// 批次属性2
        /// </summary>
        public double cBatchProperty2 { get; set; }
        /// <summary>
        /// 批次属性3
        /// </summary>
        public double cBatchProperty3 { get; set; }
        /// <summary>
        /// 批次属性4
        /// </summary>
        public double cBatchProperty4 { get; set; }
        /// <summary>
        /// 批次属性5
        /// </summary>
        public double cBatchProperty5 { get; set; }
        /// <summary>
        /// 批次属性6
        /// </summary>
        public string cBatchProperty6 { get; set; }
        /// <summary>
        /// 批次属性7
        /// </summary>
        public string cBatchProperty7 { get; set; }
        /// <summary>
        /// 批次属性8
        /// </summary>
        public string cBatchProperty8 { get; set; }
        /// <summary>
        /// 批次属性9
        /// </summary>
        public string cBatchProperty9 { get; set; }
        /// <summary>
        /// 批次属性10
        /// </summary>
        public DateTime cBatchProperty10 { get; set; }
        /// <summary>
        /// 预收款原币金额 
        /// </summary>
        public double dblPreExchMomey { get; set; }
        /// <summary>
        /// 预收款本币金额 
        /// </summary>
        public double dblPreMomey { get; set; }
        /// <summary>
        /// 需求跟踪方式 
        /// </summary>
        public int idemandtype { get; set; }
        /// <summary>
        /// 需求分类代号 
        /// </summary>
        public string cdemandcode { get; set; }
        /// <summary>
        /// 需求分类说明 
        /// </summary>
        public string cdemandmemo { get; set; }
        /// <summary>
        /// 需求跟踪id 
        /// </summary>
        public string cdemandid { get; set; }
        /// <summary>
        /// 需求跟踪行号 
        /// </summary>
        public long idemandseq { get; set; }
        /// <summary>
        /// 入库单供应商编码
        /// </summary>
        public string cvencode { get; set; }
        /// <summary>
        /// 退货原因编码 
        /// </summary>
        public string cReasonCode { get; set; }
        /// <summary>
        /// 序列号 
        /// </summary>
        public string cInvSN { get; set; }
        /// <summary>
        /// 序列号个数 
        /// </summary>
        public int iInvSNCount { get; set; }
        /// <summary>
        /// 需要签回 
        /// </summary>
        public bool bneedsign { get; set; }
        /// <summary>
        /// 发货签回完成 
        /// </summary>
        public bool bsignover { get; set; }
        /// <summary>
        /// 需要损失处理 
        /// </summary>
        public bool bneedloss { get; set; }
        /// <summary>
        /// 合理损耗率
        /// </summary>
        public double flossrate { get; set; }
        /// <summary>
        /// 合理损耗数量 
        /// </summary>
        public double frlossqty { get; set; }
        /// <summary>
        /// 非合理损耗数量 
        /// </summary>
        public double fulossqty { get; set; }
        /// <summary>
        /// 责任承担处理
        /// </summary>
        public int isettletype { get; set; }
        /// <summary>
        /// 责任客户编码 
        /// </summary>
        public string crelacuscode { get; set; }
        /// <summary>
        /// 损失处理人 
        /// </summary>
        public string cLossMaker { get; set; }
        /// <summary>
        /// 损失处理日期
        /// </summary>
        public DateTime dLossDate { get; set; }
        /// <summary>
        /// 损失处理时间 
        /// </summary>
        public DateTime dLossTime { get; set; }
        /// <summary>
        /// 来源发货单子表ID 
        /// </summary>
        public long icoridlsid { get; set; }
        /// <summary>
        /// 退货出库数量
        /// </summary>
        public double fretoutqty { get; set; }
        /// <summary>
        /// body_outid
        /// </summary>
        public string body_outid { get; set; }
        /// <summary>
        /// 货位
        /// </summary>
        public string cPosition { get; set; }


        /// <summary>
        /// 编辑属性：A表新增，M表修改，D表删除，
        /// </summary>
        public string editprop { get; set; }
        
        /// <summary>
        /// 规格型号
        /// </summary>
        public string cInvStd{get;set;}
        /// <summary>
        /// 主计量单位
        /// </summary>
        public string cinvm_unit { get; set; }

        /// <summary>
        /// 计量单位组
        /// </summary>
        public string cGroupCode { get; set; }

        /// <summary>
        /// 存货自定义项1
        /// </summary>
        public string cInvDefine1 { get; set; }
        /// <summary>
        /// 存货自定义项6
        /// </summary>
        public string cInvDefine6 { get; set; }

        ///// <summary>
        ///// 存货对象
        ///// </summary>
        //public Inventory Inventory
        //{
        //    get;
        //    set;
        //}
        #endregion Model

    }
}
