using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductCategory
    {
        [Key] // لتحديد هذا العمود كمفتاح أساسي
        public int PRODUCT_CATEGORY_ID { get; set; }

        // PARENT_CATEGORY_ID يسمح بالقيم الفارغة في DB، لذا نستخدم int?
        public int? PARENT_CATEGORY_ID { get; set; }

        // CATEGORY_NAME nvarchar(100) ويسمح بالقيم الفارغة في DB
        [MaxLength(100)] // تحديد الطول الأقصى بناءً على nvarchar(100)
        public string CATEGORY_NAME { get; set; }

        // CATEGORY_IMAGE nvarchar(255) ويسمح بالقيم الفارغة في DB
        [MaxLength(255)] // تحديد الطول الأقصى بناءً على nvarchar(255)
        public string CATEGORY_IMAGE { get; set; }

        // CATEGORY_DESCRIPTION nvarchar(255) ويسمح بالقيم الفارغة في DB
        [MaxLength(255)] // تحديد الطول الأقصى بناءً على nvarchar(255)
        public string CATEGORY_DESCRIPTION { get; set; }

        // ****** الخاصية الإضافية لبناء شجرة الفئات في الذاكرة ******
        [NotMapped] // تخبر Entity Framework بعدم تخزين هذا العمود في قاعدة البيانات
        public ICollection<ProductCategory> SubCategories { get; set; } = new List<ProductCategory>();
    }
}
