using coworking_space.BAL.DTOs;
using coworking_space.BAL.Interaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coworking_space.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto dto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.AddProductAsync(dto);

            var product = new {
                Id = 1, // This should be replaced with the actual product ID after creation
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category
            };
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // Optional GET for CreatedAtAction support
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id) {
            // Just stubbed here, implement based on your use case
            return Ok();
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetProducts() {
            var products = await _productService.GetProductsAsync();
            if (products == null || !products.Any()) {
                return NotFound("No products found.");
            }
            return Ok(products);
        }
    }
}