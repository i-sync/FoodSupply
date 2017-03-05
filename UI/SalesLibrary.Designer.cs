namespace UI
{
    partial class SalesLibrary
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.label6 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.lblScanedNum = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCount = new MyTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblValidDate = new System.Windows.Forms.Label();
            this.lblProDate = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblInvStd = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInvName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarcode = new MyTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDelivery = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCWhName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblcCusName = new System.Windows.Forms.Label();
            this.chkSingle = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.Text = "仓库名称:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(185, 270);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 34);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(133, 270);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(51, 34);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(67, 270);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(65, 34);
            this.btnDone.TabIndex = 4;
            this.btnDone.Text = "已扫数据";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnSource
            // 
            this.btnSource.Enabled = false;
            this.btnSource.Location = new System.Drawing.Point(1, 270);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(65, 34);
            this.btnSource.TabIndex = 8;
            this.btnSource.Text = "来源单据";
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // lblScanedNum
            // 
            this.lblScanedNum.Location = new System.Drawing.Point(188, 245);
            this.lblScanedNum.Name = "lblScanedNum";
            this.lblScanedNum.Size = new System.Drawing.Size(38, 20);
            this.lblScanedNum.Text = "123";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(127, 246);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 20);
            this.label12.Text = "已扫描:";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(66, 244);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(55, 23);
            this.txtCount.TabIndex = 3;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1, 244);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 20);
            this.label11.Text = "      数量:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(1, 218);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 20);
            this.label10.Text = "      批次:";
            // 
            // lblBatch
            // 
            this.lblBatch.Location = new System.Drawing.Point(66, 218);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(172, 20);
            this.lblBatch.Text = "20150101";
            // 
            // lblValidDate
            // 
            this.lblValidDate.Location = new System.Drawing.Point(66, 192);
            this.lblValidDate.Name = "lblValidDate";
            this.lblValidDate.Size = new System.Drawing.Size(172, 20);
            this.lblValidDate.Text = "20150101";
            // 
            // lblProDate
            // 
            this.lblProDate.Location = new System.Drawing.Point(66, 165);
            this.lblProDate.Name = "lblProDate";
            this.lblProDate.Size = new System.Drawing.Size(172, 20);
            this.lblProDate.Text = "20120101";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(1, 192);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.Text = "有效期至:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(1, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.Text = "生产日期:";
            // 
            // lblInvStd
            // 
            this.lblInvStd.Location = new System.Drawing.Point(66, 138);
            this.lblInvStd.Name = "lblInvStd";
            this.lblInvStd.Size = new System.Drawing.Size(172, 20);
            this.lblInvStd.Text = "10g/袋";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.Text = "      规格:";
            // 
            // lblInvName
            // 
            this.lblInvName.Location = new System.Drawing.Point(66, 112);
            this.lblInvName.Name = "lblInvName";
            this.lblInvName.Size = new System.Drawing.Size(172, 20);
            this.lblInvName.Text = "人参";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.Text = "      品名:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(66, 55);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(172, 23);
            this.txtBarcode.TabIndex = 2;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.Text = "食品标签:";
            // 
            // txtDelivery
            // 
            this.txtDelivery.Location = new System.Drawing.Point(66, 2);
            this.txtDelivery.Name = "txtDelivery";
            this.txtDelivery.ReadOnly = true;
            this.txtDelivery.Size = new System.Drawing.Size(134, 23);
            this.txtDelivery.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.Text = " 发 货 单:";
            // 
            // lblCWhName
            // 
            this.lblCWhName.Location = new System.Drawing.Point(66, 85);
            this.lblCWhName.Name = "lblCWhName";
            this.lblCWhName.Size = new System.Drawing.Size(172, 20);
            this.lblCWhName.Text = "健康新仓库";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.Text = "客户简称:";
            // 
            // lblcCusName
            // 
            this.lblcCusName.Location = new System.Drawing.Point(66, 31);
            this.lblcCusName.Name = "lblcCusName";
            this.lblcCusName.Size = new System.Drawing.Size(172, 20);
            this.lblcCusName.Text = "XXXX";
            // 
            // chkSingle
            // 
            this.chkSingle.Location = new System.Drawing.Point(202, 3);
            this.chkSingle.Name = "chkSingle";
            this.chkSingle.Size = new System.Drawing.Size(36, 20);
            this.chkSingle.TabIndex = 25;
            this.chkSingle.Text = "单";
            // 
            // SalesLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 309);
            this.ControlBox = false;
            this.Controls.Add(this.chkSingle);
            this.Controls.Add(this.lblcCusName);
            this.Controls.Add(this.lblCWhName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.lblScanedNum);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblBatch);
            this.Controls.Add(this.lblValidDate);
            this.Controls.Add(this.lblProDate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblInvStd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInvName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDelivery);
            this.Controls.Add(this.label1);
            this.Name = "SalesLibrary";
            this.Text = "销售出库";
            this.Load += new System.EventHandler(this.SalesLibrary_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label lblScanedNum;
        private System.Windows.Forms.Label label12;
        private MyTextBox txtCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.Label lblValidDate;
        private System.Windows.Forms.Label lblProDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblInvStd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInvName;
        private System.Windows.Forms.Label label3;
        private MyTextBox txtBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDelivery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCWhName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblcCusName;
        private System.Windows.Forms.CheckBox chkSingle;
    }
}