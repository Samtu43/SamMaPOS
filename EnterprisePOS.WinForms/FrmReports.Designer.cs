namespace EnterprisePOS.WinForms
{
    partial class FrmReports
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
            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblReportsTitle = new System.Windows.Forms.Label();
            this.pnlTotalSales = new System.Windows.Forms.Panel();
            this.pnlTotalOrders = new System.Windows.Forms.Panel();
            this.pnlAverageSale = new System.Windows.Forms.Panel();
            this.lblTotalSalesTitle = new System.Windows.Forms.Label();
            this.lblTotalSalesValue = new System.Windows.Forms.Label();
            this.lblTotalOrdersTitle = new System.Windows.Forms.Label();
            this.lblTotalOrdersValue = new System.Windows.Forms.Label();
            this.lblAverageSaleTitle = new System.Windows.Forms.Label();
            this.lblAverageSaleValue = new System.Windows.Forms.Label();
            this.pnlSalesReport = new System.Windows.Forms.Panel();
            this.lblSalesReportTitle = new System.Windows.Forms.Label();
            this.dgvSalesReport = new System.Windows.Forms.DataGridView();
            this.pnlFilters.SuspendLayout();
            this.pnlTotalSales.SuspendLayout();
            this.pnlTotalOrders.SuspendLayout();
            this.pnlAverageSale.SuspendLayout();
            this.pnlSalesReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(24, 58);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(56, 13);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.Click += new System.EventHandler(this.lblFromDate_Click);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(291, 18);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(46, 13);
            this.lblToDate.TabIndex = 1;
            this.lblToDate.Text = "To Date";
            this.lblToDate.Click += new System.EventHandler(this.lblToDate_Click);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(27, 98);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(200, 20);
            this.dtpFromDate.TabIndex = 2;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(294, 57);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(200, 20);
            this.dtpToDate.TabIndex = 3;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(27, 134);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(130, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "GENERATE REPORT";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(294, 94);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "CLEAR FILTERS";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlFilters.Controls.Add(this.dtpToDate);
            this.pnlFilters.Controls.Add(this.lblToDate);
            this.pnlFilters.Controls.Add(this.btnClear);
            this.pnlFilters.Location = new System.Drawing.Point(12, 41);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(526, 136);
            this.pnlFilters.TabIndex = 6;
            this.pnlFilters.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFilters_Paint);
            // 
            // lblReportsTitle
            // 
            this.lblReportsTitle.AutoSize = true;
            this.lblReportsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportsTitle.Location = new System.Drawing.Point(13, 13);
            this.lblReportsTitle.Name = "lblReportsTitle";
            this.lblReportsTitle.Size = new System.Drawing.Size(109, 13);
            this.lblReportsTitle.TabIndex = 7;
            this.lblReportsTitle.Text = "SALES REPORTS";
            this.lblReportsTitle.Click += new System.EventHandler(this.lblReportsTitle_Click);
            // 
            // pnlTotalSales
            // 
            this.pnlTotalSales.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlTotalSales.Controls.Add(this.lblTotalSalesValue);
            this.pnlTotalSales.Controls.Add(this.lblTotalSalesTitle);
            this.pnlTotalSales.Location = new System.Drawing.Point(16, 200);
            this.pnlTotalSales.Name = "pnlTotalSales";
            this.pnlTotalSales.Size = new System.Drawing.Size(168, 69);
            this.pnlTotalSales.TabIndex = 8;
            this.pnlTotalSales.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTotalSales_Paint);
            // 
            // pnlTotalOrders
            // 
            this.pnlTotalOrders.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlTotalOrders.Controls.Add(this.lblTotalOrdersValue);
            this.pnlTotalOrders.Controls.Add(this.lblTotalOrdersTitle);
            this.pnlTotalOrders.Location = new System.Drawing.Point(191, 200);
            this.pnlTotalOrders.Name = "pnlTotalOrders";
            this.pnlTotalOrders.Size = new System.Drawing.Size(172, 69);
            this.pnlTotalOrders.TabIndex = 9;
            this.pnlTotalOrders.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTotalOrders_Paint);
            // 
            // pnlAverageSale
            // 
            this.pnlAverageSale.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlAverageSale.Controls.Add(this.lblAverageSaleValue);
            this.pnlAverageSale.Controls.Add(this.lblAverageSaleTitle);
            this.pnlAverageSale.Location = new System.Drawing.Point(370, 200);
            this.pnlAverageSale.Name = "pnlAverageSale";
            this.pnlAverageSale.Size = new System.Drawing.Size(168, 69);
            this.pnlAverageSale.TabIndex = 10;
            this.pnlAverageSale.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAverageSale_Paint);
            // 
            // lblTotalSalesTitle
            // 
            this.lblTotalSalesTitle.AutoSize = true;
            this.lblTotalSalesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSalesTitle.Location = new System.Drawing.Point(8, 10);
            this.lblTotalSalesTitle.Name = "lblTotalSalesTitle";
            this.lblTotalSalesTitle.Size = new System.Drawing.Size(94, 13);
            this.lblTotalSalesTitle.TabIndex = 0;
            this.lblTotalSalesTitle.Text = "TOTAL SALES ";
            this.lblTotalSalesTitle.Click += new System.EventHandler(this.lblTotalSalesTitle_Click);
            // 
            // lblTotalSalesValue
            // 
            this.lblTotalSalesValue.AutoSize = true;
            this.lblTotalSalesValue.Location = new System.Drawing.Point(8, 39);
            this.lblTotalSalesValue.Name = "lblTotalSalesValue";
            this.lblTotalSalesValue.Size = new System.Drawing.Size(35, 13);
            this.lblTotalSalesValue.TabIndex = 1;
            this.lblTotalSalesValue.Text = "₱0.00";
            this.lblTotalSalesValue.Click += new System.EventHandler(this.lblTotalSalesValue_Click);
            // 
            // lblTotalOrdersTitle
            // 
            this.lblTotalOrdersTitle.AutoSize = true;
            this.lblTotalOrdersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrdersTitle.Location = new System.Drawing.Point(4, 9);
            this.lblTotalOrdersTitle.Name = "lblTotalOrdersTitle";
            this.lblTotalOrdersTitle.Size = new System.Drawing.Size(103, 13);
            this.lblTotalOrdersTitle.TabIndex = 0;
            this.lblTotalOrdersTitle.Text = "TOTAL ORDERS";
            this.lblTotalOrdersTitle.Click += new System.EventHandler(this.lblTotalOrdersTitle_Click);
            // 
            // lblTotalOrdersValue
            // 
            this.lblTotalOrdersValue.AutoSize = true;
            this.lblTotalOrdersValue.Location = new System.Drawing.Point(7, 38);
            this.lblTotalOrdersValue.Name = "lblTotalOrdersValue";
            this.lblTotalOrdersValue.Size = new System.Drawing.Size(13, 13);
            this.lblTotalOrdersValue.TabIndex = 1;
            this.lblTotalOrdersValue.Text = "0";
            this.lblTotalOrdersValue.Click += new System.EventHandler(this.lblTotalOrdersValue_Click);
            // 
            // lblAverageSaleTitle
            // 
            this.lblAverageSaleTitle.AutoSize = true;
            this.lblAverageSaleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageSaleTitle.Location = new System.Drawing.Point(12, 10);
            this.lblAverageSaleTitle.Name = "lblAverageSaleTitle";
            this.lblAverageSaleTitle.Size = new System.Drawing.Size(100, 13);
            this.lblAverageSaleTitle.TabIndex = 0;
            this.lblAverageSaleTitle.Text = "AVERAGE SALE";
            this.lblAverageSaleTitle.Click += new System.EventHandler(this.lblAverageSaleTitle_Click);
            // 
            // lblAverageSaleValue
            // 
            this.lblAverageSaleValue.AutoSize = true;
            this.lblAverageSaleValue.Location = new System.Drawing.Point(15, 38);
            this.lblAverageSaleValue.Name = "lblAverageSaleValue";
            this.lblAverageSaleValue.Size = new System.Drawing.Size(35, 13);
            this.lblAverageSaleValue.TabIndex = 1;
            this.lblAverageSaleValue.Text = "₱0.00";
            this.lblAverageSaleValue.Click += new System.EventHandler(this.lblAverageSaleValue_Click);
            // 
            // pnlSalesReport
            // 
            this.pnlSalesReport.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlSalesReport.Controls.Add(this.dgvSalesReport);
            this.pnlSalesReport.Controls.Add(this.lblSalesReportTitle);
            this.pnlSalesReport.Location = new System.Drawing.Point(16, 295);
            this.pnlSalesReport.Name = "pnlSalesReport";
            this.pnlSalesReport.Size = new System.Drawing.Size(522, 243);
            this.pnlSalesReport.TabIndex = 11;
            this.pnlSalesReport.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSalesReport_Paint);
            // 
            // lblSalesReportTitle
            // 
            this.lblSalesReportTitle.AutoSize = true;
            this.lblSalesReportTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesReportTitle.Location = new System.Drawing.Point(8, 14);
            this.lblSalesReportTitle.Name = "lblSalesReportTitle";
            this.lblSalesReportTitle.Size = new System.Drawing.Size(146, 13);
            this.lblSalesReportTitle.TabIndex = 0;
            this.lblSalesReportTitle.Text = "SALES TRANSACTIONS";
            this.lblSalesReportTitle.Click += new System.EventHandler(this.lblSalesReportTitle_Click);
            // 
            // dgvSalesReport
            // 
            this.dgvSalesReport.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSalesReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesReport.Location = new System.Drawing.Point(11, 40);
            this.dgvSalesReport.Name = "dgvSalesReport";
            this.dgvSalesReport.Size = new System.Drawing.Size(497, 187);
            this.dgvSalesReport.TabIndex = 1;
            this.dgvSalesReport.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSalesReport_CellContentClick);
            // 
            // FrmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 551);
            this.Controls.Add(this.pnlSalesReport);
            this.Controls.Add(this.pnlAverageSale);
            this.Controls.Add(this.pnlTotalOrders);
            this.Controls.Add(this.pnlTotalSales);
            this.Controls.Add(this.lblReportsTitle);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.pnlFilters);
            this.Name = "FrmReports";
            this.Text = "FrmReports";
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlTotalSales.ResumeLayout(false);
            this.pnlTotalSales.PerformLayout();
            this.pnlTotalOrders.ResumeLayout(false);
            this.pnlTotalOrders.PerformLayout();
            this.pnlAverageSale.ResumeLayout(false);
            this.pnlAverageSale.PerformLayout();
            this.pnlSalesReport.ResumeLayout(false);
            this.pnlSalesReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblReportsTitle;
        private System.Windows.Forms.Panel pnlTotalSales;
        private System.Windows.Forms.Label lblTotalSalesTitle;
        private System.Windows.Forms.Panel pnlTotalOrders;
        private System.Windows.Forms.Panel pnlAverageSale;
        private System.Windows.Forms.Label lblTotalSalesValue;
        private System.Windows.Forms.Label lblTotalOrdersValue;
        private System.Windows.Forms.Label lblTotalOrdersTitle;
        private System.Windows.Forms.Label lblAverageSaleValue;
        private System.Windows.Forms.Label lblAverageSaleTitle;
        private System.Windows.Forms.Panel pnlSalesReport;
        private System.Windows.Forms.DataGridView dgvSalesReport;
        private System.Windows.Forms.Label lblSalesReportTitle;
    }
}