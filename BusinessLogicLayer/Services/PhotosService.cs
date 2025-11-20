using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services
{
    public class PhotosService : IPhotosService
    {

        private readonly IPhotosRepository _repo;

        public PhotosService(IPhotosRepository repo)
        {
            _repo = repo;
        }

            public async Task<IEnumerable<PhotosDto>> GetPhotosByProductItemIdAsync(int productItemId)
            {
                var images = await _repo.GetByProductItemIdAsync(productItemId);
                return images.Select(img => new PhotosDto
                {
                    Id = img.Id,
                    ProductItemId = img.ProductItemId,
                    ImageUrl = img.ImageFilename
                });
            }


    }
}



