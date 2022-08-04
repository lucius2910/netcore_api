using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Business.Inventories.Contracts;

namespace Business.Inventories.Interfaces
{
    public interface IWarehouseServices
    {
        Task<IPagedList<WarehouseResponse>> GetPaged(WarehousePagedRequest request);
        Task<int> Create(WarehouseRequest request);
        Task<WarehouseResponse> GetById(Guid id);
        Task<int> Update(Guid id, WarehouseRequest request);
        Task<int> Delete(Guid id);
    }
}
