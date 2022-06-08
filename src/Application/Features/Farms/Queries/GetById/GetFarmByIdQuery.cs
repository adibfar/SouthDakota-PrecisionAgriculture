using AutoMapper;
using PAS.Application.Interfaces.Repositories;
using PAS.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PAS.Domain.Entities.Farms;

namespace PAS.Application.Features.Farms.Queries.GetById
{
    public class GetFarmByIdQuery : IRequest<Result<GetFarmByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetFarmByIdQueryHandler : IRequestHandler<GetFarmByIdQuery, Result<GetFarmByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetFarmByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetFarmByIdResponse>> Handle(GetFarmByIdQuery query, CancellationToken cancellationToken)
        {
            var farm = await _unitOfWork.Repository<Farm>().GetByIdAsync(query.Id);
            var mappedFarm = _mapper.Map<GetFarmByIdResponse>(farm);
            return await Result<GetFarmByIdResponse>.SuccessAsync(mappedFarm);
        }
    }
}