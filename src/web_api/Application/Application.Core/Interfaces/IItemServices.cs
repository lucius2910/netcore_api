using Application.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Application.Core.Interfaces
{
    public interface IItemServices
    {
        Task<IPagedList<ItemResponse>> GetPaged(PagedRequest request);
        Task<IPagedList<ItemResponse>> GetListByType(ItemTypePagedRequest request);
    }
}
