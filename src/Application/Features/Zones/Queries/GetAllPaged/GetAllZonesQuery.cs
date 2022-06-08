using MediatR;
using PAS.Application.Extensions;
using PAS.Application.Interfaces.Repositories;
using PAS.Application.Specifications.Farms;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Wrapper;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Zones.Queries.GetAllPaged
{
    public class GetAllZonesQuery : IRequest<PaginatedResult<GetAllPagedZonesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllZonesQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetAllZonesQueryHandler : IRequestHandler<GetAllZonesQuery, PaginatedResult<GetAllPagedZonesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllZonesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedZonesResponse>> Handle(GetAllZonesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Zone, GetAllPagedZonesResponse>> expression = e => new GetAllPagedZonesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Farm = e.Farm.Name,
                FarmId = e.FarmId
            };
            var zoneFilterSpec = new ZoneFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Zone>().Entities
                   .Specify(zoneFilterSpec)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Zone>().Entities
                   .Specify(zoneFilterSpec)
                   .OrderBy(ordering) // require system.linq.dynamic.core
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;

            }
        }
    }
}