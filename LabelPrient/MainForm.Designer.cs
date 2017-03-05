namespace LabelPrint
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabCategory = new System.Windows.Forms.TabControl();
            this.tpInventory = new System.Windows.Forms.TabPage();
            this.btnInventorySearch = new System.Windows.Forms.Button();
            this.txtInvCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInvName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpPurchase = new System.Windows.Forms.TabPage();
            this.cmbPuchaseList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpSaleLibrary = new System.Windows.Forms.TabPage();
            this.cmbSaleDeliveryList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.chkDouble = new System.Windows.Forms.CheckBox();
            this.chkPrintDate = new System.Windows.Forms.CheckBox();
            this.rdoLittleLable = new System.Windows.Forms.RadioButton();
            this.rdoBigLable = new System.Windows.Forms.RadioButton();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.cbSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cInvCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cInvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cInvStd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iMassDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMassUintName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPart = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.gbPrintPreview = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSure = new System.Windows.Forms.Button();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnDefaultPrinter = new System.Windows.Forms.Button();
            this.cmbAllPrinter = new System.Windows.Forms.ComboBox();
            this.lblDefaultPrinter = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabCategory.SuspendLayout();
            this.tpInventory.SuspendLayout();
            this.tpPurchase.SuspendLayout();
            this.tpSaleLibrary.SuspendLayout();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.gbPrintPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCategory
            // 
            this.tabCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCategory.Controls.Add(this.tpInventory);
            this.tabCategory.Controls.Add(this.tpPurchase);
            this.tabCategory.Controls.Add(this.tpSaleLibrary);
            this.tabCategory.Location = new System.Drawing.Point(6, 3);
            this.tabCategory.Name = "tabCategory";
            this.tabCategory.SelectedIndex = 0;
            this.tabCategory.Size = new System.Drawing.Size(612, 80);
            this.tabCategory.TabIndex = 0;
            this.tabCategory.SelectedIndexChanged += new System.EventHandler(this.tabCategory_SelectedIndexChanged);
            // 
            // tpInventory
            // 
            this.tpInventory.Controls.Add(this.btnInventorySearch);
            this.tpInventory.Controls.Add(this.txtInvCode);
            this.tpInventory.Controls.Add(this.label2);
            this.tpInventory.Controls.Add(this.txtInvName);
            this.tpInventory.Controls.Add(this.label1);
            this.tpInventory.Location = new System.Drawing.Point(4, 22);
            this.tpInventory.Name = "tpInventory";
            this.tpInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tpInventory.Size = new System.Drawing.Size(604, 54);
            this.tpInventory.TabIndex = 0;
            this.tpInventory.Text = "存货标签";
            this.tpInventory.UseVisualStyleBackColor = true;
            // 
            // btnInventorySearch
            // 
            this.btnInventorySearch.Location = new System.Drawing.Point(475, 17);
            this.btnInventorySearch.Name = "btnInventorySearch";
            this.btnInventorySearch.Size = new System.Drawing.Size(79, 23);
            this.btnInventorySearch.TabIndex = 2;
            this.btnInventorySearch.Text = "查询";
            this.btnInventorySearch.UseVisualStyleBackColor = true;
            this.btnInventorySearch.Click += new System.EventHandler(this.btnInventorySearch_Click);
            // 
            // txtInvCode
            // 
            this.txtInvCode.Location = new System.Drawing.Point(78, 19);
            this.txtInvCode.Name = "txtInvCode";
            this.txtInvCode.Size = new System.Drawing.Size(126, 21);
            this.txtInvCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "存货编码";
            // 
            // txtInvName
            // 
            this.txtInvName.Location = new System.Drawing.Point(286, 19);
            this.txtInvName.Name = "txtInvName";
            this.txtInvName.Size = new System.Drawing.Size(119, 21);
            this.txtInvName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "存货名称";
            // 
            // tpPurchase
            // 
            this.tpPurchase.Controls.Add(this.cmbPuchaseList);
            this.tpPurchase.Controls.Add(this.label3);
            this.tpPurchase.Location = new System.Drawing.Point(4, 22);
            this.tpPurchase.Name = "tpPurchase";
            this.tpPurchase.Padding = new System.Windows.Forms.Padding(3);
            this.tpPurchase.Size = new System.Drawing.Size(604, 54);
            this.tpPurchase.TabIndex = 1;
            this.tpPurchase.Text = "采购到货";
            this.tpPurchase.UseVisualStyleBackColor = true;
            // 
            // cmbPuchaseList
            // 
            this.cmbPuchaseList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPuchaseList.FormattingEnabled = true;
            this.cmbPuchaseList.Location = new System.Drawing.Point(109, 19);
            this.cmbPuchaseList.Name = "cmbPuchaseList";
            this.cmbPuchaseList.Size = new System.Drawing.Size(302, 20);
            this.cmbPuchaseList.TabIndex = 3;
            this.cmbPuchaseList.SelectedIndexChanged += new System.EventHandler(this.cmbPuchaseList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "采购到货单：";
            // 
            // tpSaleLibrary
            // 
            this.tpSaleLibrary.Controls.Add(this.cmbSaleDeliveryList);
            this.tpSaleLibrary.Controls.Add(this.label4);
            this.tpSaleLibrary.Location = new System.Drawing.Point(4, 22);
            this.tpSaleLibrary.Name = "tpSaleLibrary";
            this.tpSaleLibrary.Size = new System.Drawing.Size(604, 54);
            this.tpSaleLibrary.TabIndex = 2;
            this.tpSaleLibrary.Text = "销售出库";
            this.tpSaleLibrary.UseVisualStyleBackColor = true;
            // 
            // cmbSaleDeliveryList
            // 
            this.cmbSaleDeliveryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaleDeliveryList.FormattingEnabled = true;
            this.cmbSaleDeliveryList.Location = new System.Drawing.Point(109, 19);
            this.cmbSaleDeliveryList.Name = "cmbSaleDeliveryList";
            this.cmbSaleDeliveryList.Size = new System.Drawing.Size(302, 20);
            this.cmbSaleDeliveryList.TabIndex = 3;
            this.cmbSaleDeliveryList.SelectedIndexChanged += new System.EventHandler(this.cmbSaleDeliveryList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "销售发货单：";
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.chkDouble);
            this.groupBox.Controls.Add(this.chkPrintDate);
            this.groupBox.Controls.Add(this.rdoLittleLable);
            this.groupBox.Controls.Add(this.rdoBigLable);
            this.groupBox.Location = new System.Drawing.Point(624, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(272, 66);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "选择标签类型";
            // 
            // chkDouble
            // 
            this.chkDouble.AutoSize = true;
            this.chkDouble.Enabled = false;
            this.chkDouble.Location = new System.Drawing.Point(211, 33);
            this.chkDouble.Name = "chkDouble";
            this.chkDouble.Size = new System.Drawing.Size(48, 16);
            this.chkDouble.TabIndex = 1;
            this.chkDouble.Text = "双份";
            this.chkDouble.UseVisualStyleBackColor = true;
            // 
            // chkPrintDate
            // 
            this.chkPrintDate.AutoSize = true;
            this.chkPrintDate.Location = new System.Drawing.Point(76, 33);
            this.chkPrintDate.Name = "chkPrintDate";
            this.chkPrintDate.Size = new System.Drawing.Size(48, 16);
            this.chkPrintDate.TabIndex = 1;
            this.chkPrintDate.Text = "日期";
            this.chkPrintDate.UseVisualStyleBackColor = true;
            // 
            // rdoLittleLable
            // 
            this.rdoLittleLable.AutoSize = true;
            this.rdoLittleLable.Location = new System.Drawing.Point(146, 33);
            this.rdoLittleLable.Name = "rdoLittleLable";
            this.rdoLittleLable.Size = new System.Drawing.Size(59, 16);
            this.rdoLittleLable.TabIndex = 0;
            this.rdoLittleLable.Text = "小标签";
            this.rdoLittleLable.UseVisualStyleBackColor = true;
            this.rdoLittleLable.CheckedChanged += new System.EventHandler(this.rdoLable_CheckedChanged);
            // 
            // rdoBigLable
            // 
            this.rdoBigLable.AutoSize = true;
            this.rdoBigLable.Checked = true;
            this.rdoBigLable.Location = new System.Drawing.Point(16, 33);
            this.rdoBigLable.Name = "rdoBigLable";
            this.rdoBigLable.Size = new System.Drawing.Size(59, 16);
            this.rdoBigLable.TabIndex = 0;
            this.rdoBigLable.TabStop = true;
            this.rdoBigLable.Text = "大标签";
            this.rdoBigLable.UseVisualStyleBackColor = true;
            this.rdoBigLable.CheckedChanged += new System.EventHandler(this.rdoLable_CheckedChanged);
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbSelect,
            this.cInvCode,
            this.cInvName,
            this.cInvStd,
            this.iMassDate,
            this.cMassUintName,
            this.cBatch,
            this.iQuantity});
            this.dgView.Location = new System.Drawing.Point(5, 88);
            this.dgView.Name = "dgView";
            this.dgView.RowTemplate.Height = 23;
            this.dgView.Size = new System.Drawing.Size(891, 393);
            this.dgView.TabIndex = 2;
            this.dgView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellValueChanged);
            this.dgView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgView_CellValidating);
            this.dgView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgView_CurrentCellDirtyStateChanged);
            this.dgView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgView_DataError);
            // 
            // cbSelect
            // 
            this.cbSelect.HeaderText = "选择";
            this.cbSelect.Name = "cbSelect";
            this.cbSelect.Width = 40;
            // 
            // cInvCode
            // 
            this.cInvCode.DataPropertyName = "cInvCode";
            this.cInvCode.HeaderText = "存货编码";
            this.cInvCode.Name = "cInvCode";
            this.cInvCode.ReadOnly = true;
            this.cInvCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cInvCode.Width = 90;
            // 
            // cInvName
            // 
            this.cInvName.DataPropertyName = "cInvName";
            this.cInvName.HeaderText = "存货名称";
            this.cInvName.Name = "cInvName";
            this.cInvName.ReadOnly = true;
            this.cInvName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cInvName.Width = 110;
            // 
            // cInvStd
            // 
            this.cInvStd.DataPropertyName = "cInvStd";
            this.cInvStd.HeaderText = "规格";
            this.cInvStd.Name = "cInvStd";
            this.cInvStd.ReadOnly = true;
            this.cInvStd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cInvStd.Width = 110;
            // 
            // iMassDate
            // 
            this.iMassDate.DataPropertyName = "iMassDate";
            this.iMassDate.HeaderText = "保质期数量";
            this.iMassDate.Name = "iMassDate";
            this.iMassDate.ReadOnly = true;
            this.iMassDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.iMassDate.Width = 90;
            // 
            // cMassUintName
            // 
            this.cMassUintName.DataPropertyName = "cMassUnit";
            this.cMassUintName.HeaderText = "保质期单位";
            this.cMassUintName.Name = "cMassUintName";
            this.cMassUintName.ReadOnly = true;
            this.cMassUintName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cMassUintName.Width = 90;
            // 
            // cBatch
            // 
            this.cBatch.DataPropertyName = "cBatch";
            this.cBatch.HeaderText = "批次";
            this.cBatch.Name = "cBatch";
            this.cBatch.Width = 110;
            // 
            // iQuantity
            // 
            this.iQuantity.DataPropertyName = "iQuantity";
            this.iQuantity.HeaderText = "打印数量";
            this.iQuantity.Name = "iQuantity";
            this.iQuantity.Width = 80;
            // 
            // btnPart
            // 
            this.btnPart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPart.Location = new System.Drawing.Point(731, 489);
            this.btnPart.Name = "btnPart";
            this.btnPart.Size = new System.Drawing.Size(75, 30);
            this.btnPart.TabIndex = 3;
            this.btnPart.Text = "选中打印";
            this.btnPart.UseVisualStyleBackColor = true;
            this.btnPart.Click += new System.EventHandler(this.btnPart_Click);
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Location = new System.Drawing.Point(821, 489);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 30);
            this.btnAll.TabIndex = 3;
            this.btnAll.Text = "全部打印";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // gbPrintPreview
            // 
            this.gbPrintPreview.Controls.Add(this.btnCancel);
            this.gbPrintPreview.Controls.Add(this.btnSure);
            this.gbPrintPreview.Controls.Add(this.pbPreview);
            this.gbPrintPreview.Controls.Add(this.btnDefaultPrinter);
            this.gbPrintPreview.Controls.Add(this.cmbAllPrinter);
            this.gbPrintPreview.Controls.Add(this.lblDefaultPrinter);
            this.gbPrintPreview.Controls.Add(this.label6);
            this.gbPrintPreview.Controls.Add(this.label5);
            this.gbPrintPreview.Location = new System.Drawing.Point(169, 136);
            this.gbPrintPreview.Name = "gbPrintPreview";
            this.gbPrintPreview.Size = new System.Drawing.Size(395, 318);
            this.gbPrintPreview.TabIndex = 4;
            this.gbPrintPreview.TabStop = false;
            this.gbPrintPreview.Text = "打印预览";
            this.gbPrintPreview.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(291, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(210, 261);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 5;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // pbPreview
            // 
            this.pbPreview.Location = new System.Drawing.Point(100, 76);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(240, 160);
            this.pbPreview.TabIndex = 4;
            this.pbPreview.TabStop = false;
            // 
            // btnDefaultPrinter
            // 
            this.btnDefaultPrinter.Location = new System.Drawing.Point(295, 47);
            this.btnDefaultPrinter.Name = "btnDefaultPrinter";
            this.btnDefaultPrinter.Size = new System.Drawing.Size(75, 23);
            this.btnDefaultPrinter.TabIndex = 3;
            this.btnDefaultPrinter.Text = "设为默认";
            this.btnDefaultPrinter.UseVisualStyleBackColor = true;
            this.btnDefaultPrinter.Click += new System.EventHandler(this.btnDefaultPrinter_Click);
            // 
            // cmbAllPrinter
            // 
            this.cmbAllPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllPrinter.FormattingEnabled = true;
            this.cmbAllPrinter.Location = new System.Drawing.Point(101, 50);
            this.cmbAllPrinter.Name = "cmbAllPrinter";
            this.cmbAllPrinter.Size = new System.Drawing.Size(171, 20);
            this.cmbAllPrinter.TabIndex = 2;
            // 
            // lblDefaultPrinter
            // 
            this.lblDefaultPrinter.AutoSize = true;
            this.lblDefaultPrinter.Location = new System.Drawing.Point(99, 26);
            this.lblDefaultPrinter.Name = "lblDefaultPrinter";
            this.lblDefaultPrinter.Size = new System.Drawing.Size(11, 12);
            this.lblDefaultPrinter.TabIndex = 1;
            this.lblDefaultPrinter.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "      效果：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "默认打印机：";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "选择";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "cInvCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "存货编码";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 90;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "cInvName";
            this.dataGridViewTextBoxColumn2.HeaderText = "存货名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "cInvStd";
            this.dataGridViewTextBoxColumn3.HeaderText = "规格";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "cMassUnitName";
            this.dataGridViewTextBoxColumn4.HeaderText = "保质期单位";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 90;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "iMassDate";
            this.dataGridViewTextBoxColumn5.HeaderText = "保质期数量";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 90;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "cBatch";
            this.dataGridViewTextBoxColumn6.HeaderText = "批次";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 110;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "iQuantity";
            this.dataGridViewTextBoxColumn7.HeaderText = "打印数量";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 526);
            this.Controls.Add(this.gbPrintPreview);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnPart);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.tabCategory);
            this.Name = "MainForm";
            this.Text = "标签打印";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabCategory.ResumeLayout(false);
            this.tpInventory.ResumeLayout(false);
            this.tpInventory.PerformLayout();
            this.tpPurchase.ResumeLayout(false);
            this.tpPurchase.PerformLayout();
            this.tpSaleLibrary.ResumeLayout(false);
            this.tpSaleLibrary.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.gbPrintPreview.ResumeLayout(false);
            this.gbPrintPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCategory;
        private System.Windows.Forms.TabPage tpInventory;
        private System.Windows.Forms.TabPage tpPurchase;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Button btnPart;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.TabPage tpSaleLibrary;
        private System.Windows.Forms.RadioButton rdoLittleLable;
        private System.Windows.Forms.RadioButton rdoBigLable;
        private System.Windows.Forms.TextBox txtInvName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInvCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInventorySearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbPrintPreview;
        private System.Windows.Forms.Button btnDefaultPrinter;
        private System.Windows.Forms.ComboBox cmbAllPrinter;
        private System.Windows.Forms.Label lblDefaultPrinter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.ComboBox cmbPuchaseList;
        private System.Windows.Forms.ComboBox cmbSaleDeliveryList;
        private System.Windows.Forms.CheckBox chkPrintDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInvCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInvStd;
        private System.Windows.Forms.DataGridViewTextBoxColumn iMassDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMassUintName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn iQuantity;
        private System.Windows.Forms.CheckBox chkDouble;
    }
}