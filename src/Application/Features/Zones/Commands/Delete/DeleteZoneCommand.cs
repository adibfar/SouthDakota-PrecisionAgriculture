using MediatR;
using PAS.Application.Interfaces.Repositories;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Zones.Commands.Delete
{
    public class DeleteZoneCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteZoneCommandHandler : IRequestHandler<DeleteZoneCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteZoneCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteZoneCommand command, CancellationToken cancellationToken)
        {
            var zone = await _unitOfWork.Repository<Zone>().GetByIdAsync(command.Id);
            if (zone != null)
            {
                await _unitOfWork.Repository<Zone>().DeleteAsync(zone);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(zone.Id, "Zone Deleted");
            }
            else
            {
                return await Result<int>.FailAsync("Zone Not Found!");
            }
        }
    }
}