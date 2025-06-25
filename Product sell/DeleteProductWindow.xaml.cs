using System.Windows;

namespace Product_sell
{
    public partial class DeleteProductWindow : Window
    {
        private Product _product;
        private AdminFacade _adminFacade = new AdminFacade();

        public bool IsDeleted { get; private set; } = false;

        public DeleteProductWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            ProductInfoText.Text = $"Вы уверены, что хотите удалить товар:\n\n{_product.Name}\n(Цена: {_product.Price}, Количество: {_product.Quality})";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _adminFacade.DeleteProduct(_product.Id);
            IsDeleted = true;
            MessageBox.Show("Товар удалён.");
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}