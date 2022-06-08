using AutoMapper;
using PAS.Application.Features.Farms.Commands.AddEdit;
using PAS.Application.Features.Farms.Queries.GetAll;
using PAS.Application.Features.Farms.Queries.GetById;
using PAS.Domain.Entities.Farms;

namespace PAS.Application.Mappings
{
    public class FarmProfile : Profile
    {
        public FarmProfile()
        {
            CreateMap<AddEditFarmCommand, Farm>().ReverseMap();
            CreateMap<GetFarmByIdResponse, Farm>().ReverseMap();
            CreateMap<GetAllFarmsResponse, Farm>().ReverseMap();
        }
    }
}