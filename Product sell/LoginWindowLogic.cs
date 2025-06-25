using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_sell
{
    public class LoginWindowLogic
    {
        private AdminFacade adminFacade = new AdminFacade();
        private CustomerFacade customerFacade = new CustomerFacade();

        public bool TryAdminLogin(string login, string password, out Admin admin)
        {
            admin = adminFacade.Login(login, password);
            return admin != null;
        }

        public bool TryCustomerLogin(string login, string password, out Customer customer)
        {
            customer = customerFacade.Login(login, password);
            return customer != null;
        }
    }
}
