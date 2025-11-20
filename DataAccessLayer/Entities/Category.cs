using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class Category
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
      
        [Required]
        public string CategoryName { get; set; }    


        public ICollection<CategoryItem> CategoryItems { get; set; }    

    }
}
