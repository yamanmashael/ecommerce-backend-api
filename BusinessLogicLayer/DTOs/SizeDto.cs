using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class SizeDto
    {
        public int Id { get; set; }
        public string SizeNmae { get; set; }
    }

    public class CreateSizeDto
    {
        public string SizeNmae { get; set; }
    }

    public class UpdateSizeDto
    {
        public int Id { get; set; }
        public string SizeNmae { get; set; }
    }

}
