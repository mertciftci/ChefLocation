using ChefLocation.WebApi.Context;
using ChefLocation.WebApi.Contracts;
using ChefLocation.WebApi.Contracts.Product;
using ChefLocation.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChefLocation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProductController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var product = new Product
            {
                ImageUrl = request.ImageUrl,
                IsActive = true,
                Price = request.Price,  
                ProcductName = request.ProcductName,
                ProductDescription = request.ProductDescription,
                
            };
            _context.Products.Add(product); 
            await _context.SaveChangesAsync();
            return Ok("Ürün eklendi..");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var findProduct = await _context.Products
                .Select(a => new GetAllProductResponse
                {
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    ProcductName = a.ProcductName,
                    ProductDescription = a.ProductDescription
                }).ToListAsync();
            return Ok(findProduct); 
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var findProduct = await _context.Products.FindAsync(id);
            findProduct.IsActive=false;
            await _context.SaveChangesAsync();
            return Ok("Ürün silindi..");
        }
    }
}
