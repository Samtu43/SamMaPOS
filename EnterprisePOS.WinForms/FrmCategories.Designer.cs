namespace EnterprisePOS.WinForms
{
    partial class FrmCategories
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
            this.lblCategoriesTitle = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlCategoryList = new System.Windows.Forms.Panel();
            this.pnlCategoryDetails = new System.Windows.Forms.Panel();
            this.lblCategoryListTitle = new System.Windows.Forms.Label();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.lblCategoryDetailsTitle = new System.Windows.Forms.Label();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlSearch.SuspendLayout();
            this.pnlCategoryList.SuspendLayout();
            this.pnlCategoryDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCategoriesTitle
            // 
            this.lblCategoriesTitle.AutoSize = true;
            this.lblCategoriesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoriesTitle.Location = new System.Drawing.Point(12, 19);
            this.lblCategoriesTitle.Name = "lblCategoriesTitle";
            this.lblCategoriesTitle.Size = new System.Drawing.Size(165, 13);
            this.lblCategoriesTitle.TabIndex = 0;
            this.lblCategoriesTitle.Text = "CATEGORY MANAGEMENT";
            this.lblCategoriesTitle.Click += new System.EventHandler(this.lblCategoriesTitle_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Location = new System.Drawing.Point(15, 35);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(641, 110);
            this.pnlSearch.TabIndex = 1;
            this.pnlSearch.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSearch_Paint);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(13, 13);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(111, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search Categories";
            this.lblSearch.Click += new System.EventHandler(this.lblSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(16, 57);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(231, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(309, 54);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlCategoryList
            // 
            this.pnlCategoryList.Controls.Add(this.dgvCategories);
            this.pnlCategoryList.Controls.Add(this.lblCategoryListTitle);
            this.pnlCategoryList.Location = new System.Drawing.Point(15, 176);
            this.pnlCategoryList.Name = "pnlCategoryList";
            this.pnlCategoryList.Size = new System.Drawing.Size(313, 391);
            this.pnlCategoryList.TabIndex = 2;
            this.pnlCategoryList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCategoryList_Paint);
            // 
            // pnlCategoryDetails
            // 
            this.pnlCategoryDetails.Controls.Add(this.btnClear);
            this.pnlCategoryDetails.Controls.Add(this.btnDelete);
            this.pnlCategoryDetails.Controls.Add(this.btnUpdate);
            this.pnlCategoryDetails.Controls.Add(this.btnAdd);
            this.pnlCategoryDetails.Controls.Add(this.txtDescription);
            this.pnlCategoryDetails.Controls.Add(this.lblDescription);
            this.pnlCategoryDetails.Controls.Add(this.txtCategoryName);
            this.pnlCategoryDetails.Controls.Add(this.lblCategoryName);
            this.pnlCategoryDetails.Controls.Add(this.lblCategoryDetailsTitle);
            this.pnlCategoryDetails.Location = new System.Drawing.Point(334, 176);
            this.pnlCategoryDetails.Name = "pnlCategoryDetails";
            this.pnlCategoryDetails.Size = new System.Drawing.Size(322, 391);
            this.pnlCategoryDetails.TabIndex = 3;
            this.pnlCategoryDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCategoryDetails_Paint);
            // 
            // lblCategoryListTitle
            // 
            this.lblCategoryListTitle.AutoSize = true;
            this.lblCategoryListTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryListTitle.Location = new System.Drawing.Point(16, 15);
            this.lblCategoryListTitle.Name = "lblCategoryListTitle";
            this.lblCategoryListTitle.Size = new System.Drawing.Size(105, 13);
            this.lblCategoryListTitle.TabIndex = 0;
            this.lblCategoryListTitle.Text = "CATEGORY LIST";
            this.lblCategoryListTitle.Click += new System.EventHandler(this.lblCategoryListTitle_Click);
            // 
            // dgvCategories
            // 
            this.dgvCategories.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Location = new System.Drawing.Point(16, 44);
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.Size = new System.Drawing.Size(284, 334);
            this.dgvCategories.TabIndex = 1;
            this.dgvCategories.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategories_CellContentClick);
            // 
            // lblCategoryDetailsTitle
            // 
            this.lblCategoryDetailsTitle.AutoSize = true;
            this.lblCategoryDetailsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryDetailsTitle.Location = new System.Drawing.Point(16, 15);
            this.lblCategoryDetailsTitle.Name = "lblCategoryDetailsTitle";
            this.lblCategoryDetailsTitle.Size = new System.Drawing.Size(130, 13);
            this.lblCategoryDetailsTitle.TabIndex = 0;
            this.lblCategoryDetailsTitle.Text = "CATEGORY DETAILS";
            this.lblCategoryDetailsTitle.Click += new System.EventHandler(this.lblCategoryDetailsTitle_Click);
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(16, 58);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(80, 13);
            this.lblCategoryName.TabIndex = 1;
            this.lblCategoryName.Text = "Category Name";
            this.lblCategoryName.Click += new System.EventHandler(this.lblCategoryName_Click);
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(19, 88);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(239, 20);
            this.txtCategoryName.TabIndex = 2;
            this.txtCategoryName.TextChanged += new System.EventHandler(this.txtCategoryName_TextChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(19, 132);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description";
            this.lblDescription.Click += new System.EventHandler(this.lblDescription_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(19, 160);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(239, 73);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(22, 266);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(118, 266);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(22, 306);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(118, 305);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FrmCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 579);
            this.Controls.Add(this.pnlCategoryDetails);
            this.Controls.Add(this.pnlCategoryList);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.lblCategoriesTitle);
            this.Name = "FrmCategories";
            this.Text = "FrmCategories";
            this.Load += new System.EventHandler(this.FrmCategories_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlCategoryList.ResumeLayout(false);
            this.pnlCategoryList.PerformLayout();
            this.pnlCategoryDetails.ResumeLayout(false);
            this.pnlCategoryDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCategoriesTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlCategoryList;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.Label lblCategoryListTitle;
        private System.Windows.Forms.Panel pnlCategoryDetails;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.Label lblCategoryDetailsTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
    }
}