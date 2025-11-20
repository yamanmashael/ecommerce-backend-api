using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
        public  class CartItem
        {
            [Key]
            public int Id { get; set; }

            [ForeignKey("Cart")]
            public int CartId { get; set; }
            public Cart Cart { get; set; } = null!;

            [ForeignKey("ProductItem")]
            public int ProductItemId { get; set; }
            public ProductItem ProductItem { get; set; } = null!;

            [ForeignKey("Size")]
            public int SizeId { get; set; }
             public Size Size { get; set; }
           public int Quantity { get; set; }
        }
}
