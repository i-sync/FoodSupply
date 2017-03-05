using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Model;

namespace UI
{
    public partial class ConsignmentList : Form
    {
        public ConsignmentList()
        {
            InitializeComponent();
        }
        private List<Receipt> list = new List<Receipt> ();
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsignmentList_Load(object sender, EventArgs e)
        {            
            //加载DataGrid样式
            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "发货单号";
            dtbc.MappingName = "Code";
            dtbc.Width = 0;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "单据号";
            dtbc.MappingName = "ShowCode";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "客户简称";
            dtbc.MappingName = "Name";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "发货日期";
            dtbc.MappingName = "dDate";
            dtbc.Format = "yyyy-MM-dd";
            dtbc.Width = 80;
            dts.GridColumnStyles.Add(dtbc);

            dgView.TableStyles.Add(dts);
            dgView.RowHeadersVisible = true;
            dts.MappingName = list.GetType().Name;

            DataBinding();
        }

        /// <summary>
        /// 绑定数据列表
        /// </summary>
        private void DataBinding()
        {
            string errMsg;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                list = new BLL.Consignment().SelectList(out errMsg);
                if (list == null)
                {
                    MessageBox.Show(errMsg);
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


            dgView.DataSource = list;
        }
        /// <summary>
        /// 点击确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (list == null || list.Count ==0)
                return;
            int index = dgView.CurrentRowIndex;
            if (index < 0)
            {
                MessageBox.Show("请选择行!");
                return;
            }
            Receipt receipt = list[index];
            SalesLibrary salesLibraryForm = new SalesLibrary(receipt.Code, receipt.ShowCode, receipt.Name);
            DialogResult dr = salesLibraryForm.ShowDialog();
            if (dr == DialogResult.OK)
            { 
                //刷新列表
                DataBinding();
            }
        }

        private void dgView_DoubleClick(object sender, EventArgs e)
        {
            if(dgView.CurrentCell.ColumnNumber==-1)
                btnSure_Click(null, null);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}