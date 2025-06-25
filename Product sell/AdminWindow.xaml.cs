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
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var productListWindow = new ProductListWindow();
            productListWindow.ShowDialog();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            
            var productListWindow = new ProductListWindow();
            productListWindow.ShowDialog();
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            var customerListWindow = new CustomerListWindow();
            customerListWindow.ShowDialog();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            var orderListWindow = new OrderListWindow();
            orderListWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Возврат к окну входа
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
