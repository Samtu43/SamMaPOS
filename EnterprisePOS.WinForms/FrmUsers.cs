
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EnterprisePOS.Data;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmUsers : Form
    {
        private readonly UserService userService;
        private int selectedUserId;
        private string selectedFullName;
        private bool isLoading;

        public FrmUsers()
        {
            InitializeComponent();

            userService = new UserService();

            Load += FrmUsers_Load;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
        }

        private void FrmUsers_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;

                ConfigureGrid();
                LoadRoles();

                txtPassword.UseSystemPasswordChar = true;

                isLoading = false;

                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                isLoading = false;

                MessageBox.Show(
                    "Unable to load users: " + ex.Message,
                    "Users Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.MultiSelect = false;
            dgvUsers.AutoGenerateColumns = true;

            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadRoles()
        {
            var roles = userService.GetAllRoles()
                .Select(r => new
                {
                    r.RoleId,
                    r.RoleName
                })
                .ToList();

            cboRole.DataSource = null;
            cboRole.DisplayMember = "RoleName";
            cboRole.ValueMember = "RoleId";
            cboRole.DataSource = roles;

            cboRole.SelectedIndex = -1;
        }

        private void LoadUsers()
        {
            if (isLoading)
                return;

            try
            {
                isLoading = true;

                var users = userService.SearchUsers(txtSearch.Text);

                var roles = userService.GetAllRoles()
                    .Select(r => new
                    {
                        r.RoleId,
                        r.RoleName
                    })
                    .ToList();

                var userList = users
                    .Select(u => new
                    {
                        u.UserId,
                        u.Username,
                        u.FullName,
                        u.RoleId,
                        RoleName = roles
                            .Where(r => r.RoleId == u.RoleId)
                            .Select(r => r.RoleName)
                            .FirstOrDefault(),
                        u.IsActive,
                        u.CreatedAt,
                        u.LastLoginAt
                    })
                    .ToList();

                dgvUsers.DataSource = null;
                dgvUsers.DataSource = userList;

                ConfigureUserColumns();

                dgvUsers.ClearSelection();
                ResetSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to retrieve users: " + ex.Message,
                    "Users Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ConfigureUserColumns()
        {
            if (dgvUsers.Columns["UserId"] != null)
                dgvUsers.Columns["UserId"].Visible = false;

            if (dgvUsers.Columns["RoleId"] != null)
                dgvUsers.Columns["RoleId"].Visible = false;

            if (dgvUsers.Columns["Username"] != null)
                dgvUsers.Columns["Username"].HeaderText =
                    "Username";

            if (dgvUsers.Columns["FullName"] != null)
                dgvUsers.Columns["FullName"].HeaderText =
                    "Full Name";

            if (dgvUsers.Columns["RoleName"] != null)
                dgvUsers.Columns["RoleName"].HeaderText =
                    "Role";

            if (dgvUsers.Columns["IsActive"] != null)
                dgvUsers.Columns["IsActive"].HeaderText =
                    "Active";

            if (dgvUsers.Columns["CreatedAt"] != null)
            {
                dgvUsers.Columns["CreatedAt"].HeaderText =
                    "Date Created";

                dgvUsers.Columns["CreatedAt"]
                    .DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }

            if (dgvUsers.Columns["LastLoginAt"] != null)
            {
                dgvUsers.Columns["LastLoginAt"].HeaderText =
                    "Last Login";

                dgvUsers.Columns["LastLoginAt"]
                    .DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }
        }

        private void ResetSelection()
        {
            selectedUserId = 0;
            selectedFullName = "";

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void ClearFields()
        {
            ResetSelection();

            txtUsername.Clear();
            txtPassword.Clear();

            cboRole.SelectedIndex = -1;
            chkActive.Checked = true;

            dgvUsers.ClearSelection();

            txtUsername.Focus();
        }

        private void SelectUser()
        {
            if (isLoading || dgvUsers.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = dgvUsers.SelectedRows[0];

            if (row.Cells["UserId"].Value == null)
                return;

            selectedUserId =
                Convert.ToInt32(row.Cells["UserId"].Value);

            selectedFullName =
                Convert.ToString(row.Cells["FullName"].Value);

            txtUsername.Text =
                Convert.ToString(row.Cells["Username"].Value);

            txtPassword.Clear();

            cboRole.SelectedValue =
                Convert.ToInt32(row.Cells["RoleId"].Value);

            chkActive.Checked =
                Convert.ToBoolean(row.Cells["IsActive"].Value);

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private int GetSelectedRoleId()
        {
            if (cboRole.SelectedIndex < 0 ||
                cboRole.SelectedValue == null)
            {
                throw new ArgumentException(
                    "Please select a role.");
            }

            return Convert.ToInt32(cboRole.SelectedValue);
        }

        private string PromptFullName(string initialValue)
        {
            using (Form dialog = new Form())
            {
                dialog.Text = "User Full Name";
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.ClientSize = new Size(390, 145);

                Label label = new Label();
                label.Text = "Enter the user's full name:";
                label.Location = new Point(15, 15);
                label.AutoSize = true;

                TextBox input = new TextBox();
                input.Location = new Point(15, 42);
                input.Size = new Size(360, 25);
                input.MaxLength = 100;
                input.Text = initialValue ?? "";

                Button okButton = new Button();
                okButton.Text = "OK";
                okButton.Location = new Point(205, 95);
                okButton.Size = new Size(80, 30);
                okButton.DialogResult = DialogResult.OK;

                Button cancelButton = new Button();
                cancelButton.Text = "Cancel";
                cancelButton.Location = new Point(295, 95);
                cancelButton.Size = new Size(80, 30);
                cancelButton.DialogResult = DialogResult.Cancel;

                dialog.Controls.Add(label);
                dialog.Controls.Add(input);
                dialog.Controls.Add(okButton);
                dialog.Controls.Add(cancelButton);

                dialog.AcceptButton = okButton;
                dialog.CancelButton = cancelButton;

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return null;

                return input.Text.Trim();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;
                int roleId = GetSelectedRoleId();

                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentException(
                        "Username is required.");

                if (string.IsNullOrEmpty(password))
                    throw new ArgumentException(
                        "Password is required.");

                string fullName = PromptFullName("");

                if (fullName == null)
                    return;

                userService.AddUser(
                    username,
                    password,
                    fullName,
                    roleId);

                MessageBox.Show(
                    "User added successfully.",
                    "Users Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to add user: " + ex.Message,
                    "Users Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedUserId <= 0)
                {
                    MessageBox.Show(
                        "Please select a user to update.",
                        "Users Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                int userId = selectedUserId;
                string username = txtUsername.Text.Trim();
                int roleId = GetSelectedRoleId();
                bool isActive = chkActive.Checked;

                string fullName =
                    PromptFullName(selectedFullName);

                if (fullName == null)
                    return;

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    DialogResult passwordResult =
                        MessageBox.Show(
                            "Do you also want to change this user's password?",
                            "Confirm Password Change",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                    if (passwordResult != DialogResult.Yes)
                        return;
                }

                userService.UpdateUser(
                    userId,
                    username,
                    fullName,
                    roleId,
                    isActive);

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    userService.ChangePassword(
                        userId,
                        txtPassword.Text);
                }

                MessageBox.Show(
                    "User updated successfully.",
                    "Users Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to update user: " + ex.Message,
                    "Users Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedUserId <= 0)
                {
                    MessageBox.Show(
                        "Please select a user to deactivate.",
                        "Users Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (!chkActive.Checked)
                {
                    MessageBox.Show(
                        "This user is already inactive.",
                        "Users Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to deactivate this user?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                userService.SetUserActive(
                    selectedUserId,
                    false);

                MessageBox.Show(
                    "User deactivated successfully.",
                    "Users Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to deactivate user: " + ex.Message,
                    "Users Management Error",
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
            LoadUsers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void dgvUsers_SelectionChanged(
            object sender, EventArgs e)
        {
            SelectUser();
        }

        private void dgvUsers_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectUser();
        }

        private void lblUsersTitle_Click(object sender, EventArgs e)
        {
        }

        private void pnlSearch_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblUserListTitle_Click(object sender, EventArgs e)
        {
        }

        private void pnlUserList_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblUserDetailsTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblRole_Click(object sender, EventArgs e)
        {
        }

        private void pnlUserDetails_Paint(object sender, PaintEventArgs e)
        {
        }

        private void cboRole_SelectedIndexChanged(
            object sender, EventArgs e)
        {
        }

        private void chkActive_CheckedChanged(
            object sender, EventArgs e)
        {
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
        }
    }
}
