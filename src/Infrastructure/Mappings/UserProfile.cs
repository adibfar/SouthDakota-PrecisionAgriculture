using AutoMapper;
using PAS.Infrastructure.Models.Identity;
using PAS.Application.Responses.Identity;

namespace PAS.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, User>().ReverseMap();
            CreateMap<ChatUserResponse, User>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}