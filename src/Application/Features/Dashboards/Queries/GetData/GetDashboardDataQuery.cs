using MediatR;
using Microsoft.EntityFrameworkCore;
using PAS.Application.Interfaces.Repositories;
using PAS.Application.Interfaces.Services.Identity;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Wrapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Dashboards.Queries.GetData
{
    public class GetDashboardDataQuery : IRequest<Result<DashboardDataResponse>>
    {

    }

    internal class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, Result<DashboardDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public GetDashboardDataQueryHandler(IUnitOfWork<int> unitOfWork, IUserService userService, IRoleService roleService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<Result<DashboardDataResponse>> Handle(GetDashboardDataQuery query, CancellationToken cancellationToken)
        {
            var response = new DashboardDataResponse
            {
                ZoneCount = await _unitOfWork.Repository<Zone>().Entities.CountAsync(cancellationToken),
                FarmCount = await _unitOfWork.Repository<Farm>().Entities.CountAsync(cancellationToken),
                UserCount = await _userService.GetCountAsync(),
                RoleCount = await _roleService.GetCountAsync()
            };

            var selectedYear = DateTime.Now.Year;
            double[] zonesFigure = new double[13];
            double[] farmsFigure = new double[13];
            for (int i = 1; i <= 12; i++)
            {
                var month = i;
                var filterStartDate = new DateTime(selectedYear, month, 01);
                var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

                zonesFigure[i - 1] = await _unitOfWork.Repository<Zone>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                farmsFigure[i - 1] = await _unitOfWork.Repository<Farm>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
            }

            response.DataEnterBarChart.Add(new ChartSeries { Name = "Zones", Data = zonesFigure });
            response.DataEnterBarChart.Add(new ChartSeries { Name = "Farms", Data = farmsFigure });

            return await Result<DashboardDataResponse>.SuccessAsync(response);
        }
    }
}