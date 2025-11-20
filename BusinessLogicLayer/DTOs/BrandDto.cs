using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{ 
   public  class BrandDto
    {
        public int Id { get; set; }
        public string BarndName { get; set; }
        public string BarndDescription { get; set; }
    }

    public class CreateBrandDto
    {

        [Required]
        public string BarndName { get; set; }
        public string BarndDescription { get; set; }
    }

    public class UpdateBrandDto
    {

        [Required]
        public string BarndName { get; set; }
        public string BarndDescription { get; set; }
    }
}
