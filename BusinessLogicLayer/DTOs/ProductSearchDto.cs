using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public  class ProductSearchDto
    {
        public string? SearchText { get; set; }

        // يجب تهيئته كقائمة فارغة لتجنب NullReferenceException
        public List<int> CategoryId { get; set; } = new List<int>();

        // يجب تهيئته كقائمة فارغة لتجنب NullReferenceException
        public List<int> BrandId { get; set; } = new List<int>();

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
