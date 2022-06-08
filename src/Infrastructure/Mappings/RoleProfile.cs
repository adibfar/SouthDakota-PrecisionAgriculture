using AutoMapper;
using PAS.Infrastructure.Models.Identity;
using PAS.Application.Responses.Identity;

namespace PAS.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, Role>().ReverseMap();
        }
    }
}