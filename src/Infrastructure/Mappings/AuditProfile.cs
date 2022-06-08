using AutoMapper;
using PAS.Infrastructure.Models.Audit;
using PAS.Application.Responses.Audit;

namespace PAS.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}