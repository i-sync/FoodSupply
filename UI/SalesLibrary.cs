using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using BLL;

namespace UI
{
    public partial class SalesLibrary : Form
    {
        /// <summary>
        /// 销售发货单主表
        /// </summary>
        private DispatchList dispatchList;
        /// <summary>
        /// 销售出库主表
        /// </summary>
        private RdRecord rdRecord = new RdRecord();
        /// <summary>
        /// 扫描存货条码
        /// </summary>
        private string[] barcode;

        /// <summary>
        /// 扫描存货对象
        /// </summary>
        private DispatchLists dispatchLists;

        /// <summary>
        /// 发货单号
        /// </summary>
        private string cDLCode;
        /// <summary>
        /// 显示的单据号
        /// </summary>
        private string showCode;
        /// <summary>
        /// 客户名称
        /// </summary>
        private string cCusName;

        public SalesLibrary()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 有参构造函数（发货单号）
        /// </summary>
        /// <param name="cDLCode"></param>
        public SalesLibrary(string cDLCode,string showCode ,string cCusName)
            : this()
        {
            this.cDLCode = cDLCode;
            this.showCode = showCode;
            this.cCusName = cCusName;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesLibrary_Load(object sender, EventArgs e)
        {
            txtDelivery.Text = showCode;
            lblcCusName.Text = cCusName;
            Clear();
            string errMsg;
            try
            {
                //显示等待
                Cursor.Current = Cursors.WaitCursor;
                dispatchList = new BLL.Consignment().Load(cDLCode, out errMsg);
                if (dispatchList != null)
                {
                    //把销售发货对象转换成销售出库对象
                    rdRecord = EntityConvert.ConvertToRdrecord(dispatchList);
                    btnSource.Enabled = true;

                    txtBarcode.Focus();
                }
                else
                {
                    MessageBox.Show(errMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 点击查看来源单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSource_Click(object sender, EventArgs e)
        {
            if (dispatchList != null && dispatchList.List.Count > 0)
            {
                //过滤已扫描完成的存货（不在显示）
                IEnumerable<DispatchLists> list = dispatchList.List.Where(dls => Math.Abs(dls.iQuantity)-Math.Abs(dls.fOutQuantity) > Math.Abs(dls.iScanQuantity));
                SourceForm sourceForm = new SourceForm(list.ToList());
                sourceForm.ShowDialog();
            }
        }

        /// <summary>
        /// 输入标签后回来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strBarcode = txtBarcode.Text.Trim();
            ///如果不为空
            if (!string.IsNullOrEmpty(strBarcode) && e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    ///分离二维码
                    barcode = strBarcode.Split(Cast.Delimiter);
                    //判断当前条码是否在来源单据中
                    dispatchLists = dispatchList.List.Find(delegate(DispatchLists temp) { return temp.cInvCode.Equals(barcode[0]) && temp.cBatch.Equals(barcode[1]); });
                    if (dispatchLists == null)
                    {
                        MessageBox.Show("条码错误:存货编码批次不正确！");
                        txtBarcode.Focus();
                        txtBarcode.SelectAll();
                        return;
                    }
                    //判断该标签是否已经扫描过（排除同个存货多次扫描）
                    RdRecords rds = rdRecord.List.Find(
                        delegate(RdRecords temp)
                        {
                            return temp.cInvCode.Equals(barcode[0]) && temp.cBatch.Equals(barcode[1]);
                        });
                    if (rds != null)//如果不为空，判断标签序列号
                    {                        
                        if (rds.SerialList.Contains(barcode[2]))
                        {
                            MessageBox.Show("条码错误:该存货已扫描！");
                            txtBarcode.Focus();
                            txtBarcode.SelectAll();
                            return;
                        }
                    }

                    //显示信息
                    lblInvName.Text = dispatchLists.cInvName;
                    lblInvStd.Text = dispatchLists.cInvStd;
                    lblCWhName.Text = dispatchLists.cWhName;
                    //lblEnterprise.Text = details.e
                    lblProDate.Text = dispatchLists.dMDate == DateTime.MinValue ? "" : dispatchLists.dMDate.ToString("yyyy-MM-dd");
                    lblValidDate.Text = dispatchLists.dVDate == DateTime.MinValue ? "" : dispatchLists.dVDate.AddDays(-1).ToString("yyyy-MM-dd");
                    lblBatch.Text = dispatchLists.cBatch;
                    //dispatchLists.cBarCode = barcode[0];

                    //显示已扫描数量
                    lblScanedNum.Text = dispatchLists.iScanQuantity.ToString();

                    txtCount.Text = (dispatchLists.iQuantity / Math.Abs(dispatchLists.iQuantity)).ToString(); ;
                    txtCount.Focus();
                    txtCount.SelectAll();

                    //如果选择单品,则实现自动扫描
                    if (chkSingle.Checked)
                    {
                        KeyPressEventArgs args = new KeyPressEventArgs((char)Keys.Enter);
                        txtCount_KeyPress(null, args);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 输入数量后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strCount = txtCount.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(strCount))
            {
                //判断数量是否正确
                double quantity = Cast.ToDouble(strCount);
                if (quantity == 0)
                {
                    MessageBox.Show("请输入正确的数量！");
                    txtCount.SelectAll();
                    txtCount.Focus();
                    return;
                }
                //判断是发货单还是退货单breturnflag
                if (dispatchList.bReturnFlag)//退货，数量不能为正
                {
                    if (quantity > 0)
                    {
                        MessageBox.Show("退货数量不能为正！");
                        txtCount.SelectAll();
                        txtCount.Focus();
                        return;
                    }
                }
                else //发货，数量不能为负
                {
                    if (quantity < 0)
                    {
                        MessageBox.Show("发货数量不能为负！");
                        txtCount.SelectAll();
                        txtCount.Focus();
                        return;
                    }
                }
                //如何录入数量大于订货数量－累计发货数量－已扫描数量，则返回
                if (Math.Abs(quantity) > Math.Abs(dispatchLists.iQuantity) - Math.Abs(dispatchLists.fOutQuantity) - Math.Abs(dispatchLists.iScanQuantity))
                {
                    MessageBox.Show("输入数量大于应发货数量");
                    txtCount.SelectAll();
                    txtCount.Focus();
                    return;
                }

                //判断已扫数据中是否存在(存货编号、批次)
                RdRecords tempRdRecords = rdRecord.List.Find(delegate(RdRecords temp) { return temp.cInvCode.Equals(barcode[0]) && temp.cBatch.Equals(barcode[1]); });
                ///不存在
                if (tempRdRecords == null)
                {
                    ///累加扫描数量
                    dispatchLists.iScanQuantity += quantity;
                    tempRdRecords = EntityConvert.ConvertToRdrecords(dispatchLists);

                    tempRdRecords.iScanQuantity = quantity;
                    tempRdRecords.iUnitCost = dispatchLists.iTaxUnitPrice;//原币含税单价
                    tempRdRecords.iPrice = tempRdRecords.iScanQuantity * tempRdRecords.iUnitCost;//原币价税合计

                    tempRdRecords.SerialList.Add(barcode[2]);//存储扫描的序列号
                    rdRecord.List.Add(tempRdRecords);

                    btnDone.Enabled = true;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    ///累加扫描数量
                    dispatchLists.iScanQuantity += quantity;
                    tempRdRecords.iScanQuantity += quantity;

                    tempRdRecords.iPrice = tempRdRecords.iScanQuantity * tempRdRecords.iUnitCost;//原币价税合计
                    tempRdRecords.SerialList.Add(barcode[2]);//存储扫描的序列号
                }

                Clear();//清空数据

                txtBarcode.Focus();
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            txtBarcode.Text = string.Empty;
            txtCount.Text = string.Empty;

            lblInvName.Text = string.Empty;
            lblInvStd.Text = string.Empty;
            lblProDate.Text = string.Empty;
            lblValidDate.Text = string.Empty;
            lblScanedNum.Text = string.Empty;
            lblBatch.Text = string.Empty;
            lblCWhName.Text = string.Empty;
        }

        /// <summary>
        /// 已扫数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (rdRecord.List.Count > 0)
            {
                DoneForm<RdRecords, DispatchLists> doneForm = new DoneForm<RdRecords, DispatchLists>(rdRecord.List,dispatchList.List);
                doneForm.ShowDialog();
            }
        }

        /// <summary>
        /// 点击提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rdRecord.List.Count == 0)
            {
                return;
            }
            //判断是否扫描完成，若未完成提示是继续提交
            bool flag = true;//标识是否扫描完成
            if (dispatchList.List.Count > rdRecord.List.Count)
            {
                flag = false;
            }
            else
            {
                //循环遍历所有的发货单存货
                foreach (DispatchLists dispatchLists in dispatchList.List)
                {
                    double num = 0d;//某存货已扫描数量
                    foreach (RdRecords rdRecords in rdRecord.List)
                    {
                        if (rdRecords.cInvCode == dispatchLists.cInvCode)
                            num += rdRecords.iScanQuantity;
                    }
                    //判断该存货是否已完整扫描
                    if (num == 0 || Math.Abs(num) < Math.Abs(dispatchLists.iQuantity) - Math.Abs(dispatchLists.fOutQuantity))
                    {
                        flag = false;
                        break;//只要有一个存货没有扫描完成，即可跳出
                    }
                }
            }

            if (!flag)
            {
                DialogResult dr = MessageBox.Show("还有存货没有扫描完成，是否提交", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)//取消提交
                {
                    return;
                }
            }

            //提交数据
            string vouchID = string.Empty;
            string errMsg = string.Empty ;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //根据仓库分组，分别生成销售出库单
                IEnumerable<IGrouping<string,RdRecords>> iList = rdRecord.List.GroupBy<RdRecords, string>(delegate (RdRecords rds) { return rds.cWhCode; });
                //int count = iList.Count();
                //循环分组数据，分别提交
                foreach (IGrouping<string, RdRecords> ig in iList)
                {
                    rdRecord.List= ig.ToList<RdRecords>();
                    //单据时间
                    rdRecord.dDate = MainForm.OperateTime;
                    ///保存销售出库单
                    flag = new BLL.SaleOut().Save(rdRecord, out errMsg);
                    if (!flag)//失败跳出循环
                        break;
                }
                if (flag)
                {
                    MessageBox.Show("保存成功！");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("保存失败：" + errMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 点击退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            //首先判断是否已扫描数据，如何有扫描数据提示用户是否确定退出
            if (rdRecord != null && rdRecord.List != null && rdRecord.List.Count > 0)
            {
                DialogResult dr = MessageBox.Show("您还有未提交的数据，确定要退出吗？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    return;
                }
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}