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
    public partial class CheckStockDetail : Form
    {
        /// <summary>
        /// 数据源
        /// </summary>
        private List<CheckVouchs> list;
        public CheckStockDetail()
        {
            InitializeComponent();
        }

        public CheckStockDetail(List<CheckVouchs> list)
            : this()
        {
            this.list = list;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckStockDetail_Load(object sender, EventArgs e)
        {
            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cInvCode";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "cInvName";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格";
            dtbc.MappingName = "cInvStd";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批次";
            dtbc.MappingName = "cbatch";
            dtbc.Width = 110;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "dMadeDate";
            dtbc.Width = 90;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "失效日期";
            dtbc.MappingName = "dvdate";
            dtbc.Width = 90;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "账面数量";
            dtbc.MappingName = "iCVQuantity";
            dtbc.Format = "F2";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "盘点数量";
            dtbc.MappingName = "iCVCQuantity";
            dtbc.Format = "F2";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "盈亏数量";
            dtbc.MappingName = "cAlcQuantity";
            dtbc.Format = "F2";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dgView.TableStyles.Add(dts);
            dgView.RowHeadersVisible = true;
            dts.MappingName = list.GetType().Name;
            this.dgView.DataSource = list;
        }

        /// <summary>
        /// 点击删除行按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //没有要操作的数据
                if (list.Count < 1)
                    return;

                int index = dgView.CurrentRowIndex;
                if (index < 0)
                {
                    MessageBox.Show("请选择要删除的行！");
                    return;
                }
                DialogResult dr = MessageBox.Show("您确定要删除此行吗？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    dgView.DataSource = null;
                    list.RemoveAt(index);
                    dgView.DataSource = list;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 点击退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}