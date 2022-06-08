using AutoMapper;
using PAS.Application.Requests.Identity;
using PAS.Application.Responses.Identity;
using PAS.Infrastructure.Models.Identity;

namespace PAS.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, RoleClaim>()
                .ForMember(nameof(RoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(RoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, RoleClaim>()
                .ForMember(nameof(RoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(RoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}