using AutoMapper;
using clinicAPI.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinicAPI.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ILogger<Category3Controller> logger;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public DoctorsController(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorsDTO>>> Get()
        {
            var doctor = await context.Doctors.ToListAsync();
            return mapper.Map<List<DoctorsDTO>>(doctor);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] DoctorsCreationDTO doctorsCreationDTO)
        {
            var doctors = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doctors == null)
            {
                return NotFound();
            }
            doctors = mapper.Map(doctorsCreationDTO, doctors);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}