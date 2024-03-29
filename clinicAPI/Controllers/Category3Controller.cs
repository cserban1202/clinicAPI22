﻿using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinicAPI.Controllers
{


    [Route("api/category3")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsClient")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Category3Controller: ControllerBase
    {
        private readonly ILogger<Category3Controller> logger;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public Category3Controller(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<List<Category3DTO>>> Get()
        {
            var category3 = await context.Categories3.ToListAsync();
            return mapper.Map<List<Category3DTO>>(category3);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Category3CreationDTO category3CreationDTO)
        {

            var category3 = await context.Categories3.FirstOrDefaultAsync(x => x.Id == id);

            if (category3 == null)
            {
                return NotFound();
            }
            category3 = mapper.Map(category3CreationDTO, category3);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
