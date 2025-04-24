using coworking_space.BAL.DTOs;
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

        public async Task<Product> AddProductAsync(CreateProductDto dto) {
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
                // Optionally: set IsAvailable based on Quantity
            };

           return await _productRepository.AddAsync(product);
        }
        public async Task<IEnumerable <Product>>GetProductsAsync() {
            return await _productRepository.GetAllAsync();
        }
    }
}
