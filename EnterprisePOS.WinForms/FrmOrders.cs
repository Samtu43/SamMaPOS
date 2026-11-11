
using System;
using System.Data;
using System.Windows.Forms;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmOrders : Form
    {
        private readonly OrderService orderService;
        private int selectedOrderId;
        private bool isLoading;

        public FrmOrders()
        {
            InitializeComponent();

            orderService = new OrderService();

            Load += FrmOrders_Load;

            dgvOrders.SelectionChanged +=
                dgvOrders_SelectionChanged;
        }

        private void FrmOrders_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;

                ConfigureGrids();

                dtpFromDate.Format = DateTimePickerFormat.Short;
                dtpToDate.Format = DateTimePickerFormat.Short;

                dtpFromDate.Value = DateTime.Today.AddMonths(-1);
                dtpToDate.Value = DateTime.Today;

                selectedOrderId = 0;

                lblSelectedOrder.Text = "No order selected";
                lblOrderTotal.Text = "Total: 0.00";

                isLoading = false;

                LoadOrders();
            }
            catch (Exception ex)
            {
                isLoading = false;

                MessageBox.Show(
                    "Unable to load orders: " + ex.Message,
                    "Orders Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrids()
        {
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.MultiSelect = false;
            dgvOrders.AutoGenerateColumns = true;

            dgvOrders.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvOrders.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvOrderDetails.ReadOnly = true;
            dgvOrderDetails.AllowUserToAddRows = false;
            dgvOrderDetails.AllowUserToDeleteRows = false;
            dgvOrderDetails.MultiSelect = false;
            dgvOrderDetails.AutoGenerateColumns = true;

            dgvOrderDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvOrderDetails.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadOrders()
        {
            if (isLoading)
                return;

            try
            {
                if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
                {
                    ClearSelectedOrder();

                    dgvOrders.DataSource = null;

                    MessageBox.Show(
                        "From Date cannot be later than To Date.",
                        "Invalid Date Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                isLoading = true;

                DataTable orders = orderService.GetOrders(
                    txtSearch.Text,
                    dtpFromDate.Value.Date,
                    dtpToDate.Value.Date);

                dgvOrders.DataSource = null;
                dgvOrders.DataSource = orders;

                ConfigureOrdersColumns();

                dgvOrders.ClearSelection();

                ClearSelectedOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to retrieve orders: " + ex.Message,
                    "Orders Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ConfigureOrdersColumns()
        {
            if (dgvOrders.Columns["OrderId"] != null)
                dgvOrders.Columns["OrderId"].Visible = false;

            if (dgvOrders.Columns["OrderNumber"] != null)
                dgvOrders.Columns["OrderNumber"].HeaderText =
                    "Order Number";

            if (dgvOrders.Columns["OrderDate"] != null)
            {
                dgvOrders.Columns["OrderDate"].HeaderText =
                    "Order Date";

                dgvOrders.Columns["OrderDate"]
                    .DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }

            if (dgvOrders.Columns["CustomerName"] != null)
                dgvOrders.Columns["CustomerName"].HeaderText =
                    "Customer";

            if (dgvOrders.Columns["CashierName"] != null)
                dgvOrders.Columns["CashierName"].HeaderText =
                    "Cashier";

            if (dgvOrders.Columns["Subtotal"] != null)
            {
                dgvOrders.Columns["Subtotal"].HeaderText =
                    "Subtotal";

                dgvOrders.Columns["Subtotal"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvOrders.Columns["TaxAmount"] != null)
            {
                dgvOrders.Columns["TaxAmount"].HeaderText =
                    "Tax";

                dgvOrders.Columns["TaxAmount"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvOrders.Columns["DiscountAmount"] != null)
            {
                dgvOrders.Columns["DiscountAmount"].HeaderText =
                    "Discount";

                dgvOrders.Columns["DiscountAmount"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvOrders.Columns["Total"] != null)
            {
                dgvOrders.Columns["Total"].HeaderText =
                    "Total";

                dgvOrders.Columns["Total"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvOrders.Columns["PaymentMethod"] != null)
                dgvOrders.Columns["PaymentMethod"].HeaderText =
                    "Payment Method";

            if (dgvOrders.Columns["AmountPaid"] != null)
            {
                dgvOrders.Columns["AmountPaid"].HeaderText =
                    "Amount Paid";

                dgvOrders.Columns["AmountPaid"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvOrders.Columns["ChangeDue"] != null)
            {
                dgvOrders.Columns["ChangeDue"].HeaderText =
                    "Change";

                dgvOrders.Columns["ChangeDue"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvOrders.Columns["Status"] != null)
                dgvOrders.Columns["Status"].HeaderText =
                    "Status";
        }

        private void LoadOrderDetails(int orderId)
        {
            try
            {
                DataTable details =
                    orderService.GetOrderDetails(orderId);

                dgvOrderDetails.DataSource = null;
                dgvOrderDetails.DataSource = details;

                ConfigureOrderDetailsColumns();

                dgvOrderDetails.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load order details: " + ex.Message,
                    "Order Details Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureOrderDetailsColumns()
        {
            if (dgvOrderDetails.Columns["OrderDetailId"] != null)
                dgvOrderDetails.Columns["OrderDetailId"].Visible = false;

            if (dgvOrderDetails.Columns["ProductId"] != null)
                dgvOrderDetails.Columns["ProductId"].Visible = false;

            if (dgvOrderDetails.Columns["Sku"] != null)
                dgvOrderDetails.Columns["Sku"].HeaderText = "SKU";

            if (dgvOrderDetails.Columns["ProductName"] != null)
                dgvOrderDetails.Columns["ProductName"].HeaderText =
                    "Product Name";

            if (dgvOrderDetails.Columns["Quantity"] != null)
                dgvOrderDetails.Columns["Quantity"].HeaderText =
                    "Quantity";

            if (dgvOrderDetails.Columns["UnitPrice"] != null)
            {
                dgvOrderDetails.Columns["UnitPrice"].HeaderText =
                    "Unit Price";

                dgvOrderDetails.Columns["UnitPrice"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (dgvOrderDetails.Columns["LineTotal"] != null)
            {
                dgvOrderDetails.Columns["LineTotal"].HeaderText =
                    "Line Total";

                dgvOrderDetails.Columns["LineTotal"]
                    .DefaultCellStyle.Format = "N2";
            }
        }

        private void SelectOrder()
        {
            if (isLoading || dgvOrders.SelectedRows.Count == 0)
                return;

            try
            {
                DataGridViewRow row = dgvOrders.SelectedRows[0];

                if (row.Cells["OrderId"].Value == null)
                    return;

                selectedOrderId =
                    Convert.ToInt32(row.Cells["OrderId"].Value);

                string orderNumber =
                    Convert.ToString(row.Cells["OrderNumber"].Value);

                decimal total =
                    Convert.ToDecimal(row.Cells["Total"].Value);

                lblSelectedOrder.Text =
                    "Selected Order: " + orderNumber;

                lblOrderTotal.Text =
                    "Total: " + total.ToString("N2");

                LoadOrderDetails(selectedOrderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to select order: " + ex.Message,
                    "Order Selection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearSelectedOrder()
        {
            selectedOrderId = 0;

            lblSelectedOrder.Text = "No order selected";
            lblOrderTotal.Text = "Total: 0.00";

            dgvOrderDetails.DataSource = null;
        }

        private void dgvOrders_SelectionChanged(
            object sender, EventArgs e)
        {
            SelectOrder();
        }

        private void dgvOrders_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectOrder();
        }

        private void txtSearch_TextChanged(
            object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void dtpFromDate_ValueChanged(
            object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void dtpToDate_ValueChanged(
            object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void lblOrdersTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void lblSelectedOrder_Click(
            object sender, EventArgs e)
        {
        }

        private void lblOrderDetailsTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void dgvOrderDetails_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void pnlOrderDetails_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblOrdersListTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void pnlOrdersList_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblSearch_Click(
            object sender, EventArgs e)
        {
        }

        private void lblFromDate_Click(
            object sender, EventArgs e)
        {
        }

        private void lblToDate_Click(
            object sender, EventArgs e)
        {
        }

        private void pnlFilters_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblOrderTotal_Click(
            object sender, EventArgs e)
        {
        }
    }
}
