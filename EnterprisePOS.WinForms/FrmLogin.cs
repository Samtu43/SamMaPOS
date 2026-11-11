
using System;
using System.Drawing;
using System.Windows.Forms;
using EnterprisePOS.Data;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmLogin : Form
    {
        private readonly AuthService authService = new AuthService();

        public FrmLogin()
        {
            InitializeComponent();

            txtPassword.UseSystemPasswordChar = true;
            chkShowPassword.Checked = false;
            lblMessage.Text = "";
            AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            lblMessage.Text = "";

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowMessage("Please enter your username.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowMessage("Please enter your password.");
                txtPassword.Focus();
                return;
            }

            btnLogin.Enabled = false;

            try
            {
                LoginUser user = authService.Login(username, password);

                if (user == null)
                {
                    ShowMessage("Invalid username or password.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                FrmDashboard dashboard = new FrmDashboard(user);

                dashboard.FormClosed += (s, args) =>
                {
                    txtPassword.Clear();
                    lblMessage.Text = "";
                    Show();
                    txtUsername.Focus();
                };

                dashboard.Show();
                Hide();
            }
            catch (Exception)
            {
                ShowMessage(
                    "Unable to log in. Please contact the administrator."
                );
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void ShowMessage(string message)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = message;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        private void lblAppName_Click(object sender, EventArgs e)
        {
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {
        }

        private void pnlLoginCard_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
