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
    public partial class VendorQualification : Form
    {
        private string cVendor;
        public VendorQualification()
        {
            InitializeComponent();
        }

        public VendorQualification(string cVendor)
            : this()
        {
            this.cVendor = cVendor;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VendorQualification_Load(object sender, EventArgs e)
        {
            //读取数据
            string errMsg;
            List<VendorLicense> list = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                list = new BLL.FoodTrace().VendorQuanlification(this.cVendor, out errMsg);
                if (list == null)
                {
                    MessageBox.Show(errMsg);
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "证照类型";
            dtbc.MappingName = "cLicenseType";
            dtbc.Width = 65;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "资料编码";
            dtbc.MappingName = "cLicenseCode";
            dtbc.Width = 65;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "资料名称";
            dtbc.MappingName = "cLicenseName";
            dtbc.Width = 135;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "证照号码";
            dtbc.MappingName = "cLicenseNum";
            dtbc.Width = 125;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生效日期";
            dtbc.MappingName = "dValidDate";
            dtbc.Width = 80;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "证照到期日";
            dtbc.MappingName = "dEndDate";
            dtbc.Width = 80;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dgView.TableStyles.Add(dts);
            dgView.RowHeadersVisible = true;
            dts.MappingName = list.GetType().Name;
            this.dgView.DataSource = list;
        }

        /// <summary>
        /// 点击返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}