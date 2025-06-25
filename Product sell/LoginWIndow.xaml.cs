using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Product_sell
{
    public partial class LoginWindow : Window
    {
        private LoginWindowLogic logic = new LoginWindowLogic();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            

            if (logic.TryAdminLogin(login, password, out var admin))
            {
                MessageBox.Show("Вход выполнен как администратор!");
                var adminWindow = new AdminWindow(); // если нужно, можно передать admin
                adminWindow.Show();
                this.Close();
                return;
            }

            if (logic.TryCustomerLogin(login, password, out var customer))
            {
                MessageBox.Show("Вход выполнен как покупатель!");
                var customerWindow = new CustomerWindow(customer);
                customerWindow.Show();
                this.Close();
                return;
            }

            MessageBox.Show("Неверный логин или пароль!");
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }


    }
}
