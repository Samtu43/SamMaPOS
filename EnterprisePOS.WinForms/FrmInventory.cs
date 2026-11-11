
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EnterprisePOS.Data;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmInventory : Form
    {
        private readonly InventoryService inventoryService;
        private readonly int currentUserId;
        private bool isLoading;

        public FrmInventory() : this(0)
        {
        }

        public FrmInventory(int userId)
        {
            InitializeComponent();

            inventoryService = new InventoryService();
            currentUserId = userId;

            Load += FrmInventory_Load;
            dgvInventory.SelectionChanged += dgvInventory_SelectionChanged;
            btnSearch.Click += btnSearch_Click;
            txtSearch.KeyDown += txtSearch_KeyDown;
        }

        private void FrmInventory_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;

                ConfigureGrids();

                cboAdjustmentType.Items.Clear();
                cboAdjustmentType.Items.Add("Stock In");
                cboAdjustmentType.Items.Add("Stock Out");
                cboAdjustmentType.DropDownStyle = ComboBoxStyle.DropDownList;
                cboAdjustmentType.SelectedIndex = 0;

                cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;

                nudQuantity.Minimum = 1;
                nudQuantity.Maximum = 1000000;
                nudQuantity.Value = 1;

                LoadProducts();
                LoadInventoryHistory();

                isLoading = false;
            }
            catch (Exception ex)
            {
                isLoading = false;

                MessageBox.Show(
                    "Unable to load inventory: " + ex.Message,
                    "Inventory Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrids()
        {
            dgvInventory.ReadOnly = true;
            dgvInventory.AllowUserToAddRows = false;
            dgvInventory.AllowUserToDeleteRows = false;
            dgvInventory.MultiSelect = false;
            dgvInventory.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventory.AutoGenerateColumns = true;

            dgvInventoryHistory.ReadOnly = true;
            dgvInventoryHistory.AllowUserToAddRows = false;
            dgvInventoryHistory.AllowUserToDeleteRows = false;
            dgvInventoryHistory.MultiSelect = false;
            dgvInventoryHistory.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvInventoryHistory.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventoryHistory.AutoGenerateColumns = true;
        }

        private void LoadProducts()
        {
            var products = inventoryService.GetAllProducts();

            BindProducts(products);
        }

        private void BindProducts(System.Collections.Generic.List<Product> products)
        {
            isLoading = true;

            var inventoryList = products
                .Select(p => new
                {
                    p.ProductId,
                    p.Sku,
                    p.Name,
                    p.StockQty,
                    p.ReorderLevel,
                    p.Price
                })
                .ToList();

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = inventoryList;

            if (dgvInventory.Columns["ProductId"] != null)
                dgvInventory.Columns["ProductId"].Visible = false;

            if (dgvInventory.Columns["Sku"] != null)
                dgvInventory.Columns["Sku"].HeaderText = "SKU";

            if (dgvInventory.Columns["Name"] != null)
                dgvInventory.Columns["Name"].HeaderText = "Product Name";

            if (dgvInventory.Columns["StockQty"] != null)
                dgvInventory.Columns["StockQty"].HeaderText = "Current Stock";

            if (dgvInventory.Columns["ReorderLevel"] != null)
                dgvInventory.Columns["ReorderLevel"].HeaderText = "Reorder Level";

            if (dgvInventory.Columns["Price"] != null)
            {
                dgvInventory.Columns["Price"].HeaderText = "Price";
                dgvInventory.Columns["Price"].DefaultCellStyle.Format = "N2";
            }

            var productOptions = inventoryService.GetAllProducts()
                .Select(p => new
                {
                    p.ProductId,
                    DisplayName = p.Sku + " - " + p.Name
                })
                .ToList();

            cboProduct.DataSource = null;
            cboProduct.DisplayMember = "DisplayName";
            cboProduct.ValueMember = "ProductId";
            cboProduct.DataSource = productOptions;

            dgvInventory.ClearSelection();

            if (cboProduct.Items.Count > 0)
                cboProduct.SelectedIndex = 0;

            isLoading = false;
        }

        private void LoadInventoryHistory()
        {
            dgvInventoryHistory.DataSource =
                inventoryService.GetInventoryHistory();

            if (dgvInventoryHistory.Columns["LogId"] != null)
                dgvInventoryHistory.Columns["LogId"].Visible = false;

            if (dgvInventoryHistory.Columns["CreatedAt"] != null)
            {
                dgvInventoryHistory.Columns["CreatedAt"].HeaderText =
                    "Date and Time";

                dgvInventoryHistory.Columns["CreatedAt"]
                    .DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }

            if (dgvInventoryHistory.Columns["Sku"] != null)
                dgvInventoryHistory.Columns["Sku"].HeaderText = "SKU";

            if (dgvInventoryHistory.Columns["ProductName"] != null)
                dgvInventoryHistory.Columns["ProductName"].HeaderText =
                    "Product";

            if (dgvInventoryHistory.Columns["ChangeQty"] != null)
                dgvInventoryHistory.Columns["ChangeQty"].HeaderText =
                    "Quantity Change";

            if (dgvInventoryHistory.Columns["Reason"] != null)
                dgvInventoryHistory.Columns["Reason"].HeaderText =
                    "Reason";

            if (dgvInventoryHistory.Columns["AdjustedBy"] != null)
                dgvInventoryHistory.Columns["AdjustedBy"].HeaderText =
                    "Adjusted By";
        }

        private void SearchProducts()
        {
            try
            {
                var products =
                    inventoryService.SearchProducts(txtSearch.Text);

                BindProducts(products);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to search products: " + ex.Message,
                    "Search Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchProducts();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchProducts();
                e.SuppressKeyPress = true;
            }
        }

        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading || dgvInventory.SelectedRows.Count == 0)
                return;

            try
            {
                object value =
                    dgvInventory.SelectedRows[0].Cells["ProductId"].Value;

                if (value == null)
                    return;

                int productId = Convert.ToInt32(value);

                cboProduct.SelectedValue = productId;
            }
            catch
            {
            }
        }

        private void btnSaveAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUserId <= 0)
                {
                    MessageBox.Show(
                        "The logged-in user ID is missing.",
                        "Inventory Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (cboProduct.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Please select a product.",
                        "Inventory Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                int productId =
                    Convert.ToInt32(cboProduct.SelectedValue);

                string adjustmentType =
                    cboAdjustmentType.SelectedItem == null
                        ? ""
                        : cboAdjustmentType.SelectedItem.ToString();

                int quantity = Convert.ToInt32(nudQuantity.Value);
                string reason = txtReason.Text.Trim();

                Product product =
                    inventoryService.GetProductById(productId);

                if (product == null)
                {
                    MessageBox.Show(
                        "The selected product was not found.",
                        "Inventory Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string message =
                    "Product: " + product.Name +
                    "\nCurrent Stock: " + product.StockQty +
                    "\nAdjustment: " + adjustmentType +
                    "\nQuantity: " + quantity +
                    "\nReason: " + reason +
                    "\n\nDo you want to continue?";

                DialogResult confirmation = MessageBox.Show(
                    message,
                    "Confirm Stock Adjustment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation != DialogResult.Yes)
                    return;

                int newStock = inventoryService.AdjustStock(
                    productId,
                    adjustmentType,
                    quantity,
                    reason,
                    currentUserId);

                MessageBox.Show(
                    "Stock adjusted successfully." +
                    "\nNew Stock Quantity: " + newStock,
                    "Inventory Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtReason.Clear();
                nudQuantity.Value = 1;
                cboAdjustmentType.SelectedIndex = 0;

                LoadProducts();
                LoadInventoryHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to adjust stock: " + ex.Message,
                    "Inventory Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pnlInventoryList_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblHistoryTitle_Click(object sender, EventArgs e)
        {
        }

        private void txtReason_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblReason_Click(object sender, EventArgs e)
        {
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
        }

        private void lblQuantity_Click(object sender, EventArgs e)
        {
        }

        private void cboAdjustmentType_SelectedIndexChanged(
            object sender, EventArgs e)
        {
        }

        private void lblAdjustmentType_Click(object sender, EventArgs e)
        {
        }

        private void cboProduct_SelectedIndexChanged(
            object sender, EventArgs e)
        {
        }

        private void lblProduct_Click(object sender, EventArgs e)
        {
        }

        private void lblAdjustmentTitle_Click(object sender, EventArgs e)
        {
        }

        private void pnlStockAdjustment_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dgvInventory_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvInventoryHistory_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblInventoryListTitle_Click(object sender, EventArgs e)
        {
        }

        private void pnlInventoryHistory_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
