namespace EnterprisePOS.WinForms
{
    partial class FrmOrders
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
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlOrdersList = new System.Windows.Forms.Panel();
            this.lblOrdersListTitle = new System.Windows.Forms.Label();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.pnlOrderDetails = new System.Windows.Forms.Panel();
            this.lblOrderDetailsTitle = new System.Windows.Forms.Label();
            this.lblSelectedOrder = new System.Windows.Forms.Label();
            this.lblOrderTotal = new System.Windows.Forms.Label();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.pnlFilters.SuspendLayout();
            this.pnlOrdersList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.pnlOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdersTitle.Location = new System.Drawing.Point(12, 25);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(142, 13);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "ORDER MANAGEMENT";
            this.lblOrdersTitle.Click += new System.EventHandler(this.lblOrdersTitle_Click);
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlFilters.Controls.Add(this.btnClear);
            this.pnlFilters.Controls.Add(this.btnSearch);
            this.pnlFilters.Controls.Add(this.dtpToDate);
            this.pnlFilters.Controls.Add(this.dtpFromDate);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.lblToDate);
            this.pnlFilters.Controls.Add(this.lblFromDate);
            this.pnlFilters.Controls.Add(this.lblSearch);
            this.pnlFilters.Location = new System.Drawing.Point(15, 69);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(591, 144);
            this.pnlFilters.TabIndex = 1;
            this.pnlFilters.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFilters_Paint);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(13, 20);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(75, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search Orders";
            this.lblSearch.Click += new System.EventHandler(this.lblSearch_Click);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(138, 20);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(56, 13);
            this.lblFromDate.TabIndex = 1;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.Click += new System.EventHandler(this.lblFromDate_Click);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(363, 20);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(46, 13);
            this.lblToDate.TabIndex = 2;
            this.lblToDate.Text = "To Date";
            this.lblToDate.Click += new System.EventHandler(this.lblToDate_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(16, 61);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(141, 58);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(200, 20);
            this.dtpFromDate.TabIndex = 4;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(366, 58);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(200, 20);
            this.dtpToDate.TabIndex = 5;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(16, 108);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(119, 108);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "CLEAR FILTERS";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // pnlOrdersList
            // 
            this.pnlOrdersList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlOrdersList.Controls.Add(this.dgvOrders);
            this.pnlOrdersList.Controls.Add(this.lblOrdersListTitle);
            this.pnlOrdersList.Location = new System.Drawing.Point(15, 231);
            this.pnlOrdersList.Name = "pnlOrdersList";
            this.pnlOrdersList.Size = new System.Drawing.Size(591, 251);
            this.pnlOrdersList.TabIndex = 3;
            this.pnlOrdersList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlOrdersList_Paint);
            // 
            // lblOrdersListTitle
            // 
            this.lblOrdersListTitle.AutoSize = true;
            this.lblOrdersListTitle.Location = new System.Drawing.Point(16, 15);
            this.lblOrdersListTitle.Name = "lblOrdersListTitle";
            this.lblOrdersListTitle.Size = new System.Drawing.Size(72, 13);
            this.lblOrdersListTitle.TabIndex = 0;
            this.lblOrdersListTitle.Text = "ORDER LIST";
            this.lblOrdersListTitle.Click += new System.EventHandler(this.lblOrdersListTitle_Click);
            // 
            // dgvOrders
            // 
            this.dgvOrders.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(19, 48);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(557, 190);
            this.dgvOrders.TabIndex = 1;
            this.dgvOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellContentClick);
            // 
            // pnlOrderDetails
            // 
            this.pnlOrderDetails.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlOrderDetails.Controls.Add(this.dgvOrderDetails);
            this.pnlOrderDetails.Controls.Add(this.lblOrderTotal);
            this.pnlOrderDetails.Controls.Add(this.lblSelectedOrder);
            this.pnlOrderDetails.Controls.Add(this.lblOrderDetailsTitle);
            this.pnlOrderDetails.Location = new System.Drawing.Point(15, 509);
            this.pnlOrderDetails.Name = "pnlOrderDetails";
            this.pnlOrderDetails.Size = new System.Drawing.Size(591, 325);
            this.pnlOrderDetails.TabIndex = 4;
            this.pnlOrderDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlOrderDetails_Paint);
            // 
            // lblOrderDetailsTitle
            // 
            this.lblOrderDetailsTitle.AutoSize = true;
            this.lblOrderDetailsTitle.Location = new System.Drawing.Point(19, 14);
            this.lblOrderDetailsTitle.Name = "lblOrderDetailsTitle";
            this.lblOrderDetailsTitle.Size = new System.Drawing.Size(94, 13);
            this.lblOrderDetailsTitle.TabIndex = 0;
            this.lblOrderDetailsTitle.Text = "ORDER DETAILS";
            this.lblOrderDetailsTitle.Click += new System.EventHandler(this.lblOrderDetailsTitle_Click);
            // 
            // lblSelectedOrder
            // 
            this.lblSelectedOrder.AutoSize = true;
            this.lblSelectedOrder.Location = new System.Drawing.Point(19, 45);
            this.lblSelectedOrder.Name = "lblSelectedOrder";
            this.lblSelectedOrder.Size = new System.Drawing.Size(62, 13);
            this.lblSelectedOrder.TabIndex = 1;
            this.lblSelectedOrder.Text = "Order ID: — ";
            this.lblSelectedOrder.Click += new System.EventHandler(this.lblSelectedOrder_Click);
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.AutoSize = true;
            this.lblOrderTotal.Location = new System.Drawing.Point(178, 45);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new System.Drawing.Size(65, 13);
            this.lblOrderTotal.TabIndex = 2;
            this.lblOrderTotal.Text = "Total: ₱0.00";
            this.lblOrderTotal.Click += new System.EventHandler(this.lblOrderTotal_Click);
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Location = new System.Drawing.Point(16, 75);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.Size = new System.Drawing.Size(550, 232);
            this.dgvOrderDetails.TabIndex = 3;
            this.dgvOrderDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderDetails_CellContentClick);
            // 
            // FrmOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 846);
            this.Controls.Add(this.pnlOrderDetails);
            this.Controls.Add(this.pnlOrdersList);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.lblOrdersTitle);
            this.Name = "FrmOrders";
            this.Text = "FrmOrders";
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlOrdersList.ResumeLayout(false);
            this.pnlOrdersList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.pnlOrderDetails.ResumeLayout(false);
            this.pnlOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlOrdersList;
        private System.Windows.Forms.Label lblOrdersListTitle;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Panel pnlOrderDetails;
        private System.Windows.Forms.Label lblSelectedOrder;
        private System.Windows.Forms.Label lblOrderDetailsTitle;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Label lblOrderTotal;
    }
}