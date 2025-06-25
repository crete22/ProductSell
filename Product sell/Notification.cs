using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_sell
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Связь с заказом, о котором уведомление
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } 

        // Связь с администратором, которому уведомление
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }
        public Notification()
        {
            CreatedAt = DateTime.UtcNow;

        }
    }
}
