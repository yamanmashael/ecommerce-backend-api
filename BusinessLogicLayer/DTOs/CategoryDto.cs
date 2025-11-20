using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; } // للعرض
    }

    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public int GenderId { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string CategoryName { get; set; }
        public int GenderId { get; set; }
    }


}
