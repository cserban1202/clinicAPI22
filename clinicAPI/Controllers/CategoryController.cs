using clinicAPI.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using clinicAPI.DTOs;
using AutoMapper;

namespace clinicAPI.Controllers
{
    [Route("api/category1")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        
        private readonly ILogger<CategoryController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoryController(ILogger<CategoryController> logger, 
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<Category1DTO>>> Get()
        {
            logger.LogInformation("getting all the categories");
            var category1 = await context.Categories.ToListAsync();
            return mapper.Map<List<Category1DTO>>(category1);   
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Category1CreationDTO category1CreationDTO)
        {
            var category1 = mapper.Map<Category1DTO>(category1CreationDTO);
            context.Add(category1);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Category1 category1)
        {

            throw new NotImplementedException();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{Id:int}\n", Name = "getGenre")]   //api/genres/example
        public ActionResult<Category1> Get(int Id)
        {

            throw new NotImplementedException();
        }
    }
}
