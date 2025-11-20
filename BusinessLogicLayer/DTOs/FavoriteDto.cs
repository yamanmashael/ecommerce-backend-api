using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{


    public class FavoriteDto
    {
        public int FavoriteId { get; set; }
        public int ProductItemId { get; set; }
         public int productId { get; set; }
        public string BarndName { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public decimal SalesPrice { get; set; }
        public List<ProductFavoriteImageeDto> favoriteImageeDtos { get; set; }

    }

    public class ProductFavoriteImageeDto
    {
        public string ImageFilename { get; set; }
    }

 

    

}
