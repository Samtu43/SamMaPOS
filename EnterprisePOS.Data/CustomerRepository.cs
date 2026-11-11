
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterprisePOS.Data
{
    public class CustomerRepository
    {
        public List<Customer> GetAllCustomers()
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Customers
                    .OrderBy(c => c.FullName)
                    .ToList();
            }
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            using (var db = new PosDBDataDataContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return db.Customers
                        .OrderBy(c => c.FullName)
                        .ToList();
                }

                keyword = keyword.Trim();

                return db.Customers
                    .Where(c =>
                        c.FullName.Contains(keyword) ||
                        (c.Phone != null &&
                         c.Phone.Contains(keyword)) ||
                        (c.Email != null &&
                         c.Email.Contains(keyword)))
                    .OrderBy(c => c.FullName)
                    .ToList();
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Customers.FirstOrDefault(
                    c => c.CustomerId == customerId);
            }
        }

        public bool CustomerHasOrders(int customerId)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Orders.Any(
                    o => o.CustomerId == customerId);
            }
        }

        public void AddCustomer(
            string fullName,
            string phone,
            string email,
            string address)
        {
            using (var db = new PosDBDataDataContext())
            {
                Customer customer = new Customer
                {
                    FullName = fullName,
                    Phone = phone,
                    Email = email,
                    Address = address,
                    CreatedAt = DateTime.Now
                };

                db.Customers.InsertOnSubmit(customer);
                db.SubmitChanges();
            }
        }

        public void UpdateCustomer(
            int customerId,
            string fullName,
            string phone,
            string email,
            string address)
        {
            using (var db = new PosDBDataDataContext())
            {
                Customer customer = db.Customers.FirstOrDefault(
                    c => c.CustomerId == customerId);

                if (customer == null)
                    throw new InvalidOperationException(
                        "Customer not found.");

                customer.FullName = fullName;
                customer.Phone = phone;
                customer.Email = email;
                customer.Address = address;

                db.SubmitChanges();
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (var db = new PosDBDataDataContext())
            {
                Customer customer = db.Customers.FirstOrDefault(
                    c => c.CustomerId == customerId);

                if (customer == null)
                    throw new InvalidOperationException(
                        "Customer not found.");

                if (db.Orders.Any(
                    o => o.CustomerId == customerId))
                {
                    throw new InvalidOperationException(
                        "Cannot delete this customer because " +
                        "they have existing orders.");
                }

                db.Customers.DeleteOnSubmit(customer);
                db.SubmitChanges();
            }
        }
    }
}
