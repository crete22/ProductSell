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
using Product_sell;

namespace Product_sell
{
    public partial class ProductListWindow : Window
    {
        private AdminFacade adminFacade = new AdminFacade();

        public ProductListWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            List<Product> products = adminFacade.GetAllProducts();
            ProductsGrid.ItemsSource = products;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                var editWindow = new EditProductWindow(selectedProduct);
                editWindow.ShowDialog();
                LoadProducts(); // Обновить список после редактирования
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                var deleteWindow = new DeleteProductWindow(selectedProduct);
                deleteWindow.ShowDialog();
                if (deleteWindow.IsDeleted)
                {
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления.");
            }
        }
    }
}
