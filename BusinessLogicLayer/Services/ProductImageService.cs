using Azure.Storage.Blobs;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _repository;
        private readonly IConfiguration _configuration;

        public ProductImageService(IProductImageRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProductImageDto>> GetImagesByProductItemIdAsync(int productItemId)
        {
            var images = await _repository.GetByProductItemIdAsync(productItemId);

            return images.Select(img => new ProductImageDto
            {
                Id = img.Id,
                ProductItemId = img.ProductItemId,
                ImageUrl = "https://yamanstore.blob.core.windows.net/product-photos/" + img.ImageFilename
            });
        }

        public async Task<int> AddImageAsync(ProductImageCreateDto dto)
        {
            if (dto.ImageFile == null || !dto.ImageFile.ContentType.StartsWith("image/"))
                throw new Exception("Uploaded file is not a valid image");

            string connectionString = _configuration["Storage:Azure"];
            string containerName = "product-photos";

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            string fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = dto.ImageFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            var productImage = new ProductImage
            {
                ProductItemId = dto.ProductItemId,
                ImageFilename = fileName
            };

            await _repository.AddAsync(productImage);
            await _repository.SaveChangesAsync();

            return productImage.ProductItemId;
        }

        public async Task<int> DeleteImageAsync(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null) throw new Exception("Image not found");

            string connectionString = _configuration["Storage:Azure"];
            string containerName = "product-photos";

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(image.ImageFilename);

            await blobClient.DeleteIfExistsAsync();

            await _repository.DeleteAsync(image);
            await _repository.SaveChangesAsync();

            return image.ProductItemId;
        }

        private string GenerateBlobUrl(string fileName)
        {
            string containerName = "product-photos";
            string accountName = _configuration["Storage:AccountName"];
            return $"https://{accountName}.blob.core.windows.net/{containerName}/{fileName}";
        }
    }
}
