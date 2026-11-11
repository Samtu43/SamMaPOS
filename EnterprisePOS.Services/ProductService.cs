
using EnterprisePOS.Data;
using System;
using System.Collections.Generic;

namespace EnterprisePOS.Services
{
    public class ProductService
    {
        private readonly ProductRepository repository;

        public ProductService()
        {
            repository = new ProductRepository();
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public Product GetProductById(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product ID.");

            return repository.GetProductById(productId);
        }

        public List<Product> SearchProducts(string keyword)
        {
            return repository.SearchProducts(keyword);
        }

        public List<Category> GetAllCategories()
        {
            return repository.GetAllCategories();
        }

        public void AddProduct(
            string sku,
            string name,
            int categoryId,
            decimal price,
            decimal cost,
            int stockQty,
            int userId)
        {
            ValidateProduct(sku, name, categoryId, price, cost);

            if (stockQty < 0)
                throw new ArgumentException(
                    "Stock quantity cannot be negative.");

            if (userId <= 0)
                throw new ArgumentException(
                    "A valid logged-in user is required.");

            repository.AddProduct(
                sku.Trim(),
                name.Trim(),
                categoryId,
                price,
                cost,
                stockQty,
                userId);
        }

        public void UpdateProduct(
            int productId,
            string sku,
            string name,
            int categoryId,
            decimal price,
            decimal cost)
        {
            if (productId <= 0)
                throw new ArgumentException(
                    "Please select a product to update.");

            ValidateProduct(sku, name, categoryId, price, cost);

            repository.UpdateProduct(
                productId,
                sku.Trim(),
                name.Trim(),
                categoryId,
                price,
                cost);
        }

        public void DeleteProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException(
                    "Please select a product to delete.");

            repository.DeleteProduct(productId);
        }

        private void ValidateProduct(
            string sku,
            string name,
            int categoryId,
            decimal price,
            decimal cost)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU is required.");

            if (sku.Trim().Length > 30)
                throw new ArgumentException(
                    "SKU cannot exceed 30 characters.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Product name is required.");

            if (name.Trim().Length > 150)
                throw new ArgumentException(
                    "Product name cannot exceed 150 characters.");

            if (categoryId <= 0)
                throw new ArgumentException(
                    "Please select a category.");

            if (price < 0)
                throw new ArgumentException(
                    "Price cannot be negative.");

            if (cost < 0)
                throw new ArgumentException(
                    "Cost cannot be negative.");
        }
    }
}
