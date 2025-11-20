using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{


        public class ResponseModel<T>
        {
            public int PageNumber { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }

            public int PageSize { get; set; }
            public bool Success { get; set; }
            public string ErrorMassage { get; set; }
            public T Data { get; set; }

        }
}
