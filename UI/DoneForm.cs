using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace UI
{
    public partial class DoneForm<T,S> : Form
    {
        /// <summary>
        /// 数据源
        /// </summary>
        private List<T> list;
        private List<S> source;

        public DoneForm()
        {
            InitializeComponent();
        }

        public DoneForm(List<T> list,List<S> source)
            : this()
        {
            this.list = list;
            this.source = source;
        }

        /// <summary>
        /// 窗体加载事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoneForm_Load(object sender, EventArgs e)
        {
            if (list == null)
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
            dtbc.MappingName = "iSOutQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            //dtbc = new DataGridTextBoxColumn();
            //dtbc.HeaderText = "货位";
            //dtbc.MappingName = "cPosition";
            //dtbc.Width = 70;
            //dts.GridColumnStyles.Add(dtbc);

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
            dts.MappingName = list.GetType().Name;
            this.dgView.DataSource = list;
        }

        /// <summary>
        /// 删除行按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>2013-09-25修改，由于没有按批次删除，导致删除存货时，其它批次的扫描数量也减少</remarks>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (list.Count == 0)
                {
                    return;
                }
                int index = dgView.CurrentRowIndex;
                if (index < 0)
                {
                    MessageBox.Show("请选择要删除的行！");
                    return;
                }
                DialogResult dr = MessageBox.Show("您确定要删除此行吗？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    T t = list[index];
                    string cInvCode = string.Empty;
                    string cBatch = string.Empty;
                    //string cPosition = string.Empty;
                    double iScanQuantity = 0d;
                    //查找cInvCode,iQuantity属性值及批次属性值
                    cInvCode = typeof(T).GetProperty("cInvCode").GetValue(t, null).ToString();
                    iScanQuantity = Convert.ToDouble(typeof(T).GetProperty("iScanQuantity").GetValue(t, null));
                    cBatch = typeof(T).GetProperty("cBatch").GetValue(t, null).ToString();

                    dgView.DataSource = null;
                    list.Remove(t);
                    dgView.DataSource = list;
                    if (list.Count > 0)
                        dgView.CurrentRowIndex = 0;
                    //减少source中已扫描数量
                    foreach (S s in source)
                    {
                        string strInvCode = typeof(S).GetProperty("cInvCode").GetValue(s, null).ToString();
                        string strBatch = typeof(S).GetProperty("cBatch").GetValue(s, null).ToString();
                        if (cInvCode.Equals(strInvCode) && cBatch.Equals(strBatch))
                        {
                            double quantity = Convert.ToDouble(typeof(S).GetProperty("iScanQuantity").GetValue(s, null)) - iScanQuantity;
                            //回写
                            typeof(S).GetProperty("iScanQuantity").SetValue(s, quantity, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}