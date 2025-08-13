using AutoMapper;
using TinyFeetBackend.DTOs.Auth;
using TinyFeetBackend.Entities;


namespace TinyFeetBackend.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegistrationDto, User>().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UserLoginDto, User>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
    
    
}
