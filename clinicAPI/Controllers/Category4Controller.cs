using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;
using clinicAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinicAPI.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class Category4Controller: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public Category4Controller(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] Category4CreationDTO category4CreationDTO)
        {
            var category4 = await context.Categories2.FirstOrDefaultAsync(x => x.Id == id);
            if (category4 == null)
            {
                return NotFound();
            }
            category4 = mapper.Map(category4CreationDTO, category4);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Category4CreationDTO category4CreationDTO)
        {
            var category4 = mapper.Map<Category4>(category4CreationDTO);
  
            context.Add(category4);
            await context.SaveChangesAsync();
            return NoContent();
            
        }
    }
}
