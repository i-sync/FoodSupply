namespace UI
{
    partial class CheckStock
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
            this.label10 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.lblcWhName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuantity = new MyTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInvName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarCode = new MyTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbldVDate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbldMDate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblcBatch = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblInvStd = new System.Windows.Forms.Label();
            this.txtcCVCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(1, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 20);
            this.label10.Text = "      批次:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(164, 287);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 30);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(107, 287);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(51, 30);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Enabled = false;
            this.btnDetail.Location = new System.Drawing.Point(22, 287);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(79, 30);
            this.btnDetail.TabIndex = 3;
            this.btnDetail.Text = "已扫数据";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // lblcWhName
            // 
            this.lblcWhName.Location = new System.Drawing.Point(65, 37);
            this.lblcWhName.Name = "lblcWhName";
            this.lblcWhName.Size = new System.Drawing.Size(170, 20);
            this.lblcWhName.Text = "健康新仓库";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "盘点单号:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 20);
            this.label6.Text = "仓库名称:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(65, 256);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(66, 23);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.Text = "29";
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1, 259);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 20);
            this.label11.Text = "      数量:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.Location = new System.Drawing.Point(65, 231);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(170, 20);
            this.lblQuantity.Text = "30";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.Text = "账面数量:";
            // 
            // lblInvName
            // 
            this.lblInvName.Location = new System.Drawing.Point(65, 93);
            this.lblInvName.Name = "lblInvName";
            this.lblInvName.Size = new System.Drawing.Size(170, 20);
            this.lblInvName.Text = "枸杞子";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.Text = "      品名:";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(65, 62);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(170, 23);
            this.txtBarCode.TabIndex = 1;
            this.txtBarCode.Text = "GQZ09-01-32@123243204";
            this.txtBarCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarCode_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.Text = "成品标签:";
            // 
            // lbldVDate
            // 
            this.lbldVDate.Location = new System.Drawing.Point(65, 203);
            this.lbldVDate.Name = "lbldVDate";
            this.lbldVDate.Size = new System.Drawing.Size(170, 20);
            this.lbldVDate.Text = "2018-08-09";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(1, 203);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 20);
            this.label14.Text = "失效日期:";
            // 
            // lbldMDate
            // 
            this.lbldMDate.Location = new System.Drawing.Point(65, 175);
            this.lbldMDate.Name = "lbldMDate";
            this.lbldMDate.Size = new System.Drawing.Size(170, 20);
            this.lbldMDate.Text = "2013-03-04";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(1, 175);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 20);
            this.label12.Text = "生产日期:";
            // 
            // lblcBatch
            // 
            this.lblcBatch.Location = new System.Drawing.Point(65, 147);
            this.lblcBatch.Name = "lblcBatch";
            this.lblcBatch.Size = new System.Drawing.Size(170, 20);
            this.lblcBatch.Text = "GQZ20130903";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.Text = "      规格:";
            // 
            // lblInvStd
            // 
            this.lblInvStd.Location = new System.Drawing.Point(65, 119);
            this.lblInvStd.Name = "lblInvStd";
            this.lblInvStd.Size = new System.Drawing.Size(170, 20);
            this.lblInvStd.Text = "GQZ20130903";
            // 
            // txtcCVCode
            // 
            this.txtcCVCode.Enabled = false;
            this.txtcCVCode.Location = new System.Drawing.Point(65, 6);
            this.txtcCVCode.Name = "txtcCVCode";
            this.txtcCVCode.Size = new System.Drawing.Size(170, 23);
            this.txtcCVCode.TabIndex = 17;
            // 
            // CheckStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 320);
            this.Controls.Add(this.txtcCVCode);
            this.Controls.Add(this.lblInvStd);
            this.Controls.Add(this.lblcBatch);
            this.Controls.Add(this.lbldVDate);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lbldMDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.lblcWhName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInvName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.label2);
            this.Name = "CheckStock";
            this.Text = "库存盘点";
            this.Load += new System.EventHandler(this.CheckStock_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Label lblcWhName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private MyTextBox txtQuantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInvName;
        private System.Windows.Forms.Label label3;
        private MyTextBox txtBarCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbldVDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbldMDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblcBatch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInvStd;
        private System.Windows.Forms.TextBox txtcCVCode;
    }
}