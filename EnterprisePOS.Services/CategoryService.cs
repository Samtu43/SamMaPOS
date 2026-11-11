
using System;
using System.Collections.Generic;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository repository;

        public CategoryService()
        {
            repository = new CategoryRepository();
        }

        public List<Category> GetAllCategories()
        {
            return repository.GetAllCategories();
        }

        public List<Category> SearchCategories(string keyword)
        {
            return repository.SearchCategories(keyword);
        }

        public Category GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID.");

            return repository.GetCategoryById(categoryId);
        }

        public void AddCategory(string name, string description)
        {
            ValidateCategory(name, description);

            name = name.Trim();
            description = description == null
                ? ""
                : description.Trim();

            if (repository.CategoryNameExists(name))
                throw new InvalidOperationException(
                    "A category with this name already exists.");

            repository.AddCategory(name, description);
        }

        public void UpdateCategory(
            int categoryId,
            string name,
            string description)
        {
            if (categoryId <= 0)
                throw new ArgumentException(
                    "Please select a category to update.");

            ValidateCategory(name, description);

            name = name.Trim();
            description = description == null
                ? ""
                : description.Trim();

            if (repository.GetCategoryById(categoryId) == null)
                throw new InvalidOperationException(
                    "Category not found.");

            if (repository.CategoryNameExists(name, categoryId))
                throw new InvalidOperationException(
                    "A category with this name already exists.");

            repository.UpdateCategory(
                categoryId,
                name,
                description);
        }

        public void DeleteCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException(
                    "Please select a category to delete.");

            if (repository.GetCategoryById(categoryId) == null)
                throw new InvalidOperationException(
                    "Category not found.");

            if (repository.CategoryHasProducts(categoryId))
                throw new InvalidOperationException(
                    "Cannot delete this category because " +
                    "it is assigned to existing products.");

            repository.DeleteCategory(categoryId);
        }

        private void ValidateCategory(
            string name,
            string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Category name is required.");

            if (name.Trim().Length > 100)
                throw new ArgumentException(
                    "Category name cannot exceed 100 characters.");

            if (description != null &&
                description.Trim().Length > 200)
                throw new ArgumentException(
                    "Description cannot exceed 200 characters.");
        }
    }
}
