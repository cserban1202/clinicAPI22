﻿using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace clinicAPI.Controllers
{

    [Route("api/consultation")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public ConsultationController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ConsultationCreationDTO consultationCreationDTO)
        {
            var consultation = mapper.Map<Consultation>(consultationCreationDTO);
            context.Add(consultation);
            await context.SaveChangesAsync();
            return NoContent();
        }

        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] Category1CreationDTO category1CreationDTO)
        //{
        //    var category = mapper.Map<Category1>(category1CreationDTO);
        //    context.Add(category);
        //    await context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}
