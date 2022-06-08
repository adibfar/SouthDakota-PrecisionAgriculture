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

namespace PAS.Application.Features.Farms.Queries.Export
{
    public class ExportFarmsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportFarmsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportFarmsQueryHandler : IRequestHandler<ExportFarmsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;

        public ExportFarmsQueryHandler(IExcelService excelService, IUnitOfWork<int> unitOfWork)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ExportFarmsQuery request, CancellationToken cancellationToken)
        {
            var farmFilterSpec = new FarmFilterSpecification(request.SearchString);
            var farm = await _unitOfWork.Repository<Farm>().Entities
                .Specify(farmFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(farm, mappers: new Dictionary<string, Func<Farm, object>>
            {
                { "Id", item => item.Id },
                { "Name", item => item.Name },
                { "Description", item => item.Description },
            }, sheetName: "Farms");

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
