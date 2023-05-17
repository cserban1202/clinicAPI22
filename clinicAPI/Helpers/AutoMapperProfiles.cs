using AutoMapper;
using clinicAPI.DTOs;
using clinicAPI.Entitites;
using Microsoft.AspNet.Identity.CoreCompat;

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

            CreateMap<Category3DTO, Category3>().ReverseMap();
            CreateMap<Category3CreationDTO, Category3>();

            CreateMap<DoctorsDTO, Doctors>().ReverseMap();

            CreateMap<Category4DTO, Category4>().ReverseMap();
            CreateMap<Category4CreationDTO, Category4>();

            CreateMap<IdentityUser, UserDTO>();

            CreateMap<ConsultationDTO, Consultation>().ReverseMap();
            CreateMap<ConsultationCreationDTO, Consultation>();

            CreateMap<ExaminationDTO, Examination>().ReverseMap();
            CreateMap<ExaminationCreationDTO, Examination>();

        }
    }
}
