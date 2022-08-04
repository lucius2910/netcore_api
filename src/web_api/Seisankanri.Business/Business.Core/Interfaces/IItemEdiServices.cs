using Business.Core.Contracts;
using Framework.Core.Abstractions;
using Seisankanri.Model.Entities;

namespace Business.Core.Interfaces
{
    public interface IItemEdiServices 
    {
        Task<IPagedList<ItemEdiResponse>> GetPaged(ItemEdiPagedRequest request);

        Task<ItemEdiResponse> GetById(Guid id);

        Task<IList<ItemEdi>> GetByItemAndCompany(string item_cd, string compnay_cd);

        Task<int> Create(ItemEdiCreateRequest request);

        Task<int> Update(Guid id, ItemEdiUpdateRequest request);

        Task<int> Delete(Guid id);
    }
}
