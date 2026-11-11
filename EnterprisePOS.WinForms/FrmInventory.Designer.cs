namespace EnterprisePOS.WinForms
{
    partial class FrmInventory
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
            this.lblInventoryTitle = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlInventoryList = new System.Windows.Forms.Panel();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.lblInventoryListTitle = new System.Windows.Forms.Label();
            this.pnlStockAdjustment = new System.Windows.Forms.Panel();
            this.btnSaveAdjustment = new System.Windows.Forms.Button();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.cboAdjustmentType = new System.Windows.Forms.ComboBox();
            this.lblAdjustmentType = new System.Windows.Forms.Label();
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblAdjustmentTitle = new System.Windows.Forms.Label();
            this.pnlInventoryHistory = new System.Windows.Forms.Panel();
            this.dgvInventoryHistory = new System.Windows.Forms.DataGridView();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.pnlSearch.SuspendLayout();
            this.pnlInventoryList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.pnlStockAdjustment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.pnlInventoryHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInventoryTitle
            // 
            this.lblInventoryTitle.AutoSize = true;
            this.lblInventoryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventoryTitle.Location = new System.Drawing.Point(36, 20);
            this.lblInventoryTitle.Name = "lblInventoryTitle";
            this.lblInventoryTitle.Size = new System.Drawing.Size(170, 13);
            this.lblInventoryTitle.TabIndex = 0;
            this.lblInventoryTitle.Text = "INVENTORY MANAGEMENT";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Location = new System.Drawing.Point(13, 59);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(684, 124);
            this.pnlSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(364, 78);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(29, 82);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(312, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(26, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search Products";
            // 
            // pnlInventoryList
            // 
            this.pnlInventoryList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlInventoryList.Controls.Add(this.dgvInventory);
            this.pnlInventoryList.Controls.Add(this.lblInventoryListTitle);
            this.pnlInventoryList.Location = new System.Drawing.Point(13, 220);
            this.pnlInventoryList.Name = "pnlInventoryList";
            this.pnlInventoryList.Size = new System.Drawing.Size(328, 295);
            this.pnlInventoryList.TabIndex = 2;
            this.pnlInventoryList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInventoryList_Paint);
            // 
            // dgvInventory
            // 
            this.dgvInventory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Location = new System.Drawing.Point(17, 47);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.Size = new System.Drawing.Size(297, 229);
            this.dgvInventory.TabIndex = 1;
            this.dgvInventory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventory_CellContentClick);
            // 
            // lblInventoryListTitle
            // 
            this.lblInventoryListTitle.AutoSize = true;
            this.lblInventoryListTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventoryListTitle.Location = new System.Drawing.Point(14, 15);
            this.lblInventoryListTitle.Name = "lblInventoryListTitle";
            this.lblInventoryListTitle.Size = new System.Drawing.Size(143, 13);
            this.lblInventoryListTitle.TabIndex = 0;
            this.lblInventoryListTitle.Text = "CURRENT INVENTORY";
            this.lblInventoryListTitle.Click += new System.EventHandler(this.lblInventoryListTitle_Click);
            // 
            // pnlStockAdjustment
            // 
            this.pnlStockAdjustment.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlStockAdjustment.Controls.Add(this.btnSaveAdjustment);
            this.pnlStockAdjustment.Controls.Add(this.txtReason);
            this.pnlStockAdjustment.Controls.Add(this.lblReason);
            this.pnlStockAdjustment.Controls.Add(this.nudQuantity);
            this.pnlStockAdjustment.Controls.Add(this.lblQuantity);
            this.pnlStockAdjustment.Controls.Add(this.cboAdjustmentType);
            this.pnlStockAdjustment.Controls.Add(this.lblAdjustmentType);
            this.pnlStockAdjustment.Controls.Add(this.cboProduct);
            this.pnlStockAdjustment.Controls.Add(this.lblProduct);
            this.pnlStockAdjustment.Controls.Add(this.lblAdjustmentTitle);
            this.pnlStockAdjustment.Location = new System.Drawing.Point(347, 220);
            this.pnlStockAdjustment.Name = "pnlStockAdjustment";
            this.pnlStockAdjustment.Size = new System.Drawing.Size(350, 295);
            this.pnlStockAdjustment.TabIndex = 3;
            this.pnlStockAdjustment.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStockAdjustment_Paint);
            // 
            // btnSaveAdjustment
            // 
            this.btnSaveAdjustment.Location = new System.Drawing.Point(23, 253);
            this.btnSaveAdjustment.Name = "btnSaveAdjustment";
            this.btnSaveAdjustment.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAdjustment.TabIndex = 9;
            this.btnSaveAdjustment.Text = "SAVE ADJUSTMENT";
            this.btnSaveAdjustment.UseVisualStyleBackColor = true;
            this.btnSaveAdjustment.Click += new System.EventHandler(this.btnSaveAdjustment_Click);
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(23, 210);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(117, 20);
            this.txtReason.TabIndex = 8;
            this.txtReason.TextChanged += new System.EventHandler(this.txtReason_TextChanged);
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(20, 193);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(44, 13);
            this.lblReason.TabIndex = 7;
            this.lblReason.Text = "Reason";
            this.lblReason.Click += new System.EventHandler(this.lblReason_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(20, 166);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(120, 20);
            this.nudQuantity.TabIndex = 6;
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(20, 149);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(46, 13);
            this.lblQuantity.TabIndex = 5;
            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.Click += new System.EventHandler(this.lblQuantity_Click);
            // 
            // cboAdjustmentType
            // 
            this.cboAdjustmentType.FormattingEnabled = true;
            this.cboAdjustmentType.Location = new System.Drawing.Point(17, 121);
            this.cboAdjustmentType.Name = "cboAdjustmentType";
            this.cboAdjustmentType.Size = new System.Drawing.Size(121, 21);
            this.cboAdjustmentType.TabIndex = 4;
            this.cboAdjustmentType.SelectedIndexChanged += new System.EventHandler(this.cboAdjustmentType_SelectedIndexChanged);
            // 
            // lblAdjustmentType
            // 
            this.lblAdjustmentType.AutoSize = true;
            this.lblAdjustmentType.Location = new System.Drawing.Point(17, 104);
            this.lblAdjustmentType.Name = "lblAdjustmentType";
            this.lblAdjustmentType.Size = new System.Drawing.Size(86, 13);
            this.lblAdjustmentType.TabIndex = 3;
            this.lblAdjustmentType.Text = "Adjustment Type";
            this.lblAdjustmentType.Click += new System.EventHandler(this.lblAdjustmentType_Click);
            // 
            // cboProduct
            // 
            this.cboProduct.FormattingEnabled = true;
            this.cboProduct.Location = new System.Drawing.Point(17, 76);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(121, 21);
            this.cboProduct.TabIndex = 2;
            this.cboProduct.SelectedIndexChanged += new System.EventHandler(this.cboProduct_SelectedIndexChanged);
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(14, 47);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 1;
            this.lblProduct.Text = "Product";
            this.lblProduct.Click += new System.EventHandler(this.lblProduct_Click);
            // 
            // lblAdjustmentTitle
            // 
            this.lblAdjustmentTitle.AutoSize = true;
            this.lblAdjustmentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustmentTitle.Location = new System.Drawing.Point(14, 15);
            this.lblAdjustmentTitle.Name = "lblAdjustmentTitle";
            this.lblAdjustmentTitle.Size = new System.Drawing.Size(135, 13);
            this.lblAdjustmentTitle.TabIndex = 0;
            this.lblAdjustmentTitle.Text = "STOCK ADJUSTMENT";
            this.lblAdjustmentTitle.Click += new System.EventHandler(this.lblAdjustmentTitle_Click);
            // 
            // pnlInventoryHistory
            // 
            this.pnlInventoryHistory.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlInventoryHistory.Controls.Add(this.dgvInventoryHistory);
            this.pnlInventoryHistory.Controls.Add(this.lblHistoryTitle);
            this.pnlInventoryHistory.Location = new System.Drawing.Point(13, 535);
            this.pnlInventoryHistory.Name = "pnlInventoryHistory";
            this.pnlInventoryHistory.Size = new System.Drawing.Size(684, 304);
            this.pnlInventoryHistory.TabIndex = 4;
            this.pnlInventoryHistory.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInventoryHistory_Paint);
            // 
            // dgvInventoryHistory
            // 
            this.dgvInventoryHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInventoryHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventoryHistory.Location = new System.Drawing.Point(17, 49);
            this.dgvInventoryHistory.Name = "dgvInventoryHistory";
            this.dgvInventoryHistory.Size = new System.Drawing.Size(651, 239);
            this.dgvInventoryHistory.TabIndex = 1;
            this.dgvInventoryHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventoryHistory_CellContentClick);
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoryTitle.Location = new System.Drawing.Point(14, 13);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(212, 13);
            this.lblHistoryTitle.TabIndex = 0;
            this.lblHistoryTitle.Text = "INVENTORY MOVEMENT HISTORY";
            this.lblHistoryTitle.Click += new System.EventHandler(this.lblHistoryTitle_Click);
            // 
            // FrmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 851);
            this.Controls.Add(this.pnlInventoryHistory);
            this.Controls.Add(this.pnlStockAdjustment);
            this.Controls.Add(this.pnlInventoryList);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.lblInventoryTitle);
            this.Name = "FrmInventory";
            this.Text = "FrmInventory";
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlInventoryList.ResumeLayout(false);
            this.pnlInventoryList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.pnlStockAdjustment.ResumeLayout(false);
            this.pnlStockAdjustment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.pnlInventoryHistory.ResumeLayout(false);
            this.pnlInventoryHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInventoryTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlInventoryList;
        private System.Windows.Forms.Panel pnlStockAdjustment;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Label lblInventoryListTitle;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.ComboBox cboAdjustmentType;
        private System.Windows.Forms.Label lblAdjustmentType;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblAdjustmentTitle;
        private System.Windows.Forms.Button btnSaveAdjustment;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Panel pnlInventoryHistory;
        private System.Windows.Forms.DataGridView dgvInventoryHistory;
        private System.Windows.Forms.Label lblHistoryTitle;
    }
}