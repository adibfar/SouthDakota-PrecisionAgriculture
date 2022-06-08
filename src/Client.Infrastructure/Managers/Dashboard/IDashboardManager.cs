using PAS.Shared.Wrapper;
using System.Threading.Tasks;
using PAS.Application.Features.Dashboards.Queries.GetData;

namespace PAS.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}