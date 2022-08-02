using AutoMapper;
using Business.Inventories.Contracts;
using Business.Inventories.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Inventories.Services
{
    public class InventoryServices : BaseServices, IInventoryServices
    {
        private readonly IRepository<Inventory> inventoryRepository;
        public InventoryServices(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            inventoryRepository = _unitOfWork.GetRepository<Inventory>();
        }

        public async Task<IPagedList<InventoryResponse>> GetPaged(InventoryPagedRequest request)
        {
            var data = inventoryRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .Where(x => string.IsNullOrEmpty(request.search) ? true 
                                  : (x.code.ToLower().Contains(request.search.ToLower()) 
                                  || x.name.ToLower().Contains(request.search.ToLower())))
                                  .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<InventoryResponse>>(data);
            return dataMapping;
        }

        public async Task<InventoryResponse> GetById(Guid id)
        {
            var entity = inventoryRepository
                                 .GetQuery()
                                 .ExcludeSoftDeleted()
                                 .FilterById(id)
                                 .FirstOrDefault();

            var data = _mapper.Map<InventoryResponse>(entity);
            return data;
        }
        public async Task<int> Create(InventoryRequest request)
        {
            var inventory = _mapper.Map<Inventory>(request);
            await inventoryRepository.AddEntityAsync(inventory);
            var count = await _unitOfWork.SaveChangesAsync();

            return count;
        }

        public async Task<int> Update(Guid id, InventoryRequest request)
        {
            var count = 0;
            var entity = _unitOfWork
                            .GetRepository<Inventory>()
                            .GetQuery()
                            .FindActiveById(id)
                            .FirstOrDefault();

            if (entity == null)
                return count;

            _mapper.Map(request, entity);
            await inventoryRepository.UpdateEntityAsync(entity);
            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
        public async Task<int> Delete(Guid id)
        {
            var entity = inventoryRepository.GetQuery().FindActiveById(id).FirstOrDefault();

            await inventoryRepository.DeleteEntityAsync(entity);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Delete(Guid[] ids)
        {
            var entitys = inventoryRepository.GetQuery().FilterByIds(ids).ToList();

            foreach (var item in entitys)
            {
                await inventoryRepository.DeleteEntityAsync(item);
            }
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
    }
}
