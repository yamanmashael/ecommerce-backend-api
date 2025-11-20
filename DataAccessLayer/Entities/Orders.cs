using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } 
        public User User { get; set; }


        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; }  
        public OrderStatus OrderStatus { get; set; }    

        public string Adress { get; set; }  
        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }



        public string PaymentMethod { get; set; }
        public bool PaymentStatus { get; set; }
        public string  ShippingCost { get; set; }
        public string ShippingCompany { get; set; }
        public string TrackingNumber { get; set; }
      

        public ICollection<OrderDetails> OrderDetails { get; set; } 
    }
}
