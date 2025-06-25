using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_sell
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }

        // Связь с покупателем
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        // Связь с товарами в заказе
        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; }

        public Order()
        {
            ProductInOrders = new HashSet<ProductInOrder>(); // Инициализация коллекции
            Date = DateTime.UtcNow;
        }
    }
}
