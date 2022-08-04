using Business.SaleOrder.Contracts;

namespace Business.SaleOrder.Interfaces
{
    public interface IReceiveOrderDtServices
    {
        Task<ReceiveOrderDtResoponse> GetById(Guid id);
        Task<int> UpdateById(Guid id, ReceiveOrderDtRequest request);
        Task<int> DeleteById(Guid id);
    }
}
