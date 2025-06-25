using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_sell
{
    public interface IAdminFacade
    {
        Admin Login(string login, string password);
        bool AddProduct(string name, decimal price, int quality, string description);
        bool UpdateProduct(Product productToUpdate);
        bool DeleteProduct(int productId);
        bool ToggleUserBlock(int customerId, bool shouldBeBlocked);
        bool IssueOrder(int orderId, string adminEmail);
        List<Order> GetAllOrders();
        List<Notification> GetActiveNotifications();
        List<Customer> GetAllCustomers();
    }
}
