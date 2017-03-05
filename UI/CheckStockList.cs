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
    public partial class CheckStockList : Form
    {

        private List<Receipt> list;

        public CheckStockList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 盘点单列表加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckStockList_Load(object sender, EventArgs e)
        {
            string errMsg;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                list = new BLL.CheckStock().SelectCheckVouchList(out errMsg);
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
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "盘点单";
            dtbc.MappingName = "Code";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "仓库名称";
            dtbc.MappingName = "Name";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "盘点日期";
            dtbc.MappingName = "dDate";
            dtbc.Format = "yyyy-MM-dd";
            dtbc.Width = 80;
            dts.GridColumnStyles.Add(dtbc);

            dgView.TableStyles.Add(dts);
            dgView.RowHeadersVisible = true;
            dts.MappingName = list.GetType().Name;
            dgView.DataSource = list;
        }

        /// <summary>
        /// 点击确定按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (list == null || list.Count == 0)
                return;
            int index = dgView.CurrentRowIndex;
            if (index < 0)
            {
                MessageBox.Show("请选择行!");
                return;
            }
            Receipt receipt = list[index];
            CheckStock form = new CheckStock(receipt.Code, receipt.Name);
            form.ShowDialog();
        }

        /// <summary>
        /// 点击返回按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }  
    }
}