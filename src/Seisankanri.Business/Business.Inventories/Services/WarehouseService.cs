using AutoMapper;
using Business.Inventories.Contracts;
using Business.Inventories.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Inventories.Services
{
    public class WarehouseService : BaseServices, IWarehouseServices
    {
        private readonly IRepository<Warehouses> warehousesRepository;
        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            warehousesRepository = _unitOfWork.GetRepository<Warehouses>();
        }
        public async Task<IPagedList<WarehouseResponse>> GetPaged(WarehousePagedRequest request)
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
