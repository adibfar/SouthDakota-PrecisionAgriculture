using PAS.Application.Interfaces.Common;
using PAS.Application.Requests.Identity;
using PAS.Application.Responses.Identity;
using PAS.Shared.Wrapper;
using System.Threading.Tasks;

namespace PAS.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}