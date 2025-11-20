using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; } 
        public Product Product { get; set; }    

        [ForeignKey("Color")]
        public int ColorId { get; set; }        
        public Color Color { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal SalesPrice { get; set; }

        public string product_code { get; set; }

        public DateTime CreatedDate { get; set; }


        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductItemSize> ProductItemSizes { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }  
        public ICollection<CartItem> cartItems { get; set; }
        public ICollection<Favorites> Favorites { get; set; }   




    }
}
