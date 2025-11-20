using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class Address
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }  

        public string FulName { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string FullAddress { get; set; }

        public bool IsDefault { get; set; }
    }
}
