using AutoMapper;
using MediatR;
using PAS.Application.Interfaces.Repositories;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Constants.Application;
using PAS.Shared.Wrapper;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Farms.Commands.AddEdit
{
    public partial class AddEditFarmCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    internal class AddEditFarmCommandHandler : IRequestHandler<AddEditFarmCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditFarmCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditFarmCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var farm = _mapper.Map<Farm>(command);
                await _unitOfWork.Repository<Farm>().AddAsync(farm);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllFarmsCacheKey);
                return await Result<int>.SuccessAsync(farm.Id, "Farm Saved");
            }
            else
            {
                var farm = await _unitOfWork.Repository<Farm>().GetByIdAsync(command.Id);
                if (farm != null)
                {
                    farm.Name = command.Name ?? farm.Name;
                    farm.Description = command.Description ?? farm.Description;
                    await _unitOfWork.Repository<Farm>().UpdateAsync(farm);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllFarmsCacheKey);
                    return await Result<int>.SuccessAsync(farm.Id, "Farm Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Farm Not Found!");
                }
            }
        }
    }
}