using AutoMapper;
using LazyCache;
using MediatR;
using PAS.Application.Interfaces.Repositories;
using PAS.Domain.Entities.Farms;
using PAS.Shared.Constants.Application;
using PAS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PAS.Application.Features.Farms.Queries.GetAll
{
    public class GetAllFarmsQuery : IRequest<Result<List<GetAllFarmsResponse>>>
    {
        public GetAllFarmsQuery()
        {
        }
    }

    internal class GetAllFarmsCachedQueryHandler : IRequestHandler<GetAllFarmsQuery, Result<List<GetAllFarmsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllFarmsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllFarmsResponse>>> Handle(GetAllFarmsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Farm>>> getAllFarms = () => _unitOfWork.Repository<Farm>().GetAllAsync();
            var farmList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllFarmsCacheKey, getAllFarms);
            var mappedFarms = _mapper.Map<List<GetAllFarmsResponse>>(farmList);
            return await Result<List<GetAllFarmsResponse>>.SuccessAsync(mappedFarms);
        }
    }
}