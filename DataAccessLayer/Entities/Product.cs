using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CategoryItem")]
        public int CategoryItemId { get; set; } 
        public CategoryItem CategoryItem { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }    
        public Brand Brand { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }    

        public ICollection<ProductItem> ProductItems { get; set; }  

        
    }
}
