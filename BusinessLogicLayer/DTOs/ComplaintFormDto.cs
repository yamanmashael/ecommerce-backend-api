using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public  class ComplaintFormDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Complaint { get; set; }
        public string OrderNo { get; set; }
        public string PhoneNumber { get; set; }
        public string Body { get; set; }
    }
}
