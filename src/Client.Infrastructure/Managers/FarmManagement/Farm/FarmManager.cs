using PAS.Client.Infrastructure.Extensions;
using PAS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PAS.Application.Features.Farms.Commands.AddEdit;
using PAS.Application.Features.Farms.Queries.GetAll;

namespace PAS.Client.Infrastructure.Managers.FarmManagement.Farm
{
    public class FarmManager : IFarmManager
    {
        private readonly HttpClient _httpClient;

        public FarmManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.FarmsEndpoints.Export
                : Routes.FarmsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.FarmsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllFarmsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.FarmsEndpoints.GetAll);
            return await response.ToResult<List<GetAllFarmsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditFarmCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.FarmsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}