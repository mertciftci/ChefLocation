using ChefLocation.WebApi.Context;
using ChefLocation.WebApi.Contracts;
using ChefLocation.WebApi.Contracts.Feature;
using ChefLocation.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChefLocation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly ApiContext _context;

        public FeatureController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureRequest request)
        {
            var feature = new Feature
            {
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsActive = true,
                SubTitle = request.SubTitle,
                Title = request.Title,
                VideoUrl = request.VideoUrl,
            };
            _context.Features.Add(feature);
            
            await _context.SaveChangesAsync();
            return Ok("Özellik Eklendi.");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeature()
        {
            var featureList = await _context.Features
                .Select(a=>new GetAllFeatureResponse
                {
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    SubTitle = a.SubTitle,
                    Title = a.Title,
                    VideoUrl=a.VideoUrl

                }).ToListAsync();

            return Ok(featureList);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var findFeature = await _context.Features.FindAsync(id);
            if (findFeature == null) 
                return NotFound("Bölye bir feature bulunamadı..");
            findFeature.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("Kayıt silindi");
        }
    }
      
}
