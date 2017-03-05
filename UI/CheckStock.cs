using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;

namespace UI
{
    public partial class CheckStock : Form
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        private string cCVCode;
        /// <summary>
        /// 仓库名称
        /// </summary>
        private string cWhName;
        /// <summary>
        /// QR码
        /// </summary>
        private string[] barcode;
        /// <summary>
        /// 扫描对象
        /// </summary>
        private CheckVouchs checkVouchs;
        /// <summary>
        /// 扫描列表
        /// </summary>
        private List<CheckVouchs> list= new List<CheckVouchs> ();

        public CheckStock()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="cCVCode"></param>
        /// <param name="cWhName"></param>
        public CheckStock(string cCVCode, string cWhName)
            : this()
        {
            this.cCVCode = cCVCode;
            this.cWhName = cWhName;
        }

        /// <summary>
        /// 盘点窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckStock_Load(object sender, EventArgs e)
        {
            //
            txtcCVCode.Text = this.cCVCode;
            lblcWhName.Text = this.cWhName;

            Clear();
        }

        /// <summary>
        /// 扫描条码回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strBarCode = txtBarCode.Text.Trim();
            if (!string.IsNullOrEmpty(strBarCode) && e.KeyChar == (char)Keys.Enter)
            {
                //判断条码中是否包含分隔符
                if (strBarCode.IndexOf(Cast.Delimiter) < 0)
                { 
                    MessageBox.Show("条码错误,没有分隔符！");
                    txtBarCode.Focus();
                    txtBarCode.SelectAll();
                    return;
                }
                //解析QR码
                barcode = strBarCode.Split(Cast.Delimiter);
                //判断条码是否正确
                if (barcode.Length < 4)
                {
                    MessageBox.Show("条码错误！");
                    txtBarCode.Focus();
                    txtBarCode.SelectAll();
                    return;
                }

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string errMsg;
                    //封装数据
                    checkVouchs = new CheckVouchs();
                    checkVouchs.cCVCode = cCVCode;
                    checkVouchs.cInvCode = barcode[0];
                    checkVouchs.cCVBatch = barcode[1];
                    //查询数据
                    bool flag = new BLL.CheckStock().SelectCheckVouch(ref checkVouchs, out errMsg);
                    if (!flag)
                    {
                        MessageBox.Show(errMsg);
                        txtBarCode.Focus();
                        txtBarCode.SelectAll();
                        return;
                    }
                    //绑定显示
                    lblInvName.Text = checkVouchs.cInvName;
                    lblInvStd.Text = checkVouchs.cInvStd;
                    lblcBatch.Text = checkVouchs.cCVBatch;
                    lbldMDate.Text = checkVouchs.dMadeDate.ToShortDateString();
                    lbldVDate.Text = checkVouchs.dDisDate.ToShortDateString();
                    lblQuantity.Text = checkVouchs.iCVQuantity.ToString();

                    //盘点数据获取焦点
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
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
        }

        /// <summary>
        /// 录入盘点数据回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            //获取界面数据
            string strQuantity = txtQuantity.Text.Trim();
            if(string.IsNullOrEmpty(strQuantity) || e.KeyChar != (char)Keys.Enter )
                return;
            double iQuantity;
            try
            {
                iQuantity = Convert.ToDouble(strQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                return;
            }

            //首先判断已扫集合中是否已包含
            CheckVouchs cv = list.Find(
                delegate(CheckVouchs tch)
                {
                    return tch.cInvCode == barcode[0] && tch.cCVBatch == barcode[1];
                });
            //没有找到
            if (cv == null)
            {
                checkVouchs.iCVCQuantity = iQuantity;
                list.Add(checkVouchs);
            }
            else
                cv.iCVCQuantity = iQuantity;//覆盖

            Clear();
            txtBarCode.Focus();

            //如果不可用修改
            if (!btnDetail.Enabled)
            {
                btnDetail.Enabled =btnSubmit.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetail_Click(object sender, EventArgs e)
        {
            ///如果已扫描数据
            if (list.Count > 0)
            {
                CheckStockDetail form = new CheckStockDetail(list);
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 点击提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ///如果已扫描数据
            if (list.Count > 0)
            {
                try
                {
                    string errMsg;
                    Cursor.Current = Cursors.WaitCursor;
                    bool flag = new BLL.CheckStock().SaveCheckVouch(list, out errMsg);
                    if (!flag)
                    {
                        MessageBox.Show("保存失败：" + errMsg);
                    }
                    else
                    {
                        MessageBox.Show("保存成功！");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("失败：" + ex.Message);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            txtBarCode.Text = string.Empty;
            lblInvName.Text = string.Empty;
            lblInvStd.Text = string.Empty;
            lblcBatch.Text = string.Empty;
            lbldMDate.Text = string.Empty;
            lbldVDate.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            lblQuantity.Text = string.Empty;
        }

        /// <summary>
        /// 点击退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            ///如果已扫描数据
            if (list.Count > 0)
            {
                DialogResult dr = MessageBox.Show("您还有未提交的数据，确定要退出吗？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

    }
}