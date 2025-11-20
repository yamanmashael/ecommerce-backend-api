    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class ProductImage
    {
        [Key]
        public  int Id { get; set; }

        [ForeignKey("ProductItem")]
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        [Required]
        public string ImageFilename { get; set; }

    }
}
