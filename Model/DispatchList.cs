using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 发货退货单主表
    /// </summary>
    [Serializable]
    public class DispatchList
    {
        #region Model
       
        /// <summary>
        /// 单据类型编号
        /// </summary>
        public string cVouchType { get; set; }
        /// <summary>
        /// 单据类型名称
        /// </summary>
        public string cVouchName { get; set; }
        /// <summary>
        /// 发货退货单主表标识
        /// </summary>
        public int DLID { get; set; }
        /// <summary>
        /// 发货退货单号 
        /// </summary>
        public string cDLCode { get; set; }
        /// <summary>
        /// 销售类型编码
        /// </summary>
        public string cSTCode { get; set; }
        /// <summary>
        /// 销售类型
        /// </summary>
        public string cSTName { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName { get; set; }

        /// <summary>
        /// 单据日期 
        /// </summary>
        public DateTime dDate { get; set; }
        /// <summary>
        /// 收发类别编码 
        /// </summary>
        public string cRdCode { get; set; }
        /// <summary>
        /// 部门编码 
        /// </summary>
        public string cDepCode { get; set; }
        /// <summary>
        /// 业务员编码 
        /// </summary>
        public string cPersonCode { get; set; }

        /// <summary>
        /// 业务员名称
        /// </summary>
        public string cPersonName { get; set; }
        /// <summary>
        /// 销售发票主表标识 
        /// </summary>
        public int SBVID { get; set; }
        /// <summary>
        /// 销售发票号
        /// </summary>
        public string cSBVCode { get; set; }
        /// <summary>
        /// 销售订单号 
        /// </summary>
        public string cSOCode { get; set; }
        /// <summary>
        /// 客户编码 
        /// </summary>
        public string cCusCode { get; set; }
        /// <summary>
        /// 付款条件编码 
        /// </summary>
        public string cPayCode { get; set; }
        /// <summary>
        ///   发运方式编码  
        /// </summary>
        public string cSCCode { get; set; }
        /// <summary>
        /// 发往地址 
        /// </summary>
        public string cShipAddress { get; set; }
        /// <summary>
        /// 币种名称 
        /// </summary>
        public string cexch_name { get; set; }
        /// <summary>
        /// 汇率 
        /// </summary>
        public double iExchRate { get; set; }
        /// <summary>
        /// 表头税率 
        /// </summary>
        public double iTaxRate { get; set; }
        /// <summary>
        /// 销售期初标志
        /// </summary>
        public bool bFirst { get; set; }
        /// <summary>
        /// 退货标志
        /// </summary>
        public bool bReturnFlag { get; set; }
        /// <summary>
        /// 结算标志 
        /// </summary>
        public bool bSettleAll { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string cMemo { get; set; }
        /// <summary>
        /// 出库单据号字符串
        /// </summary>
        public string cSaleOut { get; set; }
        /// <summary>
        /// 自定义项1 
        /// </summary>
        public string cDefine1 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string cDefine2 { get; set; }
        /// <summary>
        /// 自定义项3
        /// </summary>
        public string cDefine3 { get; set; }
        /// <summary>
        /// 自定义项4
        /// </summary>
        public DateTime cDefine4 { get; set; }
        /// <summary>
        /// 自定义项5
        /// </summary>
        public int cDefine5 { get; set; }
        /// <summary>
        /// 自定义项6
        /// </summary>
        public DateTime cDefine6 { get; set; }
        /// <summary>
        /// 自定义项7
        /// </summary>
        public double cDefine7 { get; set; }
        /// <summary>
        /// 自定义项8
        /// </summary>
        public string cDefine8 { get; set; }
        /// <summary>
        /// 自定义项9
        /// </summary>
        public string cDefine9 { get; set; }
        /// <summary>
        /// 自定义项10
        /// </summary>
        public string cDefine10 { get; set; }
        /// <summary>
        /// 审核人 
        /// </summary>
        public string cVerifier { get; set; }
        /// <summary>
        /// 制单人  
        /// </summary>
        public string cMaker { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double iNetLock { get; set; }
        /// <summary>
        /// 是否先发货
        /// </summary>
        public int iSale { get; set; }
        /// <summary>
        /// 客户名称 
        /// </summary>
        public string cCusName { get; set; }
        /// <summary>
        /// 单据模版号 
        /// </summary>
        public int iVTid { get; set; }
        /// <summary>
        /// 时间戳 
        /// </summary>
        public string ufts { get; set; }
        /// <summary>
        /// 业务类型 
        /// </summary>
        public string cBusType { get; set; }
        /// <summary>
        /// 关闭人 
        /// </summary>
        public string cCloser { get; set; }
        /// <summary>
        /// 记账人 
        /// </summary>
        public string cAccounter { get; set; }
        /// <summary>
        /// 信用审核人
        /// </summary>
        public string cCreChpName { get; set; }
        /// <summary>
        /// 自定义项11
        /// </summary>
        public string cDefine11 { get; set; }
        /// <summary>
        /// 自定义项12
        /// </summary>
        public string cDefine12 { get; set; }
        /// <summary>
        /// 自定义项13
        /// </summary>
        public string cDefine13 { get; set; }
        /// <summary>
        /// 自定义项14
        /// </summary>
        public string cDefine14 { get; set; }
        /// <summary>
        /// 自定义项15
        /// </summary>
        public int cDefine15 { get; set; }
        /// <summary>
        /// 自定义项16
        /// </summary>
        public double cDefine16 { get; set; }
        /// <summary>
        /// 存货期初标志 
        /// </summary>
        public bool bIAFirst { get; set; }
        /// <summary>
        /// 金税导出次数 
        /// </summary>
        public int ioutgolden { get; set; }
        /// <summary>
        /// 收款单号 
        /// </summary>
        public string cgatheringplan { get; set; }
        /// <summary>
        /// 立账日 
        /// </summary>
        public DateTime dCreditStart { get; set; }
        /// <summary>
        /// 到期日 
        /// </summary>
        public DateTime dGatheringDate { get; set; }
        /// <summary>
        /// 账期 
        /// </summary>
        public int icreditdays { get; set; }
        /// <summary>
        /// 是否立账单据
        /// </summary>
        public bool bCredit { get; set; }
        /// <summary>
        /// 发货地址编码
        /// </summary>
        public string caddcode { get; set; }
        /// <summary>
        /// 审核状态 
        /// </summary>
        public int iverifystate { get; set; }
        /// <summary>
        /// 打回次数 
        /// </summary>
        public int ireturncount { get; set; }
        /// <summary>
        ///   启用工作流 
        /// </summary>
        public int iswfcontrolled { get; set; }
        /// <summary>
        /// 信用审批状态 
        /// </summary>
        public string icreditstate { get; set; }
        /// <summary>
        /// 应收期初 
        /// </summary>
        public bool bARFirst { get; set; }
        /// <summary>
        /// 修改人 
        /// </summary>
        public string cmodifier { get; set; }
        /// <summary>
        /// 修改日期 
        /// </summary>
        public DateTime dmoddate { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime dverifydate { get; set; }
        /// <summary>
        /// 客户联系人 
        /// </summary>
        public string ccusperson { get; set; }
        /// <summary>
        /// 制单时间 
        /// </summary>
        public DateTime dcreatesystime { get; set; }
        /// <summary>
        /// 审核时间 
        /// </summary>
        public DateTime dverifysystime { get; set; }
        /// <summary>
        /// 修改时间 
        /// </summary>
        public DateTime dmodifysystime { get; set; }
        /// <summary>
        /// 来源单据类型 
        /// </summary>
        public string csvouchtype { get; set; }
        /// <summary>
        /// 流程id 
        /// </summary>
        public int iflowid { get; set; }
        /// <summary>
        /// 签回损失生成 
        /// </summary>
        public bool bsigncreate { get; set; }
        /// <summary>
        /// 现款结算 
        /// </summary>
        public bool bcashsale { get; set; }
        /// <summary>
        /// 收款单号 
        /// </summary>
        public string cgathingcode { get; set; }
        /// <summary>
        /// 变更人 
        /// </summary>
        public string cChanger { get; set; }
        /// <summary>
        /// 变更原因 
        /// </summary>
        public string cChangeMemo { get; set; }
        /// <summary>
        /// outid
        /// </summary>
        public string outid { get; set; }
        /// <summary>
        /// 客户简称
        /// </summary>
        public string cCusAbbName { get; set; }
        /// <summary>
        /// 销售部门
        /// </summary>
        public string cDepName { get; set; }

        /// <summary>
        /// 收货单位
        /// </summary>
        public string cdeliverunit { get; set; }

        /// <summary>
        /// 收货联系人
        /// </summary>
        public string cContactName { get; set; }
        /// <summary>
        /// 收货联系电话
        /// </summary>
        public string cofficephone { get; set; }
        /// <summary>
        /// 收货联系人手机
        /// </summary>
        public string cmobilephone { get; set; }

        private List<DispatchLists> list = new List<DispatchLists>();
        /// <summary>
        /// 发货退货单子表集合
        /// </summary>
        public List<DispatchLists> List
        {
            get { return list; }
            set { list = value; }
        }
        #endregion Model

    }
}
