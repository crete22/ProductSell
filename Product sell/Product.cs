using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Product_sell
{
  
      
        public class Product
        {
         public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
            public string Description { get; set; } = string.Empty;
             public int Quality { get; set; } 
            public string Photo { get; set; } = string.Empty;
        }
        


    
}
