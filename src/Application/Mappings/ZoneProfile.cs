using AutoMapper;
using PAS.Application.Features.Zones.Commands.AddEdit;
using PAS.Domain.Entities.Farms;

namespace PAS.Application.Mappings
{
    public class ZoneProfile : Profile
    {
        public ZoneProfile()
        {
            CreateMap<AddEditZoneCommand, Zone>().ReverseMap();
        }
    }
}