using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_sell
{
    public class ProductInOrder
    {
        public int Id { get; set; } // Собственный первичный ключ

        // Связь с заказом
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        // Связь с товаром
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } 

        public int Quality { get; set; } // Количество товара в заказе

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } // Цена товара на момент покупки
    }
}
