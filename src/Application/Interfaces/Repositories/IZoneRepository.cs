using System.Threading.Tasks;

namespace PAS.Application.Interfaces.Repositories
{
    public interface IZoneRepository
    {
        Task<bool> IsFarmUsed(int farmId);
    }
}