using AutoMapper;
using TES_Learning_App.Application_Layer.Dtos;
using TES_Learning_App.Domain.Entities;

namespace TES_Learning_App.API.Profiles
{
    public class LevelProfile : Profile
    {
        public LevelProfile()
        {
            // Source -> Target
            CreateMap<Level, LevelResponseDTO>()
                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.Language.LanguageName));

            CreateMap<CreateLevelDTO, Level>();
            CreateMap<UpdateLevelDTO, Level>();
        }
    }
}
