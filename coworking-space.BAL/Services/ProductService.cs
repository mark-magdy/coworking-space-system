using coworking_space.BAL.DTOs.ProductDTO;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Services {
    public class ProductService : IProductService {
        public IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        public async Task<ProductReadDto> AddProductAsync(CreateProductDto dto) {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category,
            };

            Product prd = await _productRepository.AddAsync(product);
            var sbe = await _productRepository.SaveAsync();

            return new ProductReadDto
            {
                Id = prd.Id,
                Name = prd.Name,
                Description = prd.Description,
                Price = prd.Price,
                Quantity = prd.Quantity,
                CreatedAt = prd.CreatedAt,
                UpdatedAt = prd.UpdatedAt,
                IsActive = prd.IsActive,
                ImageUrl = prd.ImageUrl,
                Category = prd.Category
            };
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsAsync() {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                IsActive = p.IsActive,
                ImageUrl = p.ImageUrl,
                Category = p.Category
            });
        }

    }
}
