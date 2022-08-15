using AutoMapper;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Domain.Entities;
using Framework.Core.Extensions;
using Application.Common.Abstractions;

namespace Application.Core.Services
{
    public class ReceiveOrderDtServices : BaseService, IReceiveOrderDtServices
    {
        private readonly IRepository<ReceiveOrder> receiveOrderRepository;
        public ReceiveOrderDtServices(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            receiveOrderRepository = _unitOfWork.GetRepository<ReceiveOrder>();
        }

        public async Task<ReceiveOrderDtResoponse> GetById(Guid id)
        {
            var receiveorderdt = receiveOrderRepository
                            .GetQuery()
                            .ExcludeSoftDeleted()
                            .FilterById(id)
                            .FirstOrDefault();
            var count = _mapper.Map<ReceiveOrderDtResoponse>(receiveorderdt);
            return count;
        }

        public async Task<int> UpdateById(Guid id, ReceiveOrderDtRequest request)
        {
            var count = 0;
            //decimal total = 0;
            //var entity = _unitOfWork
            //                .GetRepository<ReceiveOrderDt>()
            //                .GetQuery()
            //                .FindActiveById(id)
            //                .FirstOrDefault();
            //if (entity == null)
            //    return count;
            //_mapper.Map(request, entity);
            //await receiveOrderDtRepository.UpdateEntityAsync(entity);


            //var order_dt_data = receiveOrderDtRepository
            //                .GetQuery()
            //                .ExcludeSoftDeleted()
            //                .Where(x => x.r_order_cd == entity.r_order_cd)
            //                .ToList();

            //foreach (var item in order_dt_data)
            //{
            //    if (item.id != id)
            //        total += (decimal)item.r_order_cost;
            //}
            //total += request.r_order_cost;

            //var order_data = receiveOrderRepository
            //    .GetQuery()
            //    .ExcludeSoftDeleted()
            //    .FirstOrDefault(x => x.code == entity.r_order_cd);

            //order_data.s_total_cost = total;

            //await receiveOrderRepository.UpdateEntityAsync(order_data);
            //count = await _unitOfWork.SaveChangesAsync();
            return count;

        }
        public async Task<int> DeleteById(Guid id)
        {
            //var entity = receiveOrderDtRepository.GetQuery().FindActiveById(id).FirstOrDefault();
            //if (entity == null) return -1;

            //await receiveOrderDtRepository.DeleteEntityAsync(entity);

            //var order_data = receiveOrderRepository
            //                    .GetQuery()
            //                    .ExcludeSoftDeleted()
            //                    .First(x => x.code == entity.r_order_cd);

            //order_data.s_total_cost = receiveOrderDtRepository
            //                       .GetQuery()
            //                       .ExcludeSoftDeleted()
            //                       .Where(x => x.r_order_cd == entity.r_order_cd && x.id != entity.id)
            //                       .Sum(x => x.r_order_cost);

            //await receiveOrderRepository.UpdateEntityAsync(order_data);

            //var count = await _unitOfWork.SaveChangesAsync();
            return 1;
        }
    }
}
