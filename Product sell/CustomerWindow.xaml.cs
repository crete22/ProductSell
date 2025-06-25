using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Product_sell
{
    public partial class CustomerWindow : Window
    {
        private CustomerFacade customerFacade = new CustomerFacade();
        private Customer _customer;
        private List<(Product, int)> cart = new List<(Product, int)>(); // корзина

        public CustomerWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            LoadProducts();
        }

        private void LoadProducts()
        {
            List<Product> products = customerFacade.GetAllProducts();
            ProductsGrid.ItemsSource = products;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                if (!int.TryParse(QuantityBox.Text.Trim(), out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество!");
                    return;
                }

                if (selectedProduct.Quality < quantity)
                {
                    MessageBox.Show("На складе недостаточно товара!");
                    return;
                }

                cart.Add((selectedProduct, quantity));
                MessageBox.Show($"Товар \"{selectedProduct.Name}\" в количестве {quantity} добавлен в заказ.");
            }
            else
            {
                MessageBox.Show("Выберите товар для добавления в заказ.");
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста! Добавьте товары для заказа.");
                return;
            }

            var order = customerFacade.CreateOrder(_customer, cart);

            if (order != null)
            {
                MessageBox.Show("Заказ успешно оформлен!");
                cart.Clear();
                LoadProducts(); // обновить остатки
            }
            else
            {
                MessageBox.Show("Ошибка при оформлении заказа.");
            }
        }

        // Обработчик двойного клика по товару (карточка товара)
        private void ProductsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                var detailsWindow = new ProductDetailsWindow(selectedProduct);
                detailsWindow.ShowDialog();
            }
        }
        private void MyOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var ordersWindow = new CustomerOrdersWindow(_customer);
            ordersWindow.ShowDialog();
        }
    }
}