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
    public partial class MainForm : Form
    {
        private Form parentForm;
        private static DateTime operateDate;//操作日期
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(DateTime operateDate, Form parentForm)
            : this()
        {
            MainForm.operateDate = operateDate;
            this.parentForm = parentForm;
        }

        /// <summary>
        /// 获取当前操作时间
        /// </summary>
        public static DateTime OperateTime
        {
            get
            {
                return Convert.ToDateTime(string.Format("{0} {1}",operateDate.ToShortDateString(),DateTime.Now.ToShortTimeString()));
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //加载基本信息
            lblUserName.Text = BLL.Common.Instance.User.UserName;
            lblCurrentDate.Text = operateDate.ToString("yyyy-MM-dd");

            //根据用户权限来控件操作按钮是否可用
            btnSaleLibrary.Enabled = BLL.Common.Competence.XSCK;
            btnCheckStock.Enabled = BLL.Common.Competence.PD;
        }

        /// <summary>
        /// 点击加载销售发货单列表按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaleLibrary_Click(object sender, EventArgs e)
        {
            ConsignmentList form = new ConsignmentList();
            form.ShowDialog();
        }
        /// <summary>
        /// 点击食品追溯按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrace_Click(object sender, EventArgs e)
        {
            FoodTrace form = new FoodTrace();
            form.ShowDialog();
        }
        /// <summary>
        /// 点击盘点按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckStock_Click(object sender, EventArgs e)
        {
            CheckStockList form = new CheckStockList();
            form.ShowDialog();
        }

        /// <summary>
        /// 点击退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            parentForm.Activate();
            this.Close();
        }
    }
}