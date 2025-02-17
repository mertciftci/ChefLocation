using ChefLocation.WebApi.Context;
using ChefLocation.WebApi.Contracts;
using ChefLocation.WebApi.Contracts.Chef;
using ChefLocation.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChefLocation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController : ControllerBase
    {

        private readonly ApiContext _context;

        public ChefController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefRequest request)
        {

            var chef = new Chef
            {
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsActive = true,
                NameSurname = request.NameSurname,
                Title = request.Title,
            };
            _context.Chefs.Add(chef);
            await _context.SaveChangesAsync();

            return Ok("Şef ekleme işlemi başarılı");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChef(int id)
        {
            var findChef = await _context.Chefs.FindAsync(id);
            if (findChef == null)
            {
                return BadRequest("Böyle bir şef bulunamadı..");
            }
            findChef.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("Şef silme işlemi başarılı");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChef()
        {
            var chef = await _context.Chefs
                .Select(a => new GetAllChefResponse
                {
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    NameSurname = a.NameSurname,
                    Title = a.Title
                }).ToListAsync();
            return Ok(chef);
        }


    }
}
