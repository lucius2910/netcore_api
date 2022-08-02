using Framework.Core.Abstractions;
using Business.SaleOrder.Contracts;

namespace Business.SaleOrder.Interfaces
{
    public interface IReceiveOrderServices
    {
        Task<int> Create(ReceiveOrderRequest request);
        Task<IPagedList<ReceiveOrderResponse>> GetPaged(ReceiveOrderSearchRequest request);
        Task<GetDataResponse> GetData(Guid order_id);
    }
}
