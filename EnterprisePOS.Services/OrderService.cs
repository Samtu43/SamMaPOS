
using System;
using System.Data;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class OrderService
    {
        private readonly OrderRepository repository;

        public OrderService()
        {
            repository = new OrderRepository();
        }

        public DataTable GetAllOrders()
        {
            return repository.GetAllOrders();
        }

        public DataTable SearchOrders(string keyword)
        {
            return repository.SearchOrders(keyword);
        }

        public DataTable GetOrders(
            string keyword,
            DateTime fromDate,
            DateTime toDate)
        {
            if (fromDate.Date > toDate.Date)
                throw new ArgumentException(
                    "From Date cannot be later than To Date.");

            return repository.GetOrders(
                keyword == null ? "" : keyword.Trim(),
                fromDate,
                toDate);
        }

        public DataTable GetOrderDetails(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException(
                    "Please select a valid order.");

            return repository.GetOrderDetails(orderId);
        }

        public DataRow GetOrderById(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException(
                    "Please select a valid order.");

            return repository.GetOrderById(orderId);
        }
    }
}
