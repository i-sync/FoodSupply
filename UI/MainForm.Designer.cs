namespace UI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnSaleLibrary = new System.Windows.Forms.Button();
            this.btnTrace = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCheckStock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblCurrentDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaleLibrary
            // 
            this.btnSaleLibrary.Location = new System.Drawing.Point(22, 59);
            this.btnSaleLibrary.Name = "btnSaleLibrary";
            this.btnSaleLibrary.Size = new System.Drawing.Size(86, 82);
            this.btnSaleLibrary.TabIndex = 0;
            this.btnSaleLibrary.Text = "销售出库";
            this.btnSaleLibrary.Click += new System.EventHandler(this.btnSaleLibrary_Click);
            // 
            // btnTrace
            // 
            this.btnTrace.Location = new System.Drawing.Point(131, 59);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(86, 82);
            this.btnTrace.TabIndex = 0;
            this.btnTrace.Text = "产品追溯";
            this.btnTrace.Click += new System.EventHandler(this.btnTrace_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(131, 160);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 78);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCheckStock
            // 
            this.btnCheckStock.Location = new System.Drawing.Point(22, 160);
            this.btnCheckStock.Name = "btnCheckStock";
            this.btnCheckStock.Size = new System.Drawing.Size(86, 78);
            this.btnCheckStock.TabIndex = 2;
            this.btnCheckStock.Text = "仓库盘点";
            this.btnCheckStock.Click += new System.EventHandler(this.btnCheckStock_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.Text = "操作员:";
            // 
            // lblUserName
            // 
            this.lblUserName.ForeColor = System.Drawing.Color.Red;
            this.lblUserName.Location = new System.Drawing.Point(54, 4);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 20);
            this.lblUserName.Text = "张三";
            // 
            // lblCurrentDate
            // 
            this.lblCurrentDate.ForeColor = System.Drawing.Color.Red;
            this.lblCurrentDate.Location = new System.Drawing.Point(163, 3);
            this.lblCurrentDate.Name = "lblCurrentDate";
            this.lblCurrentDate.Size = new System.Drawing.Size(75, 20);
            this.lblCurrentDate.Text = "2013-07-05";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(126, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.Text = "日期:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.lblCurrentDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheckStock);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnTrace);
            this.Controls.Add(this.btnSaleLibrary);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "主窗体";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaleLibrary;
        private System.Windows.Forms.Button btnTrace;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCheckStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblCurrentDate;
        private System.Windows.Forms.Label label4;
    }
}