using System.Drawing.Text;
using ChefLocation.WebApi.Context;
using ChefLocation.WebApi.Contracts;
using ChefLocation.WebApi.Contracts.Category;
using ChefLocation.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChefLocation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;

        public CategoriesController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            var category = new Category
            {
                CategoryName = request.CategoryName,
                IsActive = true
            };
            await _context.Categories.AddAsync(category);
             await _context.SaveChangesAsync();

            return Ok ("Kategori ekleme işlemi başarılı..");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var findCategory = await _context.Categories.FindAsync(id);
            findCategory.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("Kategori silme işlemi başarılı..");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _context.Categories
                .Select(a=> new GetAllCategoryResponse
                {
                    CategoryName=a.CategoryName
                }).ToListAsync();
            return Ok(categories);

        }
    }
}
