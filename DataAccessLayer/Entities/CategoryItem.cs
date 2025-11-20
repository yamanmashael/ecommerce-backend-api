using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class CategoryItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]  
        public int CategoryId { get; set; } 

        public Category Category { get; set; }

        [Required]
        public string CategoryItemName { get; set; }

  

        [Required]
        public string CategoryItemDescription { get; set; } 


        public ICollection<Product> Products { get; set; }  
    }
}
