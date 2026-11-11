
using System;
using System.Data;
using EnterprisePOS.Data;

namespace EnterprisePOS.Services
{
    public class DashboardService
    {
        private readonly DashboardRepository repository =
            new DashboardRepository();

        public DataTable GetStatistics()
        {
            return repository.GetDashboardStatistics();
        }

        public DataTable GetRecentOrders()
        {
            return repository.GetRecentOrders();
        }

        public DataTable GetLowStock()
        {
            return repository.GetLowStock();
        }

        public bool CanAccess(string role, string module)
        {
            if (string.Equals(
                role,
                "Admin",
                StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(
                role,
                "Manager",
                StringComparison.OrdinalIgnoreCase))
            {
                return module != "Users";
            }

            if (string.Equals(
                role,
                "Cashier",
                StringComparison.OrdinalIgnoreCase))
            {
                return module == "Dashboard" ||
                       module == "POS" ||
                       module == "Customers" ||
                       module == "Orders";
            }

            return false;
        }
    }
}
