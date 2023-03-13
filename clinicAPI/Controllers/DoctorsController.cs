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
    }
}