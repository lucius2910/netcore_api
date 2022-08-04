using Business.Core.Contracts;
using Framework.Core.Abstractions;

namespace Business.Core.Interfaces
{
    public interface IItemPriceMasterService
    {
        Task<IPagedList<ItemPriceResponse>> GetPaged(ItemPricePagedRequest request);
        Task<ItemPriceMasterRespones> GetByCode(string code);

        Task<int> CreateUpdate(ItemPriceMasterRequest request);
        Task<int> Delete(Guid id);
    }
}
