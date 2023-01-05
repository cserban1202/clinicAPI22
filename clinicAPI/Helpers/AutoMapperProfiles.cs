using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;

namespace clinicAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category1DTO, Category1>().ReverseMap();
            CreateMap<Category1CreationDTO, Category1>();

            CreateMap<Category2DTO, Category2>().ReverseMap();
            CreateMap<Category2CreationDTO, Category2>()
                .ForMember(x => x.Picture, options => options.Ignore());
        }
    }
}
