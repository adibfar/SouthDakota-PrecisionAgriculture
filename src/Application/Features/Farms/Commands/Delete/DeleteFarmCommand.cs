using MediatR;
using PAS.Application.Interfaces.Repositories;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Constants.Application;
using PAS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Farms.Commands.Delete
{
    public class DeleteFarmCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteFarmCommandHandler : IRequestHandler<DeleteFarmCommand, Result<int>>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteFarmCommandHandler(IUnitOfWork<int> unitOfWork, IZoneRepository zoneRepository)
        {
            _unitOfWork = unitOfWork;
            _zoneRepository = zoneRepository;
        }

        public async Task<Result<int>> Handle(DeleteFarmCommand command, CancellationToken cancellationToken)
        {
            var isFarmUsed = await _zoneRepository.IsFarmUsed(command.Id);
            if (!isFarmUsed)
            {
                var farm = await _unitOfWork.Repository<Farm>().GetByIdAsync(command.Id);
                if (farm != null)
                {
                    await _unitOfWork.Repository<Farm>().DeleteAsync(farm);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllFarmsCacheKey);
                    return await Result<int>.SuccessAsync(farm.Id, "Farm Deleted");
                }
                else
                {
                    return await Result<int>.FailAsync("Farm Not Found!");
                }
            }
            else
            {
                return await Result<int>.FailAsync("Deletion Not Allowed");
            }
        }
    }
}