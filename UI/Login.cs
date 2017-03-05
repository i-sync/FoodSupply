using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Model;
using System.Threading;

namespace UI
{
    public partial class Login : Form
    {
        /// <summary>
        /// 临时存储用户名
        /// </summary>
        string userName = string.Empty;
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 账套集合
        /// </summary>
        List<KeyValuePair<string, string>> list =null;

        /// <summary>
        /// 服务器地址
        /// </summary>
        private string url;
        /// <summary>
        /// 线程
        /// </summary>
        Thread thread;


        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Load(object sender, EventArgs e)
        {
            //txtYear.Text = DateTime.Now.ToString("yyyy");
            url = ServiceUrl();

            //设置服务器URL地址
            BLL.Common.ServiceURL = url;

            
            //获取服务器版本
            //string ver = BLL.Common.Instance.GetPPCVersion();
            //if (!System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Equals(ver))
            //{
            //    thread = new Thread(new ThreadStart(UpdateProrgram));
            //    thread.Start();
            //}
        }

        /// <summary>
        /// 更新程序 
        /// </summary>
        private void UpdateProrgram()
        {
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            //MessageBox.Show(filePath);
            //下载最新的XML文档
            string content = BLL.Common.Instance.GetPPCNewDocument();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            doc.Save(filePath + "\\PPCupdate.xml");

            System.Diagnostics.Process.Start(filePath + "\\update.exe", string.Format("{0} {1} {2}",System.Diagnostics.Process.GetCurrentProcess().Id, "PPC", url));
            //new System.Diagnostics.Process { StartInfo= {FileName
            //Application.Exit();
            thread.Abort();
        }

        

        /// <summary>
        /// 输入用户名后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(userName))
            {
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
        }
        /// <summary>
        /// 失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_LostFocus(object sender, EventArgs e)
        {
            //获取用户名信息
            string username = txtUserName.Text.Trim();
            //判断用户名是否修改，若修改则清空账套信息
            if (!this.userName.Equals(username))
            {
                this.cmbAccId.DataSource = null;
                this.userName = username;

                this.lblMessage.Text = string.Empty;
            }
        }
        /// <summary>
        /// 密码按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //txtYear.Focus();
                //txtYear.SelectAll();
                cmbAccId.Focus();
            }
        }
        
        /// <summary>
        /// 账套列表获取焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAccId_GotFocus(object sender, EventArgs e)
        {
            //首先判断数据源是否为空
            if (this.cmbAccId.DataSource == null)
            {
                //根据用户名重新读取数据
                Cursor.Current = Cursors.WaitCursor;
                list =BLL.Common.Instance.GetUAAcountInfo(userName);
                Cursor.Current = Cursors.Default;

                if (list == null || list.Count == 0)
                {
                    lblMessage.Text = "读取账套错误！不存在的用户名或已被注销";
                    txtUserName.Focus();
                    txtUserName.SelectAll();
                    return;
                }

                cmbAccId.DataSource = list;
                cmbAccId.DisplayMember = "Value";
                cmbAccId.ValueMember = "Key";
            }
        }

        /// <summary>
        /// 选择账套回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAccId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.cmbAccId.DataSource != null && e.KeyChar == (char)Keys.Enter)
            {
                //btnLogin_Click(null, null);
                dtpOperateDate.Focus();
            }
        }

        /// <summary>
        /// 选择日期后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpOperateDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        /// <summary>
        /// 点击登录按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbAccId.DataSource == null)
            {
                MessageBox.Show("请先选择账套！");
                return;
            }
            DateTime operateDate = dtpOperateDate.Value;
            //获取界面数据
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            //string year = 
            string accid = ((KeyValuePair<string, string>)(cmbAccId.SelectedItem)).Key;
            //string subid = (cmbSubsys.SelectedItem as UA_Subsys).CSub_ID;

            //封装对象
            User user = new User();
            user.UserID = userName;
            user.Password = password;
            user.Year = operateDate.Year;//Convert.ToInt32(year);
            user.AccID = accid;
            Cursor.Current = Cursors.WaitCursor;
            bool flag = BLL.Common.Instance.Login(user);
            Cursor.Current = Cursors.Default;
            if (flag)
            {
                MainForm mainForm = new MainForm(operateDate, this);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("登录失败！");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }


        #region 读取配置文件

        /// <summary>
        /// 获取web服务的URL
        /// </summary>
        /// <returns></returns>
        private string ServiceUrl()
        {
            XmlDocument xml = new XmlDocument();
            //xml路径
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            filePath = System.IO.Path.Combine(filePath, "config.xml");
            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show("配置文件不存在！");
                return "";
            }
            xml.Load(filePath);
            string url = xml.FirstChild.SelectSingleNode("webservice").InnerText;
            return url;
        }
        #endregion


    }
}