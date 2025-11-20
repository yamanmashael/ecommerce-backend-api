using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public int ProductItemId { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ProductImageCreateDto
    {
        public int ProductItemId { get; set; }
        public IFormFile ImageFile { get; set; }
    }


}
