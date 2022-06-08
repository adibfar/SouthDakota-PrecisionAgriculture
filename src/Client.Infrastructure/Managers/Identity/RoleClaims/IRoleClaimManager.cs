using System.Collections.Generic;
using System.Threading.Tasks;
using PAS.Application.Requests.Identity;
using PAS.Application.Responses.Identity;
using PAS.Shared.Wrapper;

namespace PAS.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}