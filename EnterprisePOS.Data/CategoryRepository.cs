
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterprisePOS.Data
{
    public class CategoryRepository
    {
        public List<Category> GetAllCategories()
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Categories
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }

        public List<Category> SearchCategories(string keyword)
        {
            using (var db = new PosDBDataDataContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return db.Categories
                        .OrderBy(c => c.Name)
                        .ToList();
                }

                keyword = keyword.Trim();

                return db.Categories
                    .Where(c => c.Name.Contains(keyword) ||
                        (c.Description != null &&
                         c.Description.Contains(keyword)))
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Categories.FirstOrDefault(
                    c => c.CategoryId == categoryId);
            }
        }

        public bool CategoryNameExists(
            string name,
            int excludeCategoryId = 0)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Categories.Any(
                    c => c.Name == name &&
                         c.CategoryId != excludeCategoryId);
            }
        }

        public bool CategoryHasProducts(int categoryId)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Products.Any(
                    p => p.CategoryId == categoryId);
            }
        }

        public void AddCategory(string name, string description)
        {
            using (var db = new PosDBDataDataContext())
            {
                Category category = new Category
                {
                    Name = name,
                    Description = description
                };

                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();
            }
        }

        public void UpdateCategory(
            int categoryId,
            string name,
            string description)
        {
            using (var db = new PosDBDataDataContext())
            {
                Category category = db.Categories.FirstOrDefault(
                    c => c.CategoryId == categoryId);

                if (category == null)
                    throw new InvalidOperationException(
                        "Category not found.");

                category.Name = name;
                category.Description = description;

                db.SubmitChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (var db = new PosDBDataDataContext())
            {
                Category category = db.Categories.FirstOrDefault(
                    c => c.CategoryId == categoryId);

                if (category == null)
                    throw new InvalidOperationException(
                        "Category not found.");

                if (db.Products.Any(
                    p => p.CategoryId == categoryId))
                {
                    throw new InvalidOperationException(
                        "Cannot delete this category because it is " +
                        "currently assigned to one or more products.");
                }

                db.Categories.DeleteOnSubmit(category);
                db.SubmitChanges();
            }
        }
    }
}
