using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Product_sell
{
    public partial class ProductDetailsWindow : Window
    {
        public ProductDetailsWindow(Product product)
        {
            InitializeComponent();

            NameText.Text = product.Name;
            PriceText.Text = $"Цена: {product.Price} руб.";
            QuantityText.Text = $"В наличии: {product.Quality}";
            DescriptionText.Text = product.Description;

            // Загрузка изображения, если путь указан и файл существует
            if (!string.IsNullOrEmpty(product.Photo) && File.Exists(product.Photo))
            {
                ProductImage.Source = new BitmapImage(new Uri(product.Photo, UriKind.RelativeOrAbsolute));
            }
        }
    }
}