using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class ProductItemDto
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }  
        public int ColorId { get; set; }    
        public string ColorName { get; set; }
        public decimal Price { get; set; }  
        public decimal SalesPrice { get; set; } 
        public string product_code  { get; set; }
    }



    public class CreateProductItemDto
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public decimal SalesPrice { get; set; }
        public string product_code { get; set; }
    }



    public class UpdateProductItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public decimal SalesPrice { get; set; }
        public string product_code { get; set; }
    }

}
