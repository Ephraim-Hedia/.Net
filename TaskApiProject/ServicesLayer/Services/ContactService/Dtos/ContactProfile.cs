
using AutoMapper;
using DataAccessLayer.Entites.Identity;

namespace ServicesLayer.Services.ContactService.Dtos
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactResponseDto , Contact>().ReverseMap();
            CreateMap<ContactCreateDto, Contact>().ReverseMap();
        }
    }
}
