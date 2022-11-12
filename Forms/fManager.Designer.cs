namespace PAYONEER
{
    partial class fManager
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grvAccountsTable = new System.Windows.Forms.DataGridView();
            this.btnAddNewAccounts = new System.Windows.Forms.Button();
            this.tboxSearch = new System.Windows.Forms.TextBox();
            this.btnFindEmail = new System.Windows.Forms.Button();
            this.cbox_chromeOFF = new System.Windows.Forms.CheckBox();
            this.btnDeleteAccounts = new System.Windows.Forms.Button();
            this.btnColumnSet = new System.Windows.Forms.Button();
            this.btnCheckAPI = new System.Windows.Forms.Button();
            this.combAccountCategory = new System.Windows.Forms.ComboBox();
            this.tbox_Account_Counting = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbox_setAcc_hold = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvAccountsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grvAccountsTable);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(-2, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1308, 570);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // grvAccountsTable
            // 
            this.grvAccountsTable.AllowUserToAddRows = false;
            this.grvAccountsTable.AllowUserToDeleteRows = false;
            this.grvAccountsTable.AllowUserToOrderColumns = true;
            this.grvAccountsTable.AllowUserToResizeRows = false;
            this.grvAccountsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvAccountsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvAccountsTable.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
            this.grvAccountsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grvAccountsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvAccountsTable.Location = new System.Drawing.Point(1, 21);
            this.grvAccountsTable.Name = "grvAccountsTable";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvAccountsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grvAccountsTable.RowHeadersWidth = 15;
            this.grvAccountsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvAccountsTable.Size = new System.Drawing.Size(1306, 543);
            this.grvAccountsTable.TabIndex = 0;
            this.grvAccountsTable.TabStop = false;
            // 
            // btnAddNewAccounts
            // 
            this.btnAddNewAccounts.AllowDrop = true;
            this.btnAddNewAccounts.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewAccounts.Location = new System.Drawing.Point(17, 9);
            this.btnAddNewAccounts.Name = "btnAddNewAccounts";
            this.btnAddNewAccounts.Size = new System.Drawing.Size(160, 29);
            this.btnAddNewAccounts.TabIndex = 3;
            this.btnAddNewAccounts.TabStop = false;
            this.btnAddNewAccounts.Text = "+ Thêm tài khoản mới";
            this.btnAddNewAccounts.UseVisualStyleBackColor = true;
            this.btnAddNewAccounts.Click += new System.EventHandler(this.btnAddNewAccounts_Click);
            // 
            // tboxSearch
            // 
            this.tboxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxSearch.Location = new System.Drawing.Point(518, 11);
            this.tboxSearch.Multiline = true;
            this.tboxSearch.Name = "tboxSearch";
            this.tboxSearch.Size = new System.Drawing.Size(297, 25);
            this.tboxSearch.TabIndex = 4;
            this.tboxSearch.TabStop = false;
            // 
            // btnFindEmail
            // 
            this.btnFindEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindEmail.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindEmail.Location = new System.Drawing.Point(823, 10);
            this.btnFindEmail.Name = "btnFindEmail";
            this.btnFindEmail.Size = new System.Drawing.Size(74, 27);
            this.btnFindEmail.TabIndex = 5;
            this.btnFindEmail.TabStop = false;
            this.btnFindEmail.Text = "Tìm kiếm";
            this.btnFindEmail.UseVisualStyleBackColor = true;
            this.btnFindEmail.Click += new System.EventHandler(this.btnFindEmail_Click);
            // 
            // cbox_chromeOFF
            // 
            this.cbox_chromeOFF.AutoSize = true;
            this.cbox_chromeOFF.Location = new System.Drawing.Point(18, 47);
            this.cbox_chromeOFF.Name = "cbox_chromeOFF";
            this.cbox_chromeOFF.Size = new System.Drawing.Size(157, 17);
            this.cbox_chromeOFF.TabIndex = 8;
            this.cbox_chromeOFF.Text = "Tự tắt Chrome khi hết phiên";
            this.cbox_chromeOFF.UseVisualStyleBackColor = true;
            // 
            // btnDeleteAccounts
            // 
            this.btnDeleteAccounts.AllowDrop = true;
            this.btnDeleteAccounts.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteAccounts.Location = new System.Drawing.Point(190, 9);
            this.btnDeleteAccounts.Name = "btnDeleteAccounts";
            this.btnDeleteAccounts.Size = new System.Drawing.Size(103, 29);
            this.btnDeleteAccounts.TabIndex = 9;
            this.btnDeleteAccounts.TabStop = false;
            this.btnDeleteAccounts.Text = "Xóa tài khoản";
            this.btnDeleteAccounts.UseVisualStyleBackColor = true;
            this.btnDeleteAccounts.Click += new System.EventHandler(this.btnDeleteAccounts_Click);
            // 
            // btnColumnSet
            // 
            this.btnColumnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColumnSet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColumnSet.Location = new System.Drawing.Point(1173, 9);
            this.btnColumnSet.Name = "btnColumnSet";
            this.btnColumnSet.Size = new System.Drawing.Size(107, 29);
            this.btnColumnSet.TabIndex = 10;
            this.btnColumnSet.Text = "Tùy chỉnh cột";
            this.btnColumnSet.UseVisualStyleBackColor = true;
            this.btnColumnSet.Click += new System.EventHandler(this.btnColumnSet_Click);
            // 
            // btnCheckAPI
            // 
            this.btnCheckAPI.AllowDrop = true;
            this.btnCheckAPI.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckAPI.Location = new System.Drawing.Point(306, 9);
            this.btnCheckAPI.Name = "btnCheckAPI";
            this.btnCheckAPI.Size = new System.Drawing.Size(87, 29);
            this.btnCheckAPI.TabIndex = 11;
            this.btnCheckAPI.TabStop = false;
            this.btnCheckAPI.Text = "Check API";
            this.btnCheckAPI.UseVisualStyleBackColor = true;
            this.btnCheckAPI.Click += new System.EventHandler(this.btnCheckAPI_Click);
            // 
            // combAccountCategory
            // 
            this.combAccountCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.combAccountCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combAccountCategory.FormattingEnabled = true;
            this.combAccountCategory.Items.AddRange(new object[] {
            "Tất cả",
            "Set Email",
            "Set PO",
            "Forward Email",
            "Up Link",
            "Đã set - all"});
            this.combAccountCategory.Location = new System.Drawing.Point(926, 12);
            this.combAccountCategory.MaxDropDownItems = 20;
            this.combAccountCategory.Name = "combAccountCategory";
            this.combAccountCategory.Size = new System.Drawing.Size(156, 24);
            this.combAccountCategory.TabIndex = 12;
            this.combAccountCategory.TabStop = false;
            this.combAccountCategory.Text = "Tất cả";
            this.combAccountCategory.SelectedIndexChanged += new System.EventHandler(this.combAccountCategory_SelectedIndexChanged);
            // 
            // tbox_Account_Counting
            // 
            this.tbox_Account_Counting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbox_Account_Counting.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbox_Account_Counting.Location = new System.Drawing.Point(1090, 12);
            this.tbox_Account_Counting.Name = "tbox_Account_Counting";
            this.tbox_Account_Counting.Size = new System.Drawing.Size(56, 24);
            this.tbox_Account_Counting.TabIndex = 13;
            this.tbox_Account_Counting.TabStop = false;
            this.tbox_Account_Counting.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // cbox_setAcc_hold
            // 
            this.cbox_setAcc_hold.AutoSize = true;
            this.cbox_setAcc_hold.Location = new System.Drawing.Point(191, 47);
            this.cbox_setAcc_hold.Name = "cbox_setAcc_hold";
            this.cbox_setAcc_hold.Size = new System.Drawing.Size(87, 17);
            this.cbox_setAcc_hold.TabIndex = 15;
            this.cbox_setAcc_hold.Text = "Set Acc hold";
            this.cbox_setAcc_hold.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(307, 47);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(50, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Copy";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // fManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 629);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbox_setAcc_hold);
            this.Controls.Add(this.tbox_Account_Counting);
            this.Controls.Add(this.combAccountCategory);
            this.Controls.Add(this.btnCheckAPI);
            this.Controls.Add(this.btnColumnSet);
            this.Controls.Add(this.btnDeleteAccounts);
            this.Controls.Add(this.cbox_chromeOFF);
            this.Controls.Add(this.btnFindEmail);
            this.Controls.Add(this.tboxSearch);
            this.Controls.Add(this.btnAddNewAccounts);
            this.Controls.Add(this.groupBox1);
            this.Name = "fManager";
            this.Text = "Quản lý Payoneer";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvAccountsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grvAccountsTable;
        private System.Windows.Forms.Button btnAddNewAccounts;
        private System.Windows.Forms.TextBox tboxSearch;
        private System.Windows.Forms.Button btnFindEmail;
        private System.Windows.Forms.CheckBox cbox_chromeOFF;
        private System.Windows.Forms.Button btnDeleteAccounts;
        private System.Windows.Forms.Button btnColumnSet;
        private System.Windows.Forms.Button btnCheckAPI;
        private System.Windows.Forms.ComboBox combAccountCategory;
        private System.Windows.Forms.TextBox tbox_Account_Counting;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.CheckBox cbox_setAcc_hold;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

