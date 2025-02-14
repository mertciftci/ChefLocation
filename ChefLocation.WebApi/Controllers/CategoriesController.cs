using System.Drawing.Text;
using ChefLocation.WebApi.Context;
using ChefLocation.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
             await _context.SaveChangesAsync();
            return  Ok ("Kategori ekleme işlemi başarılı..");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var findCategory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(findCategory);
            await _context.SaveChangesAsync();
            return Ok("Silme işlemi başarılı..");
        }
    }
}
