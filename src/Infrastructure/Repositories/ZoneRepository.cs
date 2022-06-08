using PAS.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PAS.Domain.Entities.Farms;

namespace PAS.Infrastructure.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly IRepositoryAsync<Zone, int> _repository;

        public ZoneRepository(IRepositoryAsync<Zone, int> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsFarmUsed(int farmId)
        {
            return await _repository.Entities.AnyAsync(b => b.FarmId == farmId);
        }
    }
}