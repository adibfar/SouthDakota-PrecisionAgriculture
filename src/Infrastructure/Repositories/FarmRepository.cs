using PAS.Application.Interfaces.Repositories;
using PAS.Domain.Entities.Farms;

namespace PAS.Infrastructure.Repositories
{
    public class FarmRepository : IFarmRepository
    {
        private readonly IRepositoryAsync<Farm, int> _repository;

        public FarmRepository(IRepositoryAsync<Farm, int> repository)
        {
            _repository = repository;
        }
    }
}