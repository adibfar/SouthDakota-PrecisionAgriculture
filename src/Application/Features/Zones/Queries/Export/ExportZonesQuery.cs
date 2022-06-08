using MediatR;
using Microsoft.EntityFrameworkCore;
using PAS.Application.Extensions;
using PAS.Application.Interfaces.Repositories;
using PAS.Application.Interfaces.Services;
using PAS.Application.Specifications.Farms;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Zones.Queries.Export
{
    public class ExportZonesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportZonesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportZonesQueryHandler : IRequestHandler<ExportZonesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;

        public ExportZonesQueryHandler(IExcelService excelService, IUnitOfWork<int> unitOfWork)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ExportZonesQuery request, CancellationToken cancellationToken)
        {
            var zoneFilterSpec = new ZoneFilterSpecification(request.SearchString);
            var zones = await _unitOfWork.Repository<Zone>().Entities
                .Specify(zoneFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(zones, mappers: new Dictionary<string, Func<Zone, object>>
            {
                { "Id", item => item.Id },
                { "Name", item => item.Name },
                { "Description", item => item.Description },
            }, sheetName: "Zones");

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}