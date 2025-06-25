using System.Collections.Generic;
using System.Windows;

namespace Product_sell
{
    public partial class CustomerOrdersWindow : Window
    {
        private CustomerFacade customerFacade = new CustomerFacade();
        private Customer _customer;

        public CustomerOrdersWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            LoadOrders();
        }

        private void LoadOrders()
        {
            List<Order> orders = customerFacade.GetCustomerOrders(_customer.Id);
            OrdersGrid.ItemsSource = orders;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem is Order selectedOrder)
            {
                var detailsWindow = new OrderDetailsWindow(selectedOrder);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите заказ для просмотра деталей.");
            }
        }

    }
}