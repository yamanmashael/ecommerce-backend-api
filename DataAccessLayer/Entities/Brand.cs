using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class Brand
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string BarndName { get; set; }

        public string BarndDescription { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
