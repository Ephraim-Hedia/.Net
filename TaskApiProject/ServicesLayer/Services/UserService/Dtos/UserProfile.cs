using AutoMapper;
using DataAccessLayer.Entites.Identity;

namespace ServicesLayer.Services.UserService.Dtos
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
