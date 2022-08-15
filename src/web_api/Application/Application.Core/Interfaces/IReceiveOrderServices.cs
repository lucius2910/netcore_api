using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Application.Core.Contracts;

namespace Application.Core.Interfaces
{
    public interface IReceiveOrderServices
    {
        Task<int> Create(ReceiveOrderRequest request);
        Task<IPagedList<ReceiveOrderResponse>> GetPaged(ReceiveOrderSearchRequest request);
    }
}
