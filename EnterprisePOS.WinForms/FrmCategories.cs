
using System;
using System.Linq;
using System.Windows.Forms;
using EnterprisePOS.Services;

namespace EnterprisePOS.WinForms
{
    public partial class FrmCategories : Form
    {
        private readonly CategoryService categoryService;
        private int selectedCategoryId;
        private bool isLoading;

        public FrmCategories()
        {
            InitializeComponent();

            categoryService = new CategoryService();

            dgvCategories.SelectionChanged +=
                dgvCategories_SelectionChanged;
        }

        private void FrmCategories_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureGrid();
                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load categories: " + ex.Message,
                    "Category Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            dgvCategories.ReadOnly = true;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.MultiSelect = false;
            dgvCategories.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.AutoGenerateColumns = true;
        }

        private void LoadCategories()
        {
            BindCategories(false);
        }

        private void BindCategories(bool search)
        {
            isLoading = true;

            try
            {
                var categories = search
                    ? categoryService.SearchCategories(txtSearch.Text)
                    : categoryService.GetAllCategories();

                var categoryList = categories
                    .Select(c => new
                    {
                        c.CategoryId,
                        c.Name,
                        c.Description
                    })
                    .ToList();

                dgvCategories.DataSource = null;
                dgvCategories.DataSource = categoryList;

                if (dgvCategories.Columns["CategoryId"] != null)
                    dgvCategories.Columns["CategoryId"].Visible = false;

                if (dgvCategories.Columns["Name"] != null)
                    dgvCategories.Columns["Name"].HeaderText =
                        "Category Name";

                if (dgvCategories.Columns["Description"] != null)
                    dgvCategories.Columns["Description"].HeaderText =
                        "Description";

                dgvCategories.ClearSelection();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ClearFields()
        {
            selectedCategoryId = 0;

            txtCategoryName.Clear();
            txtDescription.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            dgvCategories.ClearSelection();
            txtCategoryName.Focus();
        }

        private void SelectCategory()
        {
            if (isLoading || dgvCategories.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = dgvCategories.SelectedRows[0];

            if (row.Cells["CategoryId"].Value == null)
                return;

            selectedCategoryId =
                Convert.ToInt32(row.Cells["CategoryId"].Value);

            txtCategoryName.Text =
                Convert.ToString(row.Cells["Name"].Value);

            txtDescription.Text =
                Convert.ToString(row.Cells["Description"].Value);

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void RefreshCategories()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                LoadCategories();
            else
                BindCategories(true);

            ClearFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                categoryService.AddCategory(
                    txtCategoryName.Text,
                    txtDescription.Text);

                MessageBox.Show(
                    "Category added successfully.",
                    "Category Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to add category: " + ex.Message,
                    "Category Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCategoryId <= 0)
                {
                    MessageBox.Show(
                        "Please select a category to update.",
                        "Category Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                categoryService.UpdateCategory(
                    selectedCategoryId,
                    txtCategoryName.Text,
                    txtDescription.Text);

                MessageBox.Show(
                    "Category updated successfully.",
                    "Category Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to update category: " + ex.Message,
                    "Category Management Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCategoryId <= 0)
                {
                    MessageBox.Show(
                        "Please select a category to delete.",
                        "Category Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this category?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                categoryService.DeleteCategory(selectedCategoryId);

                MessageBox.Show(
                    "Category deleted successfully.",
                    "Category Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to delete category: " + ex.Message,
                    "Category Management Error",
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
            SearchCategories();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchCategories();
        }

        private void SearchCategories()
        {
            try
            {
                BindCategories(true);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to search categories: " + ex.Message,
                    "Search Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvCategories_SelectionChanged(
            object sender, EventArgs e)
        {
            SelectCategory();
        }

        private void dgvCategories_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectCategory();
        }

        private void txtDescription_TextChanged(
            object sender, EventArgs e)
        {
        }

        private void lblDescription_Click(
            object sender, EventArgs e)
        {
        }

        private void txtCategoryName_TextChanged(
            object sender, EventArgs e)
        {
        }

        private void lblCategoryName_Click(
            object sender, EventArgs e)
        {
        }

        private void lblCategoryDetailsTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void pnlCategoryDetails_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblCategoryListTitle_Click(
            object sender, EventArgs e)
        {
        }

        private void pnlCategoryList_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void pnlSearch_Paint(
            object sender, PaintEventArgs e)
        {
        }

        private void lblSearch_Click(
            object sender, EventArgs e)
        {
        }

        private void lblCategoriesTitle_Click(
            object sender, EventArgs e)
        {
        }
    }
}
