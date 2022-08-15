using Application.Inventories.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Application.Inventories.Interfaces
{
    public interface IWarehouseServices
    {
        Task<IPagedList<WarehouseResponse>> GetPaged(PagedRequest request);
        Task<int> Create(WarehouseRequest request);
        Task<WarehouseResponse> GetById(Guid id);
        Task<int> Update(Guid id, WarehouseRequest request);
        Task<int> Delete(Guid id);
    }
}
