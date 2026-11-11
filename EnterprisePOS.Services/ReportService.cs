
using System;
using System.Data;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class ReportService
    {
        private readonly ReportRepository repository;

        public ReportService()
        {
            repository = new ReportRepository();
        }

        public DataTable GetDailySales(DateTime fromDate, DateTime toDate)
        {
            if (fromDate.Date > toDate.Date)
            {
                throw new ArgumentException(
                    "From Date cannot be later than To Date.");
            }

            return repository.GetDailySales(fromDate, toDate);
        }

        public DataTable GetTopProducts()
        {
            return repository.GetTopProducts();
        }

        public DataTable GetSalesByCategory()
        {
            return repository.GetSalesByCategory();
        }

        public DataTable GetLowStock()
        {
            return repository.GetLowStock();
        }

        public DataTable GetDashboardSummary()
        {
            return repository.GetDashboardSummary();
        }
    }
}
