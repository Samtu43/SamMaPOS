
using System;
using System.Data;
using System.Windows.Forms;
using EnterprisePOS.Data;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmDashboard : Form
    {
        private readonly LoginUser currentUser;

        private readonly DashboardService dashboardService =
            new DashboardService();

        public FrmDashboard(LoginUser user)
        {
            InitializeComponent();

            if (user == null)
                throw new ArgumentNullException("user");

            currentUser = user;
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            lblCurrentUser.Text =
                currentUser.FullName + " (" + currentUser.RoleName + ")";

            lblPageTitle.Text = "Dashboard";

            ApplyRolePermissions();
            LoadDashboard();
        }

        private void ApplyRolePermissions()
        {
            string role = currentUser.RoleName;

            btnDashboard.Enabled =
                dashboardService.CanAccess(role, "Dashboard");

            btnPOS.Enabled =
                dashboardService.CanAccess(role, "POS");

            btnProducts.Enabled =
                dashboardService.CanAccess(role, "Products");

            btnInventory.Enabled =
                dashboardService.CanAccess(role, "Inventory");

            btnCategories.Enabled =
                dashboardService.CanAccess(role, "Categories");

            btnCustomers.Enabled =
                dashboardService.CanAccess(role, "Customers");

            btnOrders.Enabled =
                dashboardService.CanAccess(role, "Orders");

            btnReports.Enabled =
                dashboardService.CanAccess(role, "Reports");

            btnUsers.Enabled =
                dashboardService.CanAccess(role, "Users");
        }

        private void LoadDashboard()
        {
            try
            {
                DataTable statistics =
                    dashboardService.GetStatistics();

                if (statistics.Rows.Count > 0)
                {
                    DataRow row = statistics.Rows[0];

                    decimal sales =
                        Convert.ToDecimal(row["TodaySales"]);

                    int orders =
                        Convert.ToInt32(row["TodayOrders"]);

                    int products =
                        Convert.ToInt32(row["ActiveProducts"]);

                    int lowStock =
                        Convert.ToInt32(row["LowStockCount"]);

                    lblTotalSalesValue.Text = sales.ToString("C2");
                    lblTotalOrdersValue.Text = orders.ToString();
                    lblTotalProductsValue.Text = products.ToString();
                    lblLowStockValue.Text = lowStock.ToString();
                }

                dgvRecentOrders.DataSource =
                    dashboardService.GetRecentOrders();

                dgvLowStock.DataSource =
                    dashboardService.GetLowStock();

                ConfigureGrid(dgvRecentOrders);
                ConfigureGrid(dgvLowStock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load dashboard: " + ex.Message,
                    "Dashboard Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid(DataGridView grid)
        {
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void OpenModule(Form form, string module)
        {
            if (!dashboardService.CanAccess(
                currentUser.RoleName, module))
            {
                MessageBox.Show(
                    "You do not have permission to access this module.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                form.Dispose();
                return;
            }

            using (form)
            {
                form.ShowDialog(this);
            }

            LoadDashboard();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            lblPageTitle.Text = "Dashboard";
            LoadDashboard();
        }


        private void btnPOS_Click(object sender, EventArgs e)
        {
            OpenModule(
                new FrmPOS(currentUser.UserId),
                "POS");
        }


        private void btnProducts_Click(object sender, EventArgs e)
        {
            OpenModule(
                new FrmProducts(currentUser.UserId),
                "Products");
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            OpenModule(new FrmInventory(currentUser.UserId), "Inventory");
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            OpenModule(new FrmCategories(), "Categories");
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            OpenModule(new FrmCustomers(), "Customers");
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OpenModule(new FrmOrders(), "Orders");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenModule(new FrmReports(), "Reports");
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            OpenModule(new FrmUsers(), "Users");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Close();
        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblBrand_Click(object sender, EventArgs e)
        {
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblPageTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblCurrentUser_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void lblTotalOrdersValue_Click(object sender, EventArgs e)
        {
        }

        private void lblTotalOrdersTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblTotalSalesValue_Click(object sender, EventArgs e)
        {
        }

        private void pnlTotalOrders_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlTotalProducts_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblTotalProductsValue_Click(object sender, EventArgs e)
        {
        }

        private void lblTotalProductsTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblLowStockValue_Click(object sender, EventArgs e)
        {
        }

        private void lblLowStockTitle_Click(object sender, EventArgs e)
        {
        }

        private void pnlLowStock_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dgvLowStock_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvRecentOrders_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblLowStockAlertsTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void lblRecentOrdersTitle_Click(
            object sender, EventArgs e)
        {
        }
    }
}
