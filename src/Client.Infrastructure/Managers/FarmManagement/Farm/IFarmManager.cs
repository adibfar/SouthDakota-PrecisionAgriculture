using PAS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using PAS.Application.Features.Farms.Commands.AddEdit;
using PAS.Application.Features.Farms.Queries.GetAll;

namespace PAS.Client.Infrastructure.Managers.FarmManagement.Farm
{
    public interface IFarmManager : IManager
    {
        Task<IResult<List<GetAllFarmsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditFarmCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}