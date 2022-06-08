using System.Collections.Generic;
using System.Threading.Tasks;
using PAS.Application.Interfaces.Common;
using PAS.Application.Requests.Identity;
using PAS.Application.Responses.Identity;
using PAS.Shared.Wrapper;

namespace PAS.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}