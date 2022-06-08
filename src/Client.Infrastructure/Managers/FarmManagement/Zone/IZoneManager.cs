using PAS.Application.Features.Zones.Commands.AddEdit;
using PAS.Application.Features.Zones.Queries.GetAllPaged;
using PAS.Application.Requests.Farms;
using PAS.Shared.Wrapper;
using System.Threading.Tasks;

namespace PAS.Client.Infrastructure.Managers.FarmManagement.Zone
{
    public interface IZoneManager : IManager
    {
        Task<PaginatedResult<GetAllPagedZonesResponse>> GetZonesAsync(GetAllPagedZonesRequest request);

        Task<IResult<int>> SaveAsync(AddEditZoneCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}