using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class OrderStatus
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string StatusName { get; set; }


        public ICollection<Orders> Orders { get; set; } 

    }
}
