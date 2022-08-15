using AutoMapper;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Abstractions;

namespace Application.Core.Services
{
    public class ReceiveOrderServices : BaseService, IReceiveOrderServices
    {
        private readonly IRepository<ReceiveOrder> orderRepository;
        private readonly IRepository<User> userRepository;
        private readonly ICurrentUserService _serviceContext;

        public ReceiveOrderServices(IUnitOfWork _unitOfWork, IMapper _mapper, ICurrentUserService serviceContext) : base(_unitOfWork, _mapper)
        {
            orderRepository = _unitOfWork.GetRepository<ReceiveOrder>();
            userRepository = _unitOfWork.GetRepository<User>();
            _serviceContext = serviceContext;
        }

        public async Task<int> Create(ReceiveOrderRequest request)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var data_user = userRepository.GetQuery().ExcludeSoftDeleted().FirstOrDefault(x => x.id == _serviceContext.user_id);
                    if (data_user == null) return -1;

                    // Check Dulicate item_cd
                    foreach (var entity in request.details)
                    {
                        var getOrderDt = request.details.Where(x => x.item_cd == entity.item_cd).ToList();

                        if (getOrderDt.Count() > 1)
                        {
                            return -1;
                        }
                    }
                    var receiveOrder = _mapper.Map<ReceiveOrder>(request);
                    await orderRepository.AddEntityAsync(receiveOrder);
                    var count = await _unitOfWork.SaveChangesAsync();


                    count = await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    return count;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }
        
        public async Task<IPagedList<ReceiveOrderResponse>> GetPaged(ReceiveOrderSearchRequest request)
        {
            var data = orderRepository
                       .GetQuery()
                       .ExcludeSoftDeleted()
                       .Where(x => x.order_date >= request.order_date_start_date)
                       .Where(x => string.IsNullOrEmpty(request.order_date_end) ? true : x.order_date <= request.order_date_end_date)
                       .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<ReceiveOrderResponse>>(data);
            return dataMapping;
        }
    }
}


