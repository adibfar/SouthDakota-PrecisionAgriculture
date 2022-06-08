using AutoMapper;
using MediatR;
using PAS.Application.Interfaces.Repositories;
using PAS.Application.Interfaces.Services;
using PAS.Application.Requests;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Wrapper;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Zones.Commands.AddEdit
{
    public partial class AddEditZoneCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int FarmId { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditZoneCommandHandler : IRequestHandler<AddEditZoneCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;

        public AddEditZoneCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<Result<int>> Handle(AddEditZoneCommand command, CancellationToken cancellationToken)
        {
            //var uploadRequest = command.UploadRequest;
            //if (uploadRequest != null)
            //{
            //    uploadRequest.FileName = $"P-{command.Barcode}{uploadRequest.Extension}";
            //}

            if (command.Id == 0)
            {
                var zone = _mapper.Map<Zone>(command);
                await _unitOfWork.Repository<Zone>().AddAsync(zone);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(zone.Id, "Zone Saved");
            }
            else
            {
                var zone = await _unitOfWork.Repository<Zone>().GetByIdAsync(command.Id);
                if (zone != null)
                {
                    zone.Name = command.Name ?? zone.Name;
                    zone.Description = command.Description ?? zone.Description;
                    zone.FarmId = command.FarmId == 0 ? zone.FarmId : command.FarmId;
                    await _unitOfWork.Repository<Zone>().UpdateAsync(zone);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(zone.Id, "Zone Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Zone Not Found!");
                }
            }
        }
    }
}