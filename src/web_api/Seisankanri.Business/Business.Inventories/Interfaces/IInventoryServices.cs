using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Business.Inventories.Contracts;

namespace Business.Inventories.Interfaces
{
    public interface IInventoryServices
    {
        Task<IPagedList<InventoryResponse>> GetPaged(InventoryPagedRequest request);
        Task<int> Create(InventoryRequest request);
        Task<InventoryResponse> GetById(Guid id);
        Task<int> Update(Guid id, InventoryRequest request);
        Task<int> Delete(Guid id);
        Task<int> Delete(Guid[] ids);
    }
}
