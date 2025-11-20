using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Orders")]
        public int OrdersId { get; set; }   
        public Orders Orders { get; set; }

        [ForeignKey("ProductItem")]
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        [ForeignKey("Size")]
        public int SizeId { get; set; } 
        public Size Size { get; set; }  

        public int Quantity { get; set; }   
        public decimal Price { get; set; }  

    }
}
