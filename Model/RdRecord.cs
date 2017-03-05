using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Model
{
    /// <summary>
    /// 库存主表
    /// </summary>
    public class RdRecord
    {
        #region 以下是必输字段 
        
        /// <summary>
        /// 收发记录主表标识
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string cCode { get; set; }
        /// <summary>
        /// 出库日期/单据日期 
        /// </summary>
        public DateTime dDate { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string cBusType { get; set; }

        /// <summary>
        /// 工作流审批状态
        /// </summary>
        public int iverifystate { get; set; }
        /// <summary>
        /// 是否工作流控制 
        /// </summary>
        public int iswfcontrolled { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string cCusCode { get; set; }
        /// <summary>
        /// 客户简称 
        /// </summary>
        public string cCusAbbName { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string cMaker { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string ufts { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string cVouchType { get; set; }
        /// <summary>
        /// 单据类名
        /// </summary>
        public string cVouchName { get; set; }
        /// <summary>
        /// 单据来源
        /// </summary>
        public string cSource { get; set; }
        /// <summary>
        /// 收发标志
        /// </summary>
        public int bRdFlag { get; set; }

        /// <summary>
        /// 库存期初标记 
        /// </summary>
        public bool bIsSTQc { get; set; }
        #endregion

        #region 以下是非必输字段
        /// <summary>
        /// 修改人 
        /// </summary>
        public string cModifyPerson { get; set; }

        /// <summary>
        /// 修改日期 
        /// </summary>
        public DateTime dModifyDate { get; set; }
        /// <summary>
        /// 制单时间 
        /// </summary>
        public DateTime dnmaketime { get; set; }
        /// <summary>
        /// 修改时间 
        /// </summary>
        public DateTime dnmodifytime { get; set; }
        /// <summary>
        /// 审核时间 
        /// </summary>
        public DateTime dnverifytime { get; set; }
         /// <summary>
        /// 收发类别编码 
        /// </summary>
        public string cRdCode { get; set; }
        /// <summary>
        /// 出库类别
        /// </summary>
        public string cRdName { get; set; }
        /// <summary>
        /// 打回次数
        /// </summary>
        public int ireturncount { get; set; }
        /// <summary>
        /// 对应业务单号 
        /// </summary>
        public string cBusCode { get; set; }
        /// <summary>
        /// 销售部门
        /// </summary>
        public string cDepName{get;set;}
        /// <summary>
        /// 业务员编码 
        /// </summary>
        public string cPersonCode { get; set; }
        /// <summary>
        /// 业务员名称
        /// </summary>
        public string cPersonName { get; set; }
        /// <summary>
        /// 审核日期 
        /// </summary>
        public DateTime dVeriDate { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string cMemo { get; set; }
        /// <summary>
        /// 审核人 
        /// </summary>
        public string cHandler { get; set; }
        /// <summary>
        /// 记账人 
        /// </summary>
        public string cAccounter { get; set; }
        /// <summary>
        /// 现存量
        /// </summary>
        public double ipresent { get; set; }

        /// <summary>
        /// 客户联系人 
        /// </summary>
        public string ccusperson { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ccusphone { get; set; }
        /// <summary>
        /// 客户手机
        /// </summary>
        public string ccushand { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string ccusaddress { get; set; }
        /// <summary>
        /// 客户联系人电话
        /// </summary>
        public string contactphone { get; set; }
        /// <summary>
        /// 客户联系人手机
        /// </summary>
        public string contactmobile { get; set; }
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

        /// <summary>
        /// 业务员电话
        /// </summary>
        public string cpsnophone { get; set; }
        /// <summary>
        /// 业务员手机
        /// </summary>
        public string cpsnmobilephone { get; set; }

        /// <summary>
        /// 销售类型编码
        /// </summary>
        public string cSTCode { get; set; }
        /// <summary>
        /// 销售类型名称
        /// </summary>
        public string cSTName { get; set; }
        /// <summary>
        /// 销售部门编码
        /// </summary>
        public string cDepCode { get; set; }

        /// <summary>
        /// 发货单ID
        /// </summary>
        public int DLID { get; set; }
        /// <summary>
        /// 发货退货单号 
        /// </summary>
        public string cDLCode { get; set; }
        /// <summary>
        /// 供应商简称 
        /// </summary>
        public string cVenAbbName { get; set; }
        /// <summary>
        /// 发票主表标识
        /// </summary>
        public int cBillCode { get; set; }

        /// <summary>
        /// 入库单供应商编码
        /// </summary>
        public string cVenCode { get; set; }
        /// <summary>
        /// 可用量
        /// </summary>
        public double iAvaQuantity { get; set; }
        /// <summary>
        /// 可用件数
        /// </summary>
        public double iAvaNum { get; set; }
        /// <summary>
        /// 现存件数
        /// </summary>
        public double iPresentNum { get; set; }
        /// <summary>
        /// 是否自检
        /// </summary>
        public string gspcheck { get; set; }
        /// <summary>
        /// 发票号 
        /// </summary>
        public string isalebillid { get; set; }
        /// <summary>
        /// 发货退货单号 
        /// </summary>
        public string iarriveid { get; set; }
        /// <summary>
        /// 检验日期 
        /// </summary>
        public DateTime dChkDate { get; set; }
        /// <summary>
        /// 检验员
        /// </summary>
        public string cChkPerson { get; set; }
        /// <summary>
        /// 单据模版号 
        /// </summary>
        public int iVTid { get; set; }
        /// <summary>
        /// 发货地址编码
        /// </summary>
        public string cAddCode { get; set; }
        /// <summary>
        /// 发往地址 
        /// </summary>
        public string cShipAddress { get; set; }
        /// <summary>
        /// 检验单号
        /// </summary>
        public string cChkCode { get; set; }
        /// <summary>
        /// 最低库存量
        /// </summary>
        public double iLowSum { get; set; }
        /// <summary>
        /// 安全库存量
        /// </summary>
        public double iSafeSum { get; set; }
        /// <summary>
        /// 最高库存量
        /// </summary>
        public double iTopSum { get; set; }
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
        #endregion

        private List<RdRecords> list = new List<RdRecords>();
        /// <summary>
        /// 子表集合
        /// </summary>
        public List<RdRecords> List
        {
            get { return list; }
            set { list = value; }
        }
    }
}
