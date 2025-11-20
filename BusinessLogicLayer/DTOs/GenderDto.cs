using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class GenderDto
    {
        public int Id { get; set; }
        public string GenderName { get; set; }
    }

    public class CreateGenderDto
    {
        public string GenderName { get; set; }
    }

    public class UpdateGenderDto
    {
        public string GenderName { get; set; }
    }
}
