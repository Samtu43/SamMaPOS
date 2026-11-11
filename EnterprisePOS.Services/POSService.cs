
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class POSService
    {
        private readonly POSRepository repository;

        public POSService()
        {
            repository = new POSRepository();
        }

        public DataTable GetAvailableProducts()
        {
            return repository.GetAvailableProducts();
        }

        public DataTable SearchProducts(string keyword)
        {
            return repository.SearchProducts(keyword);
        }

        public DataRow GetProductById(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product ID.");

            return repository.GetProductById(productId);
        }

        public int GetAvailableStock(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product ID.");

            return repository.GetAvailableStock(productId);
        }

        public DataTable GetCustomers()
        {
            return repository.GetCustomers();
        }

        public decimal CalculateSubtotal(List<POSCartItem> items)
        {
            ValidateCart(items);

            decimal subtotal = 0;

            foreach (POSCartItem item in items)
            {
                DataRow product =
                    repository.GetProductById(item.ProductId);

                if (product == null)
                    throw new InvalidOperationException(
                        "A product in the cart no longer exists.");

                if (!Convert.ToBoolean(product["IsActive"]))
                    throw new InvalidOperationException(
                        "A product in the cart is inactive.");

                decimal price =
                    Convert.ToDecimal(product["Price"]);

                subtotal += price * item.Quantity;
            }

            return Math.Round(subtotal, 2);
        }

        public decimal CalculateTotal(
            decimal subtotal,
            decimal discountAmount)
        {
            if (subtotal < 0)
                throw new ArgumentException(
                    "Subtotal cannot be negative.");

            if (discountAmount < 0)
                throw new ArgumentException(
                    "Discount cannot be negative.");

            if (discountAmount > subtotal)
                throw new ArgumentException(
                    "Discount cannot exceed the subtotal.");

            return Math.Round(
                subtotal - discountAmount, 2);
        }

        public decimal CalculateChange(
            decimal total,
            decimal amountPaid)
        {
            if (total < 0)
                throw new ArgumentException(
                    "Total cannot be negative.");

            if (amountPaid < total)
                throw new ArgumentException(
                    "Payment is insufficient.");

            return Math.Round(amountPaid - total, 2);
        }

        public void Checkout(
            int userId,
            int? customerId,
            string paymentMethod,
            decimal amountPaid,
            decimal discountAmount,
            List<POSCartItem> items)
        {
            if (userId <= 0)
                throw new ArgumentException(
                    "Invalid cashier account.");

            if (customerId.HasValue && customerId.Value <= 0)
                throw new ArgumentException(
                    "Invalid customer.");

            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new ArgumentException(
                    "Please select a payment method.");

            if (paymentMethod.Trim().Length > 30)
                throw new ArgumentException(
                    "Payment method cannot exceed 30 characters.");

            if (amountPaid < 0)
                throw new ArgumentException(
                    "Payment amount cannot be negative.");

            if (discountAmount < 0)
                throw new ArgumentException(
                    "Discount cannot be negative.");

            ValidateCart(items);

            decimal subtotal = 0;

            foreach (POSCartItem item in items)
            {
                DataRow product =
                    repository.GetProductById(item.ProductId);

                if (product == null)
                    throw new InvalidOperationException(
                        "A product in the cart no longer exists.");

                if (!Convert.ToBoolean(product["IsActive"]))
                    throw new InvalidOperationException(
                        "A product in the cart is inactive.");

                int availableStock =
                    Convert.ToInt32(product["StockQty"]);

                if (item.Quantity > availableStock)
                {
                    throw new InvalidOperationException(
                        "Insufficient stock for " +
                        Convert.ToString(product["Name"]) +
                        ". Available: " +
                        availableStock);
                }

                decimal price =
                    Convert.ToDecimal(product["Price"]);

                subtotal += price * item.Quantity;
            }

            subtotal = Math.Round(subtotal, 2);

            decimal total =
                CalculateTotal(subtotal, discountAmount);

            if (amountPaid < total)
                throw new InvalidOperationException(
                    "Insufficient payment. Total: " +
                    total.ToString("N2"));

            repository.CreateOrder(
                userId,
                customerId,
                paymentMethod.Trim(),
                amountPaid,
                discountAmount,
                items);
        }

        private void ValidateCart(List<POSCartItem> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException(
                    "The shopping cart is empty.");

            foreach (POSCartItem item in items)
            {
                if (item.ProductId <= 0)
                    throw new ArgumentException(
                        "Invalid product in cart.");

                if (item.Quantity <= 0)
                    throw new ArgumentException(
                        "Product quantity must be greater than zero.");
            }

            bool hasDuplicates = items
                .GroupBy(i => i.ProductId)
                .Any(g => g.Count() > 1);

            if (hasDuplicates)
                throw new ArgumentException(
                    "The cart contains duplicate products.");
        }
    }
}
