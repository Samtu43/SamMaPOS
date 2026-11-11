
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EnterprisePOS.Data;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmPOS : Form
    {
        private readonly POSService posService;
        private readonly int cashierUserId;
        private readonly List<CartRow> cart;
        private bool isLoading;

        private class CartRow
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal LineTotal
            {
                get { return Quantity * UnitPrice; }
            }
        }

        public FrmPOS() : this(0)
        {
        }

        public FrmPOS(int userId)
        {
            InitializeComponent();

            cashierUserId = userId;
            posService = new POSService();
            cart = new List<CartRow>();

            Load += FrmPOS_Load;
            dgvProducts.SelectionChanged +=
                dgvProducts_SelectionChanged;
        }

        private void FrmPOS_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;

                ConfigureGrids();

                nudQuantity.Minimum = 1;
                nudQuantity.Maximum = 100000;
                nudQuantity.Value = 1;

                lblCashier.Text = "Cashier ID: " +
                    cashierUserId.ToString();

                txtPayment.Clear();

                isLoading = false;

                LoadProducts();
                RefreshCart();
            }
            catch (Exception ex)
            {
                isLoading = false;

                MessageBox.Show(
                    "Unable to initialize POS: " + ex.Message,
                    "POS Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrids()
        {
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.MultiSelect = false;
            dgvProducts.AutoGenerateColumns = true;

            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvCart.ReadOnly = true;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.MultiSelect = false;
            dgvCart.AutoGenerateColumns = true;

            dgvCart.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCart.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadProducts()
        {
            if (isLoading)
                return;

            try
            {
                isLoading = true;

                DataTable products =
                    posService.SearchProducts(txtSearch.Text);

                dgvProducts.DataSource = null;
                dgvProducts.DataSource = products;

                if (dgvProducts.Columns["ProductId"] != null)
                    dgvProducts.Columns["ProductId"].Visible = false;

                if (dgvProducts.Columns["Sku"] != null)
                    dgvProducts.Columns["Sku"].HeaderText = "SKU";

                if (dgvProducts.Columns["Barcode"] != null)
                    dgvProducts.Columns["Barcode"].HeaderText =
                        "Barcode";

                if (dgvProducts.Columns["Name"] != null)
                    dgvProducts.Columns["Name"].HeaderText =
                        "Product Name";

                if (dgvProducts.Columns["Price"] != null)
                {
                    dgvProducts.Columns["Price"].HeaderText =
                        "Price";

                    dgvProducts.Columns["Price"]
                        .DefaultCellStyle.Format = "N2";
                }

                if (dgvProducts.Columns["StockQty"] != null)
                    dgvProducts.Columns["StockQty"].HeaderText =
                        "Available Stock";

                dgvProducts.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load products: " + ex.Message,
                    "POS Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void RefreshCart()
        {
            var items = cart.Select(item => new
            {
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice,
                item.LineTotal
            }).ToList();

            dgvCart.DataSource = null;
            dgvCart.DataSource = items;

            if (dgvCart.Columns["ProductId"] != null)
                dgvCart.Columns["ProductId"].Visible = false;

            if (dgvCart.Columns["ProductName"] != null)
                dgvCart.Columns["ProductName"].HeaderText =
                    "Product";

            if (dgvCart.Columns["Quantity"] != null)
                dgvCart.Columns["Quantity"].HeaderText =
                    "Quantity";

            if (dgvCart.Columns["UnitPrice"] != null)
            {
                dgvCart.Columns["UnitPrice"].HeaderText =
                    "Unit Price";

                dgvCart.Columns["UnitPrice"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvCart.Columns["LineTotal"] != null)
            {
                dgvCart.Columns["LineTotal"].HeaderText =
                    "Line Total";

                dgvCart.Columns["LineTotal"]
                    .DefaultCellStyle.Format = "N2";
            }

            dgvCart.ClearSelection();

            UpdateTotals();
        }

        private decimal GetCartTotal()
        {
            return Math.Round(
                cart.Sum(item => item.LineTotal), 2);
        }

        private void UpdateTotals()
        {
            decimal total = GetCartTotal();

            lblTotal.Text = "Total: " +
                total.ToString("N2");

            decimal payment;

            if (decimal.TryParse(txtPayment.Text, out payment))
            {
                decimal change = payment - total;

                lblChange.Text = "Change: " +
                    Math.Max(change, 0).ToString("N2");
            }
            else
            {
                lblChange.Text = "Change: 0.00";
            }
        }

        private void AddSelectedProduct()
        {
            try
            {
                if (dgvProducts.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Please select a product.",
                        "POS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataGridViewRow row =
                    dgvProducts.SelectedRows[0];

                int productId =
                    Convert.ToInt32(
                        row.Cells["ProductId"].Value);

                string productName =
                    Convert.ToString(
                        row.Cells["Name"].Value);

                int quantity =
                    Convert.ToInt32(nudQuantity.Value);

                DataRow product =
                    posService.GetProductById(productId);

                if (product == null ||
                    !Convert.ToBoolean(product["IsActive"]))
                {
                    throw new InvalidOperationException(
                        "This product is no longer available.");
                }

                int availableStock =
                    posService.GetAvailableStock(productId);

                CartRow existingItem = cart
                    .FirstOrDefault(
                        item => item.ProductId == productId);

                int currentQuantity = existingItem == null
                    ? 0
                    : existingItem.Quantity;

                if (currentQuantity + quantity > availableStock)
                {
                    throw new InvalidOperationException(
                        "Insufficient stock for " + productName +
                        ". Available: " + availableStock);
                }

                decimal price =
                    Convert.ToDecimal(product["Price"]);

                if (existingItem == null)
                {
                    cart.Add(new CartRow
                    {
                        ProductId = productId,
                        ProductName = productName,
                        Quantity = quantity,
                        UnitPrice = price
                    });
                }
                else
                {
                    existingItem.Quantity += quantity;
                    existingItem.UnitPrice = price;
                }

                RefreshCart();

                nudQuantity.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to add product: " + ex.Message,
                    "POS Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RemoveSelectedItem()
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select an item from the cart.",
                    "POS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int productId = Convert.ToInt32(
                dgvCart.SelectedRows[0]
                    .Cells["ProductId"].Value);

            CartRow item = cart.FirstOrDefault(
                c => c.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
                RefreshCart();
            }
        }

        private void CompleteSale()
        {
            try
            {
                if (cashierUserId <= 0)
                {
                    throw new InvalidOperationException(
                        "No valid cashier account was provided.");
                }

                if (cart.Count == 0)
                {
                    MessageBox.Show(
                        "The shopping cart is empty.",
                        "POS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                decimal payment;

                if (!decimal.TryParse(
                    txtPayment.Text, out payment))
                {
                    throw new ArgumentException(
                        "Please enter a valid payment amount.");
                }

                decimal total = GetCartTotal();

                if (payment < total)
                {
                    throw new InvalidOperationException(
                        "Insufficient payment. Total: " +
                        total.ToString("N2"));
                }

                DialogResult result = MessageBox.Show(
                    "Complete this sale?\n\n" +
                    "Total: " + total.ToString("N2") + "\n" +
                    "Payment: " + payment.ToString("N2") + "\n" +
                    "Change: " +
                    (payment - total).ToString("N2"),
                    "Confirm Sale",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                List<POSCartItem> items = cart
                    .Select(item => new POSCartItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    })
                    .ToList();

                btnCompleteSale.Enabled = false;

                try
                {
                    posService.Checkout(
                        cashierUserId,
                        null,
                        "Cash",
                        payment,
                        0m,
                        items);
                }
                finally
                {
                    btnCompleteSale.Enabled = true;
                }

                MessageBox.Show(
                    "Sale completed successfully!\n\n" +
                    "Total: " + total.ToString("N2") + "\n" +
                    "Payment: " + payment.ToString("N2") + "\n" +
                    "Change: " +
                    (payment - total).ToString("N2"),
                    "Sale Completed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                cart.Clear();
                txtPayment.Clear();
                nudQuantity.Value = 1;

                RefreshCart();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to complete sale: " + ex.Message,
                    "POS Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvProducts_SelectionChanged(
            object sender, EventArgs e)
        {
            if (isLoading ||
                dgvProducts.SelectedRows.Count == 0)
                return;

            DataGridViewRow row =
                dgvProducts.SelectedRows[0];

            if (row.Cells["StockQty"].Value == null)
                return;

            int stock = Convert.ToInt32(
                row.Cells["StockQty"].Value);

            if (stock > 0)
            {
                nudQuantity.Maximum = stock;
                nudQuantity.Value = 1;
            }
        }

        private void txtSearch_TextChanged(
            object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnAddToCart_Click(
            object sender, EventArgs e)
        {
            AddSelectedProduct();
        }

        private void btnRemoveItem_Click(
            object sender, EventArgs e)
        {
            RemoveSelectedItem();
        }

        private void txtPayment_TextChanged(
            object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void btnCompleteSale_Click(
            object sender, EventArgs e)
        {
            CompleteSale();
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
        }

        private void dgvCart_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblCartTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblCashier_Click(object sender, EventArgs e)
        {
        }

        private void lblQuantity_Click(object sender, EventArgs e)
        {
        }

        private void nudQuantity_ValueChanged(
            object sender, EventArgs e)
        {
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
        }

        private void pnlCart_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblPayment_Click(object sender, EventArgs e)
        {
        }

        private void lblChange_Click(object sender, EventArgs e)
        {
        }

        private void lblPOSTitle_Click(object sender, EventArgs e)
        {
        }

        private void dgvProducts_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
