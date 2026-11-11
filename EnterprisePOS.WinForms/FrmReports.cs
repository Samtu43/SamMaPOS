
using System;
using System.Data;
using System.Windows.Forms;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmReports : Form
    {
        private readonly ReportService reportService;
        private bool isLoading;

        public FrmReports()
        {
            InitializeComponent();

            reportService = new ReportService();

            Load += FrmReports_Load;
        }

        private void FrmReports_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;

                ConfigureGrid();

                dtpFromDate.Format = DateTimePickerFormat.Short;
                dtpToDate.Format = DateTimePickerFormat.Short;

                dtpFromDate.Value = DateTime.Today.AddMonths(-1);
                dtpToDate.Value = DateTime.Today;

                ClearReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to initialize reports: " + ex.Message,
                    "Reports Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ConfigureGrid()
        {
            dgvSalesReport.ReadOnly = true;
            dgvSalesReport.AllowUserToAddRows = false;
            dgvSalesReport.AllowUserToDeleteRows = false;
            dgvSalesReport.MultiSelect = false;
            dgvSalesReport.AutoGenerateColumns = true;

            dgvSalesReport.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvSalesReport.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void GenerateReport()
        {
            if (isLoading)
                return;

            try
            {
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;

                if (fromDate > toDate)
                {
                    MessageBox.Show(
                        "From Date cannot be later than To Date.",
                        "Invalid Date Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                isLoading = true;

                DataTable sales =
                    reportService.GetDailySales(fromDate, toDate);

                dgvSalesReport.DataSource = null;
                dgvSalesReport.DataSource = sales;

                ConfigureReportColumns();

                dgvSalesReport.ClearSelection();

                UpdateSummary(sales);
            }
            catch (Exception ex)
            {
                ClearReport();

                MessageBox.Show(
                    "Unable to generate sales report: " + ex.Message,
                    "Reports Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private string FindColumn(
            DataTable table,
            params string[] names)
        {
            foreach (string name in names)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (string.Equals(
                        column.ColumnName,
                        name,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return column.ColumnName;
                    }
                }
            }

            return null;
        }

        private void UpdateSummary(DataTable sales)
        {
            string ordersColumn = FindColumn(
                sales,
                "TotalOrders",
                "OrderCount",
                "OrdersCount",
                "NumberOfOrders");

            string salesColumn = FindColumn(
                sales,
                "TotalSales",
                "SalesTotal",
                "Revenue",
                "TotalRevenue");

            if (string.IsNullOrEmpty(ordersColumn) ||
                string.IsNullOrEmpty(salesColumn))
            {
                throw new InvalidOperationException(
                    "The daily sales report must contain an order count " +
                    "column and a total sales column.");
            }

            int totalOrders = 0;
            decimal totalSales = 0;

            foreach (DataRow row in sales.Rows)
            {
                if (row[ordersColumn] != DBNull.Value)
                {
                    totalOrders +=
                        Convert.ToInt32(row[ordersColumn]);
                }

                if (row[salesColumn] != DBNull.Value)
                {
                    totalSales +=
                        Convert.ToDecimal(row[salesColumn]);
                }
            }

            decimal averageSale = totalOrders > 0
                ? totalSales / totalOrders
                : 0;

            lblTotalOrdersValue.Text =
                totalOrders.ToString("N0");

            lblTotalSalesValue.Text =
                totalSales.ToString("N2");

            lblAverageSaleValue.Text =
                averageSale.ToString("N2");
        }

        private void ConfigureReportColumns()
        {
            foreach (DataGridViewColumn column
                in dgvSalesReport.Columns)
            {
                if (column.ValueType == typeof(decimal) ||
                    column.ValueType == typeof(double) ||
                    column.ValueType == typeof(float))
                {
                    column.DefaultCellStyle.Format = "N2";
                }

                if (column.ValueType == typeof(DateTime))
                {
                    column.DefaultCellStyle.Format = "yyyy-MM-dd";
                }
            }
        }

        private void ClearReport()
        {
            dgvSalesReport.DataSource = null;

            lblTotalOrdersValue.Text = "0";
            lblTotalSalesValue.Text = "0.00";
            lblAverageSaleValue.Text = "0.00";
        }

        private void btnGenerate_Click(
            object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void btnClear_Click(
            object sender, EventArgs e)
        {
            isLoading = true;

            try
            {
                dtpFromDate.Value = DateTime.Today.AddMonths(-1);
                dtpToDate.Value = DateTime.Today;

                ClearReport();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void dtpFromDate_ValueChanged(
            object sender, EventArgs e)
        {
        }

        private void dtpToDate_ValueChanged(
            object sender, EventArgs e)
        {
        }

        private void pnlSalesReport_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblFromDate_Click(
            object sender, EventArgs e)
        {
        }

        private void pnlFilters_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblToDate_Click(
            object sender, EventArgs e)
        {
        }

        private void pnlTotalOrders_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void pnlTotalSales_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblTotalOrdersTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void lblTotalOrdersValue_Click(
            object sender, EventArgs e)
        {
        }

        private void lblTotalSalesTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void lblTotalSalesValue_Click(
            object sender, EventArgs e)
        {
        }

        private void lblAverageSaleTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void lblAverageSaleValue_Click(
            object sender, EventArgs e)
        {
        }

        private void pnlAverageSale_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void dgvSalesReport_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblSalesReportTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void lblReportsTitle_Click(
            object sender, EventArgs e)
        {
        }
    }
}
