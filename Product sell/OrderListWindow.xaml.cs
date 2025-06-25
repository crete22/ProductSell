using System.Collections.Generic;
using System.Windows;

namespace Product_sell
{
    public partial class OrderListWindow : Window
    {
        private AdminFacade adminFacade = new AdminFacade();

        public OrderListWindow()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            List<Order> orders = adminFacade.GetAllOrders();
            OrdersGrid.ItemsSource = orders;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem is Order selectedOrder)
            {
                MessageBox.Show($"Заказ №{selectedOrder.OrderNumber}\nДата: {selectedOrder.Date}\nПокупатель: {selectedOrder.CustomerId}");
            }
            else
            {
                MessageBox.Show("Выберите заказ для просмотра деталей.");
            }
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem is Order selectedOrder)
            {
                // Здесь можно реализовать логику выдачи заказа (например, вызвать метод IssueOrder)
                bool result = adminFacade.IssueOrder(selectedOrder.Id, "admin@example.com"); // укажите реальный email
                if (result)
                {
                    MessageBox.Show("Заказ выдан и уведомление отправлено!");
                }
                else
                {
                    MessageBox.Show("Ошибка при выдаче заказа.");
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для выдачи.");
            }
        }
    }
}