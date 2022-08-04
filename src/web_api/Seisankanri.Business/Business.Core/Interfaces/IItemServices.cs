using Business.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Business.Core.Interfaces
{
    public interface IItemServices
    {
        Task<IPagedList<ItemResponse>> GetPaged(ItemSearchRequest request);
        Task<IPagedList<GetItemByTypeResponse>> GetListByType(ItemTypePagedRequest request);
        Task<ItemResponse> GetById(Guid id);
    }
}
