using BusinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImageDto>> GetImagesByProductItemIdAsync(int productItemId);
        Task<int> AddImageAsync(ProductImageCreateDto dto);
        Task<int> DeleteImageAsync(int id);
    }
}
