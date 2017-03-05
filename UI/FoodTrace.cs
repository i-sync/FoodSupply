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
    public partial class FoodTrace : Form
    {
        /// <summary>
        /// QR码
        /// </summary>
        private string[] barcode;

        public FoodTrace()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FoodTrace_Load(object sender, EventArgs e)
        {
            Clear();
            
            //判断用户是否为主管，若为主管则显示供应商信息
            if(BLL.Common.Competence.Admin)
                panel.Top += 30;           
        }

        /// <summary>
        /// 输入条码后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strBarCode = txtBarCode.Text.Trim();
            if (!string.IsNullOrEmpty(strBarCode) && e.KeyChar == (char)Keys.Enter)
            {
                Clear();
                
                if (strBarCode.IndexOf(Cast.Delimiter) < 0)
                {
                    MessageBox.Show("条码错误， 不包含分格符！");
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
                    string errMsg;
                    Cursor.Current = Cursors.WaitCursor;
                    //查询信息
                    DataTable dt = new BLL.FoodTrace().Trace(barcode[0], barcode[1], barcode[2], out errMsg);
                    if (dt == null)
                    {
                        MessageBox.Show("对不起，没有查询到该商品信息！" + errMsg);
                        txtBarCode.Focus();
                        txtBarCode.SelectAll();
                        return;
                    }
                    //MessageBox.Show("aa");

                    //绑定显示食品信息
                    DataRow row = dt.Rows[0];
                    //采购信息
                    lblInvName.Text = Cast.ToString(row["cInvName"]);
                    lblInvStd.Text = Cast.ToString(row["cInvStd"]);
                    lblcBatch.Text = Cast.ToString(row["cBatch"]);
                    //供应商编码与名称
                    lblcVenAbbName.Tag = row["cVenCode"];
                    lblcVenAbbName.Text = Cast.ToString(row["cVenAbbName"]);
                    lbliAQuantity.Text = string.Format("{0} {1}", Cast.ToDouble(row["iAQuantity"]).ToString("F2"), row["cComunitName"]);
                    lbldMDate.Text = Cast.ToDateTime(row["dMDate"]).ToString("yyyy-MM-dd");
                    lbliMassDate.Text = string.Format("{0} {1}",row["iMassDate"],row["cMassUnit"]);
                    //销售信息
                    lblcCusAbbName.Text = row["cCusAbbName"] == DBNull.Value ? "暂无" : Cast.ToString(row["cCusAbbName"]);
                    lblcCode.Text = row["cBusCode"] == DBNull.Value ? "暂无" : Cast.ToString(row["cBusCode"]);  //row["iarriveid"] == DBNull.Value ? "暂无" : Cast.ToString(row["iarriveid"]);
                    lblcMaker.Text = row["cMaker"] == DBNull.Value ? "暂无" : Cast.ToString(row["cMaker"]);
                    lbldDate.Text = row["dDate"] == DBNull.Value ? "暂无" : Cast.ToDateTime(row["dDate"]).ToString("yyyy-MM-dd");
                    lbliSQuantity.Text = row["iSQuantity"] == DBNull.Value ? "暂无" : Cast.ToString(row["iSQuantity"]);

                    //
                    txtBarCode.Focus();
                    txtBarCode.SelectAll();
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
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            lblInvName.Text = string.Empty;
            lblInvStd.Text = string.Empty;
            lblcBatch.Text = string.Empty;
            lblcVenAbbName.Text = string.Empty;
            lbliAQuantity.Text = string.Empty;
            lbldMDate.Text = string.Empty;
            lbliMassDate.Text = string.Empty;
            lblcCusAbbName.Text = string.Empty;
            lblcCode.Text = string.Empty;
            lblcMaker.Text = string.Empty;
            lbldDate.Text = string.Empty;
            lbliSQuantity.Text = string.Empty;
        }

        /// <summary>
        /// 供应商资质信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblcVenAbbName_Click(object sender, EventArgs e)
        {
            if (lblcVenAbbName.Tag == null)
                return;
            VendorQualification vqForm = new VendorQualification(lblcVenAbbName.Tag.ToString());
            vqForm.ShowDialog();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}