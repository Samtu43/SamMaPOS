
using System;
using System.Linq;
using System.Windows.Forms;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmCustomers : Form
    {
        private readonly CustomerService customerService;
        private int selectedCustomerId;
        private bool isLoading;

        public FrmCustomers()
        {
            InitializeComponent();

            customerService = new CustomerService();

            Load += FrmCustomers_Load;

            dgvCustomers.SelectionChanged +=
                dgvCustomers_SelectionChanged;
        }

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureGrid();
                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load customers: " + ex.Message,
                    "Customer Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.MultiSelect = false;

            dgvCustomers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCustomers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvCustomers.AutoGenerateColumns = true;
        }

        private void LoadCustomers()
        {
            BindCustomers(false);
        }

        private void BindCustomers(bool search)
        {
            isLoading = true;

            try
            {
                var customers = search
                    ? customerService.SearchCustomers(txtSearch.Text)
                    : customerService.GetAllCustomers();

                var customerList = customers
                    .Select(c => new
                    {
                        c.CustomerId,
                        c.FullName,
                        c.Phone,
                        c.Email,
                        c.Address,
                        c.CreatedAt
                    })
                    .ToList();

                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = customerList;

                if (dgvCustomers.Columns["CustomerId"] != null)
                    dgvCustomers.Columns["CustomerId"].Visible = false;

                if (dgvCustomers.Columns["FullName"] != null)
                    dgvCustomers.Columns["FullName"].HeaderText =
                        "Customer Name";

                if (dgvCustomers.Columns["Phone"] != null)
                    dgvCustomers.Columns["Phone"].HeaderText =
                        "Phone";

                if (dgvCustomers.Columns["Email"] != null)
                    dgvCustomers.Columns["Email"].HeaderText =
                        "Email";

                if (dgvCustomers.Columns["Address"] != null)
                    dgvCustomers.Columns["Address"].HeaderText =
                        "Address";

                if (dgvCustomers.Columns["CreatedAt"] != null)
                {
                    dgvCustomers.Columns["CreatedAt"].HeaderText =
                        "Date Registered";

                    dgvCustomers.Columns["CreatedAt"]
                        .DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                }

                dgvCustomers.ClearSelection();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ClearFields()
        {
            selectedCustomerId = 0;

            txtCustomerName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            dgvCustomers.ClearSelection();

            txtCustomerName.Focus();
        }

        private void SelectCustomer()
        {
            if (isLoading || dgvCustomers.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = dgvCustomers.SelectedRows[0];

            if (row.Cells["CustomerId"].Value == null)
                return;

            selectedCustomerId =
                Convert.ToInt32(row.Cells["CustomerId"].Value);

            txtCustomerName.Text =
                Convert.ToString(row.Cells["FullName"].Value);

            txtPhone.Text =
                Convert.ToString(row.Cells["Phone"].Value);

            txtEmail.Text =
                Convert.ToString(row.Cells["Email"].Value);

            txtAddress.Text =
                Convert.ToString(row.Cells["Address"].Value);

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void RefreshCustomers()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                LoadCustomers();
            else
                BindCustomers(true);

            ClearFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                customerService.AddCustomer(
                    txtCustomerName.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtAddress.Text);

                MessageBox.Show(
                    "Customer added successfully.",
                    "Customer Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to add customer: " + ex.Message,
                    "Customer Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCustomerId <= 0)
                {
                    MessageBox.Show(
                        "Please select a customer to update.",
                        "Customer Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                customerService.UpdateCustomer(
                    selectedCustomerId,
                    txtCustomerName.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtAddress.Text);

                MessageBox.Show(
                    "Customer updated successfully.",
                    "Customer Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to update customer: " + ex.Message,
                    "Customer Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCustomerId <= 0)
                {
                    MessageBox.Show(
                        "Please select a customer to delete.",
                        "Customer Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this customer?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                customerService.DeleteCustomer(selectedCustomerId);

                MessageBox.Show(
                    "Customer deleted successfully.",
                    "Customer Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to delete customer: " + ex.Message,
                    "Customer Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchCustomers();
        }

        private void SearchCustomers()
        {
            try
            {
                BindCustomers(true);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to search customers: " + ex.Message,
                    "Search Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvCustomers_SelectionChanged(
            object sender, EventArgs e)
        {
            SelectCustomer();
        }

        private void dgvCustomers_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectCustomer();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
        }

        private void pnlSearch_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlCustomerList_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblCustomerListTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblCustomerDetailsTitle_Click(object sender, EventArgs e)
        {
        }

        private void pnlCustomerDetails_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblCustomerName_Click(object sender, EventArgs e)
        {
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblPhone_Click(object sender, EventArgs e)
        {
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblAddress_Click(object sender, EventArgs e)
        {
        }

        private void lblCustomersTitle_Click(object sender, EventArgs e)
        {
        }
    }
}
