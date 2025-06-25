using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_sell
{
    public interface ICustomerFacade
    {
        bool Register(string login, string password, string surname, string name, string patronymic, string phoneNumber, string mail);
        Customer Login(string login, string password);
        List<Product> GetAllProducts();
        Order CreateOrder(Customer customer, List<(Product product, int quantity)> products);
        List<Order> GetCustomerOrders(int customerId);
    }
}
