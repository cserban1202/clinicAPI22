using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;

namespace clinicAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category1DTO, Category1>().ReverseMap();
            CreateMap<Category1CreationDTO, Category1>();
        }
    }
}
