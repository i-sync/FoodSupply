using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using System.Management;
//二维码
//using Gma.QrCodeNet.Encoding;
//using Gma.QrCodeNet.Encoding.Windows.Render;
//using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

//zxing
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

//多线程
using System.Threading;
using System.Xml;

namespace LabelPrint
{
    public partial class MainForm : Form
    {
        private MyOpaqueLayer m_OpaqueLayer = null;//半透明蒙板层
        /// <summary>
        /// 企业信息
        /// </summary>
        string enterprise = "34号供应部";
        //打印文档
        System.Drawing.Printing.PrintDocument printDocument;
        /// <summary>
        /// 打印集合
        /// </summary>
        private List<Inventory> list;
        /// <summary>
        /// 打印临时对象
        /// </summary>
        private Inventory inventory;
        /// <summary>
        /// 打印小标签的流水号
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// 生产日期列
        /// </summary>
        private DateColumn dMDate;

        /// <summary>
        /// 选择当前的类别
        /// </summary>
        private Category category;

        //声明委托
        private delegate void DelegateComboBoxDataBind(List<Receipt> list);//用于绑定ComboBox
        private delegate void DelegateDataGridDataBind(DataTable dt );//用于绑定DataGrid
        private delegate void DelegateHideOpaqueLayer();//用于隐藏遮罩层
        Thread thread;

        public MainForm()
        {   
            InitializeComponent();

            //初始化打印对象
            printDocument = new System.Drawing.Printing.PrintDocument();
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);

            
            //开启线程测试连接
            //显示遮罩层
            ShowOpaqueLayer(this, 125, true);
            thread = new Thread(new ThreadStart(ContentionTest));
            thread.Start();
        }

        /// <summary>
        /// 更新程序 
        /// </summary>
        private void UpdateProrgram()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            //MessageBox.Show(filePath);
            //下载最新的XML文档
            string content = Common.Instance.Service.GetPCNewDocument();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            doc.Save(filePath + "PCupdate.xml");

            System.Diagnostics.Process.Start(filePath + "update.exe", string.Format("{0} {1} {2}", System.Diagnostics.Process.GetCurrentProcess().Id, "PC", Common.Instance.URL));
            //new System.Diagnostics.Process { StartInfo= {FileName
            //Application.Exit();
            //thread.Abort();
        }

        /// <summary>
        /// 测试数据库连接
        /// </summary>
        private void ContentionTest()
        {
            //测试连接
            string errMsg;
            try
            {
                bool flag = Common.Instance.ConnectionTest(out errMsg);
                if (!flag)//如果失败直接返回
                {
                    MessageBox.Show(errMsg);
                    Application.Exit();
                    return;
                }

                //测试连接成功后，
                //获取版本比较是否需要更新
                string ver = Common.Instance.Service.GetPCVersion();
                if (Application.ProductVersion.CompareTo(ver) < 0)
                    UpdateProrgram();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
                return;
            }
            //如果数据链接测试成功隐藏遮罩层
            //隐藏遮罩层
            //HideOpaqueLayer();
            DelegateHideOpaqueLayer dgHQL = new DelegateHideOpaqueLayer(HideOpaqueLayer);
            this.Invoke(dgHQL);

            thread.Abort();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //禁止自动生成列
            dgView.AutoGenerateColumns = false;
            
            //添加日期列
            dMDate = new DateColumn();
            dMDate.Name = "dMDate";
            dMDate.HeaderText = "生产日期";
            dMDate.DataPropertyName = "dMDate";
            dMDate.Width = 100;
            dgView.Columns.Insert(6,dMDate);

            //获取默认打印机名称 
            lblDefaultPrinter.Text = printDocument.PrinterSettings.PrinterName;

            //获取本机所有打印机
            foreach (string name in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbAllPrinter.Items.Add(name);
            }
            cmbAllPrinter.SelectedIndex = 0;

            //默认选择存货类型
            category = Category.Inventory;

        }

        /// <summary>
        /// 标签类型选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoLable_CheckedChanged(object sender, EventArgs e)
        {
            //判断选择类型
            RadioButton rb = sender as RadioButton;
            if (rb == null)
                return;
            if (rb.Name == rdoBigLable.Name)//如何选择的是大标签
            {
                //双份 不可用且不能选中
                chkDouble.Checked = false;
                chkDouble.Enabled = false;
                //日期 可用 
                chkPrintDate.Enabled = true;
            }
            else
            {
                //日期 不可用且不能选中
                chkPrintDate.Checked = false;
                chkPrintDate.Enabled = false;
                //双份 可用
                chkDouble.Enabled = true;
            }
        }

        #region 遮罩层处理

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control"></param>
        /// <param name="alpha"></param>
        /// <param name="showLoadingImage"></param>
        protected void ShowOpaqueLayer(Control control, int alpha,bool showLoading)
        {
            if (this.m_OpaqueLayer == null)
            {
                this.m_OpaqueLayer = new MyOpaqueLayer(alpha, showLoading);
                control.Controls.Add(this.m_OpaqueLayer);
                this.m_OpaqueLayer.Dock = DockStyle.Fill;
                this.m_OpaqueLayer.BringToFront();
            }
            this.m_OpaqueLayer.Enabled = true;
            this.m_OpaqueLayer.Visible = true;


        }

        /// <summary>
        /// 隐藏遮罩层
        /// </summary>
        protected void HideOpaqueLayer()
        {
            if (this.m_OpaqueLayer != null)
            {
                this.m_OpaqueLayer.Enabled = false;
                this.m_OpaqueLayer.Visible = false;
            }
        }  
      
        #endregion

        #region 数据查询及绑定
        /// <summary>
        /// tpage选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCategory.SelectedTab == tpInventory)
            {
                category = Category.Inventory;
                cmbPuchaseList.DataSource = null;
                cmbSaleDeliveryList.DataSource = null;

                //生产日期、批次可修改
                this.dMDate.ReadOnly = false ;
                this.cBatch.ReadOnly = false ;               
                txtInvCode.Focus();
            }
            else if (tabCategory.SelectedTab == tpPurchase)
            {
                category = Category.Puchase;
                txtInvCode.Text = string.Empty;
                txtInvName.Text = string.Empty;
                cmbSaleDeliveryList.DataSource = null;

                //生产日期、批次只读
                this.dMDate.ReadOnly = true;
                this.cBatch.ReadOnly = true;
                
                //加载采购到货单列表
                //显示遮罩层
                ShowOpaqueLayer(this, 125, true);

                //声明线程异步请求数据
                thread = new Thread(new ThreadStart(GetReceiptList));
                thread.Start(); 
            }
            else
            {
                category = Category.SaleDelivery;
                txtInvCode.Text = string.Empty;
                txtInvName.Text = string.Empty;
                cmbPuchaseList.DataSource = null;

                //生产日期、批次只读
                this.dMDate.ReadOnly = true;
                this.cBatch.ReadOnly = true;

                //加载销售发货单列表
                //显示遮罩层
                ShowOpaqueLayer(this, 125, true);
                thread = new Thread(new ThreadStart(GetReceiptList));
                thread.Start();
            }
            this.dgView.DataSource = null;
        }

        /// <summary>
        /// 查询单据列表：采购到货单或销售发货单
        /// </summary>
        /// <returns></returns>
        private void GetReceiptList()
        {
            string errMsg = string.Empty; ;

            List<Receipt> list = null;
            if (category== Category.Puchase)//采购到货
                list = new PrintBusiness().SelectPuchaseList(out errMsg);
            else if(category == Category.SaleDelivery)
                list = new PrintBusiness().SelectSaleDeliveryList(out errMsg);
            
            if (list == null)
            {
                MessageBox.Show(errMsg);
            }
            else if (list.Count > 0)
            {
                DelegateComboBoxDataBind cmbDB = new DelegateComboBoxDataBind(ComboBoxDataBind);
                this.Invoke(cmbDB, new object[] { list });
            }

            //隐藏遮罩层
            //HideOpaqueLayer();
            DelegateHideOpaqueLayer dgHQL = new DelegateHideOpaqueLayer(HideOpaqueLayer);
            this.Invoke(dgHQL);

            thread.Abort();
        }
        
        /// <summary>
        /// 绑定ComboBox数据
        /// </summary>
        /// <param name="list"></param>
        private void ComboBoxDataBind(List<Receipt> list)
        {
            if (category == Category.Puchase)
            {                
                cmbPuchaseList.DataSource = list;
                cmbPuchaseList.ValueMember = "Code";
                cmbPuchaseList.DisplayMember = "Show";
            }
            else if(category== Category.SaleDelivery)
            {
                cmbSaleDeliveryList.DataSource = list;
                cmbSaleDeliveryList.ValueMember = "Code";
                cmbSaleDeliveryList.DisplayMember = "Show";
            }
        }

        /// <summary>
        /// 存货查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInventorySearch_Click(object sender, EventArgs e)
        {
            //获取界面数据
            string cInvCode = txtInvCode.Text.Trim();
            string cInvName = txtInvName.Text.Trim();

            //string errMsg;
            ////查询数据
            //List<Inventory> list = new PrintBusiness().SearchInventory(cInvCode, cInvName, out errMsg);
            //if (list == null)
            //{
            //    MessageBox.Show(errMsg);
            //    return;
            //}
            //dgView.DataSource = list;

            //显示遮罩层
            ShowOpaqueLayer(this, 125, true);
            string [] strArry = new string[]{cInvCode,cInvName};
            thread = new Thread(new ParameterizedThreadStart(GetInventoryList));
            thread.Start(strArry);
        }

        /// <summary>
        /// 采购到货单列表选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPuchaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Receipt receipt = cmbPuchaseList.SelectedItem as Receipt;
            if (receipt == null)
                return;

            //string errMsg;
            ////查询数据
            //List<Inventory> list = new PrintBusiness().SearchInventoryByPUArrival(receipt.Code, out errMsg);
            //if (list == null)
            //{
            //    MessageBox.Show(errMsg);
            //    return;
            //}
            //if (list.Count == 0)
            //{
            //    MessageBox.Show("单据号不正确！");
            //    return;
            //}
            //dgView.DataSource = list;

            //显示遮罩层
            ShowOpaqueLayer(this, 125, true);

            string[] strArry = new string[] { receipt.Code };
            thread = new Thread(new ParameterizedThreadStart(GetInventoryList));
            thread.Start(strArry);
        }
        
        /// <summary>
        /// 销售发货单列表选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSaleDeliveryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Receipt receipt = cmbSaleDeliveryList.SelectedItem as Receipt;
            if (receipt == null)
                return;

            //string errMsg;
            ////查询数据
            //List<Inventory> list = new PrintBusiness().SearchInventoryBySaleDelivery(receipt.Code, out errMsg);
            //if (list == null)
            //{
            //    MessageBox.Show(errMsg);
            //    return;
            //}
            //if (list.Count == 0)
            //{
            //    MessageBox.Show("单据号不正确！");
            //    return;
            //}
            //dgView.DataSource = list;

            //显示遮罩层
            ShowOpaqueLayer(this, 125, true);

            string[] strArry = new string[] { receipt.Code };
            thread = new Thread(new ParameterizedThreadStart(GetInventoryList));
            thread.Start(strArry);
        }

        /// <summary>
        /// 根据类别查询存货信息
        /// </summary>
        /// <param name="category"></param>
        private void GetInventoryList(object param)
        {
            string errMsg = string.Empty ;
            //查询数据
            //List<Inventory> list = null;
            DataTable dt = null;
            string[] strArry = (string[])param;
            switch (category)
            {
                case Category.Inventory:
                    dt = new PrintBusiness().SearchInventory(strArry[0], strArry[1], out errMsg);
                    break;
                case Category.Puchase:
                    dt = new PrintBusiness().SearchInventoryByPUArrival(strArry[0], out errMsg);
                    break;
                case Category.SaleDelivery:
                    dt = new PrintBusiness().SearchInventoryBySaleDelivery(strArry[0], out errMsg);
                    break;
            }

            if (dt == null)
            {
                MessageBox.Show(errMsg);
                return;
            }

            //调用委托绑定DataGrid数据
            DelegateDataGridDataBind dgDB = new DelegateDataGridDataBind(DataGridDataBind);
            this.Invoke(dgDB, new object[] {dt });

            //隐藏遮罩层
            //HideOpaqueLayer();
            DelegateHideOpaqueLayer dgHQL = new DelegateHideOpaqueLayer(HideOpaqueLayer);
            this.Invoke(dgHQL);

            thread.Abort();
        }

        /// <summary>
        /// 绑定DataGrid
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="category"></param>
        private void DataGridDataBind(DataTable  dt)
        {
            this.dgView.DataSource = dt;
        }

        #endregion
    
        #region DataGrid验证        

        /// <summary>
        /// 数据类型验证失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }

        /// <summary>
        /// 验证用户录入是否正确
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //获取列名
            string headerText = dgView.Columns[e.ColumnIndex].HeaderText;
            DataGridViewRow row = dgView.Rows[e.RowIndex];
            //获取当前行是否选中
            bool flag = (bool)row.Cells["cbSelect"].EditedFormattedValue;

            string errMsg = string.Empty;
            if (headerText.Equals("生产日期"))
            {
                if (flag)
                {
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        errMsg = "生产日期不能为空！";
                    }
                    else
                    {
                        //获取打印数量内容
                        int iQuantity = Cast.ToInteger(Cast.ToDouble(row.Cells["iQuantity"].EditedFormattedValue));
                        if (iQuantity <= 0)
                        {
                            errMsg = "数量不正确！";
                        }
                        else if (string.IsNullOrEmpty(row.Cells["cBatch"].EditedFormattedValue.ToString()))
                        {
                            errMsg = "批次不能为空";
                        }
                    }
                }
                row.ErrorText = errMsg;
            }
            else if (headerText.Equals("批次"))
            {
                if (flag)//判断用户是否选中了该行
                {
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        errMsg = "批次不能为空！";
                    }
                    else//如果批次不为空判断数量是否正确
                    {
                        //获取打印数量内容
                        int iQuantity = Cast.ToInteger(Cast.ToDouble(row.Cells["iQuantity"].EditedFormattedValue));
                        if (iQuantity <= 0)
                        {
                            errMsg = "数量不正确！";
                        }
                        else if (string.IsNullOrEmpty(row.Cells["dMDate"].EditedFormattedValue.ToString()))
                        {
                            errMsg = "生产日期不能为空";
                        }
                    }
                }
                row.ErrorText = errMsg;
            }
            else if (headerText.Equals("打印数量"))
            {
                if (flag)
                {
                    double iQuantity;
                    if (!double.TryParse(e.FormattedValue.ToString(), out iQuantity))
                    {
                        errMsg = "请输入数字！";
                        e.Cancel = true;
                    }
                    else if (iQuantity <= 0)
                    {
                        errMsg = "数量不正确！";
                        e.Cancel = false;
                    }
                    else//如果数量正确，判断批次是否正确
                    {
                        //获取批次
                        string cBatch = row.Cells["cBatch"].EditedFormattedValue.ToString();
                        if (string.IsNullOrEmpty(cBatch))
                        {
                            errMsg = "批次不能为空！";
                        }
                        else if (string.IsNullOrEmpty(row.Cells["dMDate"].EditedFormattedValue.ToString()))
                        {
                            errMsg = "生产日期不能为空！";
                        }
                    }
                }
                row.ErrorText = errMsg;
            }
        }

        /// <summary>
        /// 提交CheckBox状态修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgView.IsCurrentCellDirty)
                dgView.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        /// <summary>
        /// checkBox 改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            //如果是选择列
            if (dgView.Columns[e.ColumnIndex].Name.Equals("cbSelect"))
            {                
                DataGridViewRow row = dgView.Rows[e.RowIndex];
                //判断是否选中,如果选中去验证批次或数量是否正确
                if ((bool)row.Cells[e.ColumnIndex].EditedFormattedValue)
                {
                    string dMDate = row.Cells["dMDate"].EditedFormattedValue.ToString();
                    string cBatch = row.Cells["cBatch"].EditedFormattedValue.ToString();
                    int iQuantity = Cast.ToInteger(row.Cells["iQuantity"].EditedFormattedValue);
                    if (string.IsNullOrEmpty(dMDate))
                    {
                        row.ErrorText = "生产日期不能为空！";
                    }
                    if (string.IsNullOrEmpty(cBatch))
                    {
                        row.ErrorText += "批次不能为空！";
                    }
                    if (iQuantity <= 0)
                    {
                        row.ErrorText += "打印数量不正确！";
                    }
                }
                else
                {
                    row.ErrorText = string.Empty;
                }
            }
        }

        #endregion

        #region 打印处理


        /// <summary>
        /// 打印选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPart_Click(object sender, EventArgs e)
        {
            List<Inventory> list =new List<Inventory>();
            Inventory inventory;
            bool flag = false ;//标识选中行是否有错误
            //获取所有选中行
            foreach (DataGridViewRow row in dgView.Rows)
            {
                //获取chSelect的值
                if ((bool)row.Cells["cbSelect"].EditedFormattedValue)
                {
                    //判断该行是否有错误
                    if (row.ErrorText != string.Empty)
                    {
                        flag = true;//有错误
                        //设置光标在错误的一行
                        row.Selected = true;
                        dgView.CurrentCell = row.Cells[0];
                        break;
                    }
                    object dMDate = row.Cells["dMDate"].EditedFormattedValue;
                    string cBatch = row.Cells["cBatch"].EditedFormattedValue.ToString();
                    string iQuantity = row.Cells["iQuantity"].EditedFormattedValue.ToString ();

                    inventory = new Inventory();
                    inventory.cInvCode = row.Cells["cInvCode"].FormattedValue.ToString();
                    inventory.cInvName = row.Cells["cInvName"].FormattedValue.ToString();
                    inventory.cInvStd = row.Cells["cInvStd"].FormattedValue.ToString();
                    inventory.dMDate = Cast.ToDateTime(dMDate);
                    inventory.cBatch = cBatch;
                    inventory.iQuantity = Cast.ToInteger(Cast.ToDouble(iQuantity));
                    list.Add(inventory);
                }
            }
            
            //判断是否有错误
            if (flag)
            {
                MessageBox.Show("有错误请检查！");
                return;
            }

            if (list.Count == 0)
                return;
            this.list = list;

            //如果选中大标签
            if (rdoBigLable.Checked)
            {
                //预览第一个
                PrintLabelBigPre(list[0]);
            }
            else
            {
                //预览时显示流水为1
                serialNumber = 1;
                PrintLabelLittlePre(list[0]);
            }

            //显示遮罩层
            ShowOpaqueLayer(this, 125,false);
            //设置打印预览位置并显示
            int left = (this.Width - gbPrintPreview.Width) / 2;
            int top = (this.Height - gbPrintPreview.Height) / 2;
            gbPrintPreview.Left = left;
            gbPrintPreview.Top = top;
            gbPrintPreview.Visible = true;
            gbPrintPreview.BringToFront();
        }

        /// <summary>
        /// 打印全部存货按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            //循环所有的数据
            foreach (DataGridViewRow row in dgView.Rows)
            {
                row.Cells["cbSelect"].Value = true;
            }
            btnPart_Click(null, null);
        }

        /// <summary>
        /// 设置默认打印机按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefaultPrinter_Click(object sender, EventArgs e)
        {
            //获取要设置为默认打印机的名称
            string dName = cmbAllPrinter.Text;
            if (string.IsNullOrEmpty(dName))
            {
                MessageBox.Show("没有要设置的打印机。");
                return;
            }

            ManagementObjectSearcher query;
            ManagementObjectCollection queryCollection;
            string _classname = "SELECT * FROM Win32_Printer";
            query = new ManagementObjectSearcher(_classname);
            queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (string.Compare(mo["Name"].ToString(), dName, true) == 0)
                {
                    mo.InvokeMethod("SetDefaultPrinter", null);
                    break;
                }
            }

            lblDefaultPrinter.Text = printDocument.PrinterSettings.PrinterName;
        }


        /// <summary>
        /// 确认打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            //设置默认打印机
            printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();

            //printDocument1.PrinterSettings.pr
            if (!printDocument.PrinterSettings.IsValid)
            {
                MessageBox.Show("无效打印机！");
                return;
            }

            //循环打印
            foreach (Inventory i in this.list)
            {
                inventory = i;
                //读写数据库（标签流水号）
                string errMsg;
                int number = Common.Instance.Service.GetLabelSerialNumber(Common.Instance.ConnectionString, i.cInvCode, i.cBatch, out errMsg);
                if (number == -1)
                {
                    MessageBox.Show(errMsg);
                    return;
                }

                serialNumber = number + 1;//新流水号开始
                for (int j = 0; j < i.iQuantity; j++)
                {
                    printDocument.PrinterSettings.Copies = chkDouble.Checked ? (short)2 : (short)1;
                    printDocument.Print();
                    serialNumber++;
                }
                //某个存货打印完成回写流水号
                bool flag = Common.Instance.Service.ModifySerialNumber(Common.Instance.ConnectionString, i.cInvCode, i.cBatch, number, number + i.iQuantity, out errMsg);
                if (!flag)
                {
                    MessageBox.Show(errMsg);
                    return;
                }
            }

            //打印完成
            btnCancel_Click(null, null);
        }

        /// <summary>
        /// 处理打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //把要打印的信息画到打印页上
            Graphics g = e.Graphics;
            Pen p = Pens.Black;
            g.Clear(System.Drawing.Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (this.inventory == null)
                return;

            //大标签
            if (rdoBigLable.Checked)
            {
                //画文字
                DrawBigLabel(g, inventory);
                string content = string.Format("{0}{1}{2}{3}{4}{5}{6}", inventory.cInvCode, Cast.Delimiter, inventory.cBatch, Cast.Delimiter, serialNumber.ToString().PadLeft(5, '0'), Cast.Delimiter, enterprise);
                //画二维码
                PrintQRCode(g, content, new Point(145, 25));
            }
            else//小标签
            { 
                //画文字
                DrawLittleLabel(g, inventory);
                string content = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", inventory.cInvCode, Cast.Delimiter, inventory.cBatch, Cast.Delimiter, serialNumber.ToString().PadLeft(5, '0'), Cast.Delimiter,inventory.cInvName,Cast.Delimiter, enterprise);
                //画二维码
                PrintQRCode(g, content, new Point(10, 10));
            }
            g.Dispose();
        }

        /// <summary>
        /// 取消打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            gbPrintPreview.Visible = false;
            HideOpaqueLayer();
        }

        #endregion

        #region 画标签

        /// <summary>
        /// 打印QR
        /// </summary>
        /// <param name="g"></param>
        /// <param name="content"></param>
        /// <param name="pointF"></param>
        private void PrintQRCode(Graphics g, string content, Point point)
        {
            //QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            
            //QrCode qrCode = new QrCode();
            //qrEncoder.TryEncode(content, out qrCode);


            ////EncapsulatedPostScriptRenderer renderer = new Gma.QrCodeNet.Encoding.Windows.Render.EncapsulatedPostScriptRenderer(new FixedModuleSize(5, QuietZoneModules.Two), new FormColor(Color.Black), new FormColor(Color.White));
            //Gma.QrCodeNet.Encoding.Windows.Render.GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(2, QuietZoneModules.Two));// Modules size is 6/72th inch (72 points = 1 inch)
            //renderer.Draw(g, qrCode.Matrix, point);

            //zxing
            IBarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,//QR码
                Options = new QrCodeEncodingOptions { Width = 85, Height = 85, Margin = 0, ErrorCorrection = ErrorCorrectionLevel.M, CharacterSet = "UTF-8" }//指定一个参数
            };
            Bitmap bm = writer.Write(content);
            g.DrawImage(bm, point);
            //g.DrawRectangle(new Pen(Color.Red), point.X, point.Y, bm.Width, bm.Height);
        }

        /// <summary>
        /// 画出标签预览
        /// </summary>
        /// <param name="inventory"></param>
        private void PrintLabelBigPre(Inventory inventory)
        {
            string content = string.Format("{0}{1}{2}{3}00001{4}{5}",inventory.cInvCode,Cast.Delimiter,inventory.cBatch,Cast.Delimiter,Cast.Delimiter, enterprise);
            //创建图片对象
            Bitmap bp = new Bitmap(240, 160); //new Bitmap((int)MillimetersToPixelsWidth(60), (int)MillimetersToPixelsWidth(40));
            Graphics g = Graphics.FromImage(bp);
            g.Clear(Color.White);
            //画文字
            DrawBigLabel(g, inventory);

            //画QR
            PrintQRCode(g, content, new Point(145, 25));

            pbPreview.Image = bp;
        }

        /// <summary>
        /// 画大标签文字
        /// </summary>
        /// <param name="g"></param>
        private void DrawBigLabel(Graphics g,Inventory inventory)
        {
            float x1 = 3F;
            float y1 = 35F;
            float x2 = 63F;
            float w1 = 63F;
            float w2 = 80F;
            float h1 = 15F;

            //变量
            string cString = string.Empty;
            Font font = new Font("黑体", 9, FontStyle.Bold);
            RectangleF rect = new RectangleF(x1, y1, w1, h1);
            SolidBrush brush = new SolidBrush(System.Drawing.Color.Black);

            //名称
            cString = "名    称:";
            g.DrawString(cString, font, brush, rect);
            //名称内容
            cString = inventory.cInvName;
            rect = new RectangleF(x2, y1, w2, 2 * h1);
            g.DrawString(cString, font, brush, rect);

            //批次
            cString = "批    次:";
            rect = new RectangleF(x1, y1 + 2 * h1, w1, h1);
            g.DrawString(cString, font, brush, rect);
            //批次内容
            cString = inventory.cBatch;
            rect = new RectangleF(x2, y1 + 2 * h1, w2, 2 * h1);
            g.DrawString(cString, font, brush, rect);
            //g.DrawRectangle(new Pen(Color.Blue), x2, y1 + 2 * h1, w2, 2 * h1);

            //判断是否打印日期
            if (chkPrintDate.Checked)
            {
                //生产日期
                cString = "生产日期:";
                rect = new RectangleF(x1, y1 + 4 * h1, w1, h1);
                g.DrawString(cString, font, brush, rect);
                //生产日期内容
                cString = inventory.dMDateShort;
                rect = new RectangleF(x2, y1 + 4 * h1, w2, h1);
                g.DrawString(cString, font, brush, rect);
            }
        }

        /// <summary>
        /// 画出小标签预览
        /// </summary>
        /// <param name="inventory"></param>
        private void PrintLabelLittlePre(Inventory inventory)
        {
            string content = string.Format("{0}{1}{2}{3}00001{4}{5}{6}{7}", inventory.cInvCode, Cast.Delimiter, inventory.cBatch,Cast.Delimiter,Cast.Delimiter,inventory.cInvName,Cast.Delimiter, enterprise);
            //创建图片对象
            Bitmap bp = new Bitmap(110, 145);
            Graphics g = Graphics.FromImage(bp);
            g.Clear(Color.White);     
            //画文字
            DrawLittleLabel(g, inventory);

            //画QR
            PrintQRCode(g, content, new Point(10, 10));

            pbPreview.Image = bp;
        }

        /// <summary>
        /// 画小标签文字
        /// </summary>
        /// <param name="g"></param>
        /// <param name="inventory"></param>
        private void DrawLittleLabel(Graphics g,Inventory inventory)
        {            
            float x1 = 10F;
            float x2 = 55F;
            float y1 = 105F;
            float w1 = 100F;
            float h1 = 15F;

            string cBatch = inventory.cBatch;
            //变量(画出批次流水号后5位)
            string cString = cBatch.Substring(cBatch.Length - 5);
            Font font = new Font("黑体", 8, FontStyle.Bold);
            RectangleF rect = new RectangleF(x1, y1, w1, h1);
            SolidBrush brush = new SolidBrush(System.Drawing.Color.Black);
            g.DrawString(cString, font, brush, rect);

            //显示标签流水号
            cString = serialNumber.ToString().PadLeft(5, '0');
            rect = new RectangleF(x2, y1, w1, h1);
            g.DrawString(cString, font, brush, rect);

            //名称
            cString = inventory.cInvName;
            rect = new RectangleF(x1, y1 + h1, w1, 2 * h1);
            g.DrawString(cString, font, brush, rect);
        }

        #endregion

    }
}
