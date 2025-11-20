using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Favorites
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductItem")]
        public int ProductItemId { get; set; }   
        public ProductItem ProductItem { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } 
        public User User { get; set; }  

    }
}
