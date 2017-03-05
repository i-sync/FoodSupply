using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class SourceForm : Form
    {
        /// <summary>
        /// 数据源
        /// </summary>
        private object source;
        public SourceForm()
        {
            InitializeComponent();
        }
        public SourceForm(object source)
            : this()
        {
            this.source = source;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceForm_Load(object sender, EventArgs e)
        {
            if (source == null)
                return;

            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cInvCode";
            dtbc.Width = 80;
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
            dtbc.MappingName = "cBatch";
            dtbc.Width = 110;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "iQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "累计发货数量";
            dtbc.MappingName = "iFHQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "累计出库数量";
            dtbc.MappingName = "fOutQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "已扫数量";
            dtbc.MappingName = "iScanQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            //dtbc = new DataGridTextBoxColumn();
            //dtbc.HeaderText = "产地";
            //dtbc.MappingName = "cDefine22";
            //dtbc.Width = 100;
            //dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "主计量单位";
            dtbc.MappingName = "cinvm_unit";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "含税单价";
            dtbc.MappingName = "iTaxUnitPrice";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            //dtbc = new DataGridTextBoxColumn();
            //dtbc.HeaderText = "客户名称";
            //dtbc.MappingName = "cCusName";
            //dtbc.Width = 120;
            //dts.GridColumnStyles.Add(dtbc);

            //dtbc = new DataGridTextBoxColumn();
            //dtbc.HeaderText = "客户简称";
            //dtbc.MappingName = "ccusabbname";
            //dtbc.Width = 120;
            //dts.GridColumnStyles.Add(dtbc);

            dgView.TableStyles.Add(dts);
            dgView.RowHeadersVisible = true;
            dts.MappingName = source.GetType().Name;
            this.dgView.DataSource = source;
        }

        /// <summary>
        /// 点击返回按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}