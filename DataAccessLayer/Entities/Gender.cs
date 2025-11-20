using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public  class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GenderName { get; set; }

        public ICollection<Category> Categories  { get; set; }   
    }
}
