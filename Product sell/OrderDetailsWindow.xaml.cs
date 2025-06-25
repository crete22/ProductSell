using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Product_sell
{
    public partial class OrderDetailsWindow : Window
    {
        public OrderDetailsWindow(Order order)
        {
            InitializeComponent();

            OrderInfoText.Text = $"Заказ №{order.OrderNumber} от {order.Date}";

            // Получаем все ProductInOrder для этого заказа
            var db = new DatabaseFacade();
            var productsInOrder = db.GetAllEntities<ProductInOrder>()
                .Where(pio => pio.OrderId == order.Id)
                .ToList();

            // Для удобства добавим вычисляемое свойство Total (сумма по позиции)

            var displayList = productsInOrder.Select(pio => new
            {
                Product = db.GetEntityById<Product>(pio.ProductId).Name,
                Quality = pio.Quality,
                Price = pio.Price,
                Total = pio.Price * pio.Quality
            }).ToList();

            ProductsInOrderGrid.ItemsSource = displayList;
        }
    }
}