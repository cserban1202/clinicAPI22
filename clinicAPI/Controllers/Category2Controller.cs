using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;
using clinicAPI.Helpers;
using clinicAPI.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinicAPI.Controllers
{
    [Route("api/category2")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsClient")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Category2Controller : ControllerBase

    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "reviews";

        public Category2Controller(ApplicationDbContext context, IMapper mapper,
            IFileStorageService fileStorageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category2DTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Categories2.AsQueryable();
            await HttpContext.InsertParameterPaginationInHeader(queryable);
            var category2 = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<Category2DTO>>(category2);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category2DTO>> Get(int id)
        {
            var category2 = await context.Categories2.FirstOrDefaultAsync(x => x.Id == id);
            if (category2 == null)
            {
                return NotFound();
            }
            return mapper.Map<Category2DTO>(category2);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] Category2CreationDTO category2CreationDTO)
        {
            var category2 = mapper.Map<Category2>(category2CreationDTO);
            if(category2CreationDTO.Picture != null)
            {
                category2.Picture = await fileStorageService.SaveFile(containerName, category2CreationDTO.Picture);
            }
            context.Add(category2);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] Category2CreationDTO category2CreationDTO)
        {
            var category2 = await context.Categories2.FirstOrDefaultAsync(x => x.Id == id);
            if (category2 == null)
            {
                return NotFound();
            }
            category2 = mapper.Map(category2CreationDTO, category2);
            if (category2CreationDTO.Picture != null)
            {
                category2.Picture = await fileStorageService.EditFile(containerName,
                    category2CreationDTO.Picture, category2.Picture);
            }
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category2 = await context.Categories2.FirstOrDefaultAsync(x => x.Id == id);

            if (category2 == null)
            {
                return NotFound();
            }
            context.Remove(category2);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(category2.Picture, containerName);
            return NoContent();
        }
    }
}
