using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
  
    public class ColorDto
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
       
    }

    public class CreateColorDto
    {
        public string ColorName { get; set; }
      
    }

    public class UpdateColorDto
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        
    }

}
