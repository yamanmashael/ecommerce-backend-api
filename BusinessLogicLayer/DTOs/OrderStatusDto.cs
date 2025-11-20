using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
    }

    public class CreateOrderStatusDto
    {
        public string StatusName { get; set; }
    }

    public class UpdateOrderStatusDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
    }

}
