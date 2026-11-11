
using System;
using System.Collections.Generic;
using System.Data;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class InventoryService
    {
        private readonly InventoryRepository repository;

        public InventoryService()
        {
            repository = new InventoryRepository();
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public List<Product> SearchProducts(string keyword)
        {
            return repository.SearchProducts(keyword);
        }

        public Product GetProductById(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product ID.");

            return repository.GetProductById(productId);
        }

        public DataTable GetInventoryHistory()
        {
            return repository.GetInventoryHistory();
        }

        public int AdjustStock(
            int productId,
            string adjustmentType,
            int quantity,
            string reason,
            int userId)
        {
            if (productId <= 0)
                throw new ArgumentException(
                    "Please select a product.");

            if (adjustmentType != "Stock In" &&
                adjustmentType != "Stock Out")
                throw new ArgumentException(
                    "Please select a valid adjustment type.");

            if (quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException(
                    "Please enter a reason for the adjustment.");

            if (reason.Trim().Length > 200)
                throw new ArgumentException(
                    "Reason cannot exceed 200 characters.");

            if (userId <= 0)
                throw new ArgumentException(
                    "A valid logged-in user is required.");

            Product product = repository.GetProductById(productId);

            if (product == null)
                throw new InvalidOperationException(
                    "Product not found or inactive.");

            int changeQty = adjustmentType == "Stock In"
                ? quantity
                : -quantity;

            if (changeQty < 0 && product.StockQty < quantity)
                throw new InvalidOperationException(
                    "Insufficient stock. Available quantity: " +
                    product.StockQty);

            return repository.AdjustStock(
                productId,
                changeQty,
                reason.Trim(),
                userId);
        }
    }
}
