using Application.Common.Abstractions;
using Application.Inventories.Contracts;
using Application.Inventories.Interfaces;
using AutoMapper;
using Domain.Entities;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;

namespace Application.Inventories.Services
{
    public class WarehouseService : BaseService, IWarehouseServices
    {
        private readonly IRepository<Warehouses> warehousesRepository;
        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            warehousesRepository = _unitOfWork.GetRepository<Warehouses>();
        }
        public async Task<IPagedList<WarehouseResponse>> GetPaged(PagedRequest request)
        {
            var data = warehousesRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .Where(x => string.IsNullOrEmpty(request.search) ? true
                                  : x.code.ToLower().Contains(request.search.ToLower()))
                                  .SortBy(request.sort)
                                  .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<WarehouseResponse>>(data);
            return dataMapping;
        }

        public async Task<WarehouseResponse> GetById(Guid id)
        {
            var entity = warehousesRepository
                                 .GetQuery()
                                 .ExcludeSoftDeleted()
                                 .FilterById(id)
                                 .FirstOrDefault();

            var data = _mapper.Map<WarehouseResponse>(entity);
            return data;
        }
        public async Task<int> Create(WarehouseRequest request)
        {
            var warehouses = _mapper.Map<Warehouses>(request);
            await warehousesRepository.AddEntityAsync(warehouses);
            var count = await _unitOfWork.SaveChangesAsync();

            return count;
        }

        public async Task<int> Update(Guid id, WarehouseRequest request)
        {
            var count = 0;
            var entity = _unitOfWork
                            .GetRepository<Warehouses>()
                            .GetQuery()
                            .FindActiveById(id)
                            .FirstOrDefault();

            if (entity == null)
                return count;

            _mapper.Map(request, entity);
            await warehousesRepository.UpdateEntityAsync(entity);
            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
        public async Task<int> Delete(Guid id)
        {
            var entity = warehousesRepository.GetQuery().FindActiveById(id).FirstOrDefault();

            await warehousesRepository.DeleteEntityAsync(entity);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
    }
}
