
using System;
using System.Collections.Generic;
using System.Net.Mail;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository repository;

        public CustomerService()
        {
            repository = new CustomerRepository();
        }

        public List<Customer> GetAllCustomers()
        {
            return repository.GetAllCustomers();
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return repository.SearchCustomers(keyword);
        }

        public Customer GetCustomerById(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Invalid customer ID.");

            return repository.GetCustomerById(customerId);
        }

        public void AddCustomer(
            string fullName,
            string phone,
            string email,
            string address)
        {
            ValidateCustomer(fullName, phone, email, address);

            repository.AddCustomer(
                fullName.Trim(),
                NormalizeOptional(phone),
                NormalizeOptional(email),
                NormalizeOptional(address));
        }

        public void UpdateCustomer(
            int customerId,
            string fullName,
            string phone,
            string email,
            string address)
        {
            if (customerId <= 0)
                throw new ArgumentException(
                    "Please select a customer to update.");

            ValidateCustomer(fullName, phone, email, address);

            if (repository.GetCustomerById(customerId) == null)
                throw new InvalidOperationException(
                    "Customer not found.");

            repository.UpdateCustomer(
                customerId,
                fullName.Trim(),
                NormalizeOptional(phone),
                NormalizeOptional(email),
                NormalizeOptional(address));
        }

        public void DeleteCustomer(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException(
                    "Please select a customer to delete.");

            if (repository.GetCustomerById(customerId) == null)
                throw new InvalidOperationException(
                    "Customer not found.");

            if (repository.CustomerHasOrders(customerId))
                throw new InvalidOperationException(
                    "Cannot delete this customer because " +
                    "they have existing orders.");

            repository.DeleteCustomer(customerId);
        }

        private void ValidateCustomer(
            string fullName,
            string phone,
            string email,
            string address)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException(
                    "Customer name is required.");

            if (fullName.Trim().Length > 100)
                throw new ArgumentException(
                    "Customer name cannot exceed 100 characters.");

            if (!string.IsNullOrWhiteSpace(phone) &&
                phone.Trim().Length > 30)
                throw new ArgumentException(
                    "Phone number cannot exceed 30 characters.");

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Trim().Length > 120)
                    throw new ArgumentException(
                        "Email cannot exceed 120 characters.");

                try
                {
                    var emailAddress = new MailAddress(email.Trim());

                    if (!string.Equals(
                        emailAddress.Address,
                        email.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    throw new ArgumentException(
                        "Please enter a valid email address.");
                }
            }

            if (!string.IsNullOrWhiteSpace(address) &&
                address.Trim().Length > 250)
                throw new ArgumentException(
                    "Address cannot exceed 250 characters.");
        }

        private string NormalizeOptional(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }
    }
}
