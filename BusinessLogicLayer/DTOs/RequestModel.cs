using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public  class RequestModel
    {
        public string? Search { get; set; } = null;
        public int PageNumber { get; set; } = 1;

        public int PagesSize { get; set; } = 5;
    }
}
