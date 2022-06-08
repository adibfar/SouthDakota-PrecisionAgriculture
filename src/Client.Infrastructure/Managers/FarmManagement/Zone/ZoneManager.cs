using PAS.Application.Features.Zones.Commands.AddEdit;
using PAS.Application.Features.Zones.Queries.GetAllPaged;
using PAS.Application.Requests.Farms;
using PAS.Client.Infrastructure.Extensions;
using PAS.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PAS.Client.Infrastructure.Managers.FarmManagement.Zone
{
    public class ZoneManager : IZoneManager
    {
        private readonly HttpClient _httpClient;

        public ZoneManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.ZonesEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.ZonesEndpoints.Export
                : Routes.ZonesEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedZonesResponse>> GetZonesAsync(GetAllPagedZonesRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.ZonesEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedZonesResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditZoneCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ZonesEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}