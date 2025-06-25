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
    public partial class EditProductWindow : Window
    {
        private Product _product;

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            _product = product;

            // Заполните поля окна данными выбранного товара
            string name = NameBox.Text.Trim();
            string priceText = PriceBox.Text.Trim();
            string quantityText = QuantityBox.Text.Trim();
            string description = DescriptionBox.Text.Trim();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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

            // Обновляем объект _product
            _product.Name = name;
            _product.Price = price;
            _product.Quality = quantity;
            _product.Description = description;

            // Сохраняем изменения через AdminFacade
            var adminFacade = new AdminFacade();
            bool result = adminFacade.UpdateProduct(_product);

            if (result)
            {
                MessageBox.Show("Товар успешно обновлен!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении товара.");
            }
        }
    }
}
