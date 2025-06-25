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
    public partial class AddProductWindow : Window
    {
        private AdminFacade adminFacade = new AdminFacade();

        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            string priceText = PriceBox.Text.Trim();
            string quantityText = QuantityBox.Text.Trim();
            string description = DescriptionBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(priceText) || string.IsNullOrWhiteSpace(quantityText))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену!");
                return;
            }

            if (!int.TryParse(quantityText, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Введите корректное количество!");
                return;
            }

            bool result = adminFacade.AddProduct(name, price, quantity, description);

            if (result)
            {
                MessageBox.Show("Товар успешно добавлен!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении товара.");
            }
        }
    }
}
