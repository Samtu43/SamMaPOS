
using EnterprisePOS.Data;
using EnterprisePOS.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EnterprisePOS.WinForms
{
    public partial class FrmProducts : Form
    {
        private readonly ProductService productService;
        private readonly int currentUserId;
        private int selectedProductId;

        public FrmProducts() : this(0)
        {
        }

        public FrmProducts(int userId)
        {
            InitializeComponent();

            productService = new ProductService();
            currentUserId = userId;
            selectedProductId = 0;

            Load += FrmProducts_Load;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        }

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            try
            {
                dgvProducts.ReadOnly = true;
                dgvProducts.MultiSelect = false;
                dgvProducts.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
                dgvProducts.AllowUserToAddRows = false;
                dgvProducts.AutoGenerateColumns = true;

                cboCategory.DropDownStyle =
                    ComboBoxStyle.DropDownList;

                nudPrice.DecimalPlaces = 2;
                nudCost.DecimalPlaces = 2;

                nudPrice.Maximum = 1000000;
                nudCost.Maximum = 1000000;
                nudStock.Maximum = 100000;

                LoadCategories();
                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void LoadCategories()
        {
            cboCategory.DataSource =
                productService.GetAllCategories();

            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "CategoryId";
        }

        private void LoadProducts()
        {
            var products = productService.GetAllProducts()
                .Where(p => p.IsActive == true)
                .Select(p => new
                {
                    p.ProductId,
                    p.Sku,
                    p.Name,
                    Category = p.Category != null
                        ? p.Category.Name
                        : "",
                    p.CategoryId,
                    p.Price,
                    p.Cost,
                    p.StockQty
                })
                .ToList();

            dgvProducts.DataSource = products;

            ConfigureProductGrid();
        }

        private void SearchProducts()
        {
            var products = productService.SearchProducts(
                    txtSearch.Text.Trim())
                .Where(p => p.IsActive == true)
                .Select(p => new
                {
                    p.ProductId,
                    p.Sku,
                    p.Name,
                    Category = p.Category != null
                        ? p.Category.Name
                        : "",
                    p.CategoryId,
                    p.Price,
                    p.Cost,
                    p.StockQty
                })
                .ToList();

            dgvProducts.DataSource = products;

            ConfigureProductGrid();
        }

        private void ConfigureProductGrid()
        {
            if (dgvProducts.Columns["ProductId"] != null)
                dgvProducts.Columns["ProductId"].Visible = false;

            if (dgvProducts.Columns["CategoryId"] != null)
                dgvProducts.Columns["CategoryId"].Visible = false;

            if (dgvProducts.Columns["Sku"] != null)
                dgvProducts.Columns["Sku"].HeaderText = "SKU";

            if (dgvProducts.Columns["Name"] != null)
                dgvProducts.Columns["Name"].HeaderText =
                    "Product Name";

            if (dgvProducts.Columns["StockQty"] != null)
                dgvProducts.Columns["StockQty"].HeaderText =
                    "Stock";

            if (dgvProducts.Columns["Price"] != null)
                dgvProducts.Columns["Price"]
                    .DefaultCellStyle.Format = "N2";

            if (dgvProducts.Columns["Cost"] != null)
                dgvProducts.Columns["Cost"]
                    .DefaultCellStyle.Format = "N2";

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvProducts.ClearSelection();
        }

        private void ClearFields()
        {
            selectedProductId = 0;

            txtSKU.Clear();
            txtProductName.Clear();

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            nudPrice.Value = 0;
            nudCost.Value = 0;
            nudStock.Value = 0;

            nudStock.Enabled = true;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            dgvProducts.ClearSelection();
        }

        private int GetSelectedCategoryId()
        {
            if (cboCategory.SelectedValue == null)
                return 0;

            if (int.TryParse(
                cboCategory.SelectedValue.ToString(),
                out int categoryId))
            {
                return categoryId;
            }

            return 0;
        }

        private void SelectProduct()
        {
            if (dgvProducts.CurrentRow == null ||
                dgvProducts.CurrentRow.Index < 0 ||
                !dgvProducts.CurrentRow.Selected)
            {
                return;
            }

            var row = dgvProducts.CurrentRow;

            selectedProductId =
                Convert.ToInt32(row.Cells["ProductId"].Value);

            txtSKU.Text =
                Convert.ToString(row.Cells["Sku"].Value);

            txtProductName.Text =
                Convert.ToString(row.Cells["Name"].Value);

            cboCategory.SelectedValue =
                Convert.ToInt32(row.Cells["CategoryId"].Value);

            SetNumericValue(
                nudPrice,
                Convert.ToDecimal(row.Cells["Price"].Value));

            SetNumericValue(
                nudCost,
                Convert.ToDecimal(row.Cells["Cost"].Value));

            SetNumericValue(
                nudStock,
                Convert.ToDecimal(row.Cells["StockQty"].Value));

            nudStock.Enabled = false;

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void SetNumericValue(
            NumericUpDown control,
            decimal value)
        {
            if (value < control.Minimum)
                value = control.Minimum;

            if (value > control.Maximum)
                value = control.Maximum;

            control.Value = value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUserId <= 0)
                {
                    MessageBox.Show(
                        "Please open Product Management from a logged-in session.",
                        "Authentication Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                productService.AddProduct(
                    txtSKU.Text,
                    txtProductName.Text,
                    GetSelectedCategoryId(),
                    nudPrice.Value,
                    nudCost.Value,
                    Convert.ToInt32(nudStock.Value),
                    currentUserId);

                MessageBox.Show(
                    "Product added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedProductId <= 0)
                {
                    MessageBox.Show(
                        "Please select a product to update.");
                    return;
                }

                productService.UpdateProduct(
                    selectedProductId,
                    txtSKU.Text,
                    txtProductName.Text,
                    GetSelectedCategoryId(),
                    nudPrice.Value,
                    nudCost.Value);

                MessageBox.Show(
                    "Product updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedProductId <= 0)
                {
                    MessageBox.Show(
                        "Please select a product to delete.");
                    return;
                }

                var result = MessageBox.Show(
                    "Are you sure you want to delete this product?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                productService.DeleteProduct(selectedProductId);

                MessageBox.Show(
                    "Product deleted successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void dgvProducts_SelectionChanged(
            object sender,
            EventArgs e)
        {
            SelectProduct();
        }

        private void dgvProducts_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectProduct();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Product Management Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void lblProductsTitle_Click(
            object sender, EventArgs e)
        { }

        private void lblProductListTitle_Click(
            object sender, EventArgs e)
        { }

        private void pnlProductList_Paint(
            object sender, PaintEventArgs e)
        { }

        private void lblProductDetailsTitle_Click(
            object sender, EventArgs e)
        { }

        private void pnlProductDetails_Paint(
            object sender, PaintEventArgs e)
        { }

        private void lblSKU_Click(
            object sender, EventArgs e)
        { }

        private void txtSKU_TextChanged(
            object sender, EventArgs e)
        { }

        private void lblProductName_Click(
            object sender, EventArgs e)
        { }

        private void txtProductName_TextChanged(
            object sender, EventArgs e)
        { }

        private void lblCategory_Click(
            object sender, EventArgs e)
        { }

        private void cboCategory_SelectedIndexChanged(
            object sender, EventArgs e)
        { }

        private void lblPrice_Click(
            object sender, EventArgs e)
        { }

        private void nudPrice_ValueChanged(
            object sender, EventArgs e)
        { }

        private void lblStock_Click(
            object sender, EventArgs e)
        { }

        private void nudStock_ValueChanged(
            object sender, EventArgs e)
        { }

        private void lblSearch_Click(
            object sender, EventArgs e)
        { }

        private void pnlSearch_Paint(
            object sender, PaintEventArgs e)
        { }

        private void nudCost_ValueChanged(
            object sender, EventArgs e)
        { }

        private void lblCost_Click(
            object sender, EventArgs e)
        { }
    }
}
