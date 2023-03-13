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
using clinicAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace clinicAPI.Controllers
{
    [Route("api/category1")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsClient")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        

        [HttpGet] // api/categories
        public async Task<ActionResult<List<Category1DTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Categories.AsQueryable();
            await HttpContext.InsertParameterPaginationInHeader(queryable);
            var category = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<Category1DTO>>(category);   
        }


        [HttpGet("{Id:int}")]   //api/genres/example
        public async Task<ActionResult<Category1DTO>> Get(int Id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            return mapper.Map<Category1DTO>(category);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Category1CreationDTO category1CreationDTO)
        {
            var category = mapper.Map<Category1>(category1CreationDTO);
            context.Add(category);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Category1CreationDTO category1CreationDTO)
        {

            var category1 = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(category1 == null)
            {
                return NotFound(); 
            }
            category1 = mapper.Map(category1CreationDTO, category1);     
            await context.SaveChangesAsync();              
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Categories.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Category1() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
