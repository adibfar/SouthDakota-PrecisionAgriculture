using PAS.Application.Interfaces.Common;

namespace PAS.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}