using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinicAPI.Controllers
{

    [Route("api/examination")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public ExaminationController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ExaminationCreationDTO examinationCreationDTO)
        {
            var examination = mapper.Map<Examination>(examinationCreationDTO);
            context.Add(examination);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult<List<ExaminationDTO>>> Get()
        {
            var examination = await context.Examinations.ToListAsync();
            return mapper.Map<List<ExaminationDTO>>(examination);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Examinations.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Examination() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
