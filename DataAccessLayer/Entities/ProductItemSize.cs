using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class ProductItemSize
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductItem")]
        public int PrductItemId { get; set; }   
        public ProductItem ProductItem { get; set; }


        [ForeignKey("Size")]
        public int SizeId { get; set; } 
        public Size Size { get; set; }

        [Required]
        public int Stock {  get; set; } 



    }
}
