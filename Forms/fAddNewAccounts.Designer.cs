namespace PAYONEER.Forms
{
    partial class fAddNewAccounts
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grvAddNewAccounts = new System.Windows.Forms.DataGridView();
            this.btnOpenExcelFile = new System.Windows.Forms.Button();
            this.google_sheet_BTN = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvAddNewAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grvAddNewAccounts);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(-2, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(1140, 554);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // grvAddNewAccounts
            // 
            this.grvAddNewAccounts.AllowUserToAddRows = false;
            this.grvAddNewAccounts.AllowUserToDeleteRows = false;
            this.grvAddNewAccounts.AllowUserToResizeRows = false;
            this.grvAddNewAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvAddNewAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvAddNewAccounts.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
            this.grvAddNewAccounts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grvAddNewAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvAddNewAccounts.Location = new System.Drawing.Point(2, 20);
            this.grvAddNewAccounts.Name = "grvAddNewAccounts";
            this.grvAddNewAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grvAddNewAccounts.RowHeadersWidth = 50;
            this.grvAddNewAccounts.Size = new System.Drawing.Size(1138, 531);
            this.grvAddNewAccounts.TabIndex = 2;
            // 
            // btnOpenExcelFile
            // 
            this.btnOpenExcelFile.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenExcelFile.Location = new System.Drawing.Point(12, 11);
            this.btnOpenExcelFile.Name = "btnOpenExcelFile";
            this.btnOpenExcelFile.Size = new System.Drawing.Size(148, 34);
            this.btnOpenExcelFile.TabIndex = 3;
            this.btnOpenExcelFile.TabStop = false;
            this.btnOpenExcelFile.Text = "+ Nhập từ file Exel";
            this.btnOpenExcelFile.UseVisualStyleBackColor = true;
            this.btnOpenExcelFile.Click += new System.EventHandler(this.btnOpenExcelFile_Click);
            // 
            // google_sheet_BTN
            // 
            this.google_sheet_BTN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.google_sheet_BTN.Location = new System.Drawing.Point(175, 11);
            this.google_sheet_BTN.Name = "google_sheet_BTN";
            this.google_sheet_BTN.Size = new System.Drawing.Size(190, 34);
            this.google_sheet_BTN.TabIndex = 4;
            this.google_sheet_BTN.TabStop = false;
            this.google_sheet_BTN.Text = "+ Nhập từ Google Sheet";
            this.google_sheet_BTN.UseVisualStyleBackColor = true;
            this.google_sheet_BTN.Click += new System.EventHandler(this.google_sheet_BTN_Click);
            // 
            // fAddNewAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 601);
            this.Controls.Add(this.google_sheet_BTN);
            this.Controls.Add(this.btnOpenExcelFile);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1152, 640);
            this.Name = "fAddNewAccounts";
            this.Text = "Thêm tài khoản mới";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvAddNewAccounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grvAddNewAccounts;
        private System.Windows.Forms.Button btnOpenExcelFile;
        private System.Windows.Forms.Button google_sheet_BTN;
    }
}