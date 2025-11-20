using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class Size 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SizeNmae { get; set; }


        public ICollection<ProductItemSize> ProductItemSizes { get; set; }
        public ICollection<OrderDetails> OrderDetails  { get; set; }
        public ICollection<CartItem> CartItems {  get; set; }    


    }
}
