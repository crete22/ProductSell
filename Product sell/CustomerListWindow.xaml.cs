using System.Collections.Generic;
using System.Windows;

namespace Product_sell
{
    public partial class CustomerListWindow : Window
    {
        private AdminFacade adminFacade = new AdminFacade();

        public CustomerListWindow()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            List<Customer> customers = adminFacade.GetAllCustomers();
            CustomersGrid.ItemsSource = customers;
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersGrid.SelectedItem is Customer selectedCustomer)
            {
                if (!selectedCustomer.IsBlocked)
                {
                    adminFacade.ToggleUserBlock(selectedCustomer.Id, true);
                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show("Пользователь уже заблокирован.");
                }
            }
            else
            {
                MessageBox.Show("Выберите покупателя для блокировки.");
            }
        }

        private void UnblockButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersGrid.SelectedItem is Customer selectedCustomer)
            {
                if (selectedCustomer.IsBlocked)
                {
                    adminFacade.ToggleUserBlock(selectedCustomer.Id, false);
                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show("Пользователь не заблокирован.");
                }
            }
            else
            {
                MessageBox.Show("Выберите покупателя для разблокировки.");
            }
        }
    }
}