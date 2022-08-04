using AutoMapper;
using Business.SaleOrder.Contracts;
using Business.SaleOrder.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Seisankanri.Model.Core;
using System.Transactions;

namespace Business.SaleOrder.Services
{
    public class ReceiveOrderServices : BaseServices, IReceiveOrderServices
    {
        private readonly IRepository<ReceiveOrder> orderRepository;
        private readonly IRepository<ReceiveOrderDt> orderDtRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Company> companyRepository;
        private readonly IServiceContext _serviceContext;

        public ReceiveOrderServices(IUnitOfWork _unitOfWork, IMapper _mapper, IServiceContext serviceContext) : base(_unitOfWork, _mapper)
        {
            orderRepository = _unitOfWork.GetRepository<ReceiveOrder>();
            orderDtRepository = _unitOfWork.GetRepository<ReceiveOrderDt>();
            userRepository = _unitOfWork.GetRepository<User>();
            _serviceContext = serviceContext;
            companyRepository = _unitOfWork.GetRepository<Company>();
        }

        public async Task<int> Create(ReceiveOrderRequest request)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var data_user = userRepository.GetQuery().ExcludeSoftDeleted().FirstOrDefault(x => x.id == _serviceContext._userId);
                    if (data_user == null) return -1;

                    // Check Duplicate item_cd
                    foreach (var entity in request.details)
                    {
                        var getOrderDt = request.details.Where(x => x.item_cd == entity.item_cd).ToList();

                        if (getOrderDt.Count() > 1)
                        {
                            return -1;
                        }
                    }
                    var receiveOrder = _mapper.Map<ReceiveOrder>(request);
                    receiveOrder.order_status = ReceiveOrderStatus.UnConfirmed;
                    receiveOrder.user_cd = data_user.code;
                    await orderRepository.AddEntityAsync(receiveOrder);
                    var count = await _unitOfWork.SaveChangesAsync();

                    foreach (var item in request.details)
                    {
                        var childItem = _mapper.Map<ReceiveOrderDt>(item);
                        childItem.r_order_cd = receiveOrder.code;
                        childItem.company_cd = request.company_cd;
                        childItem.warehouse_cd = request.company_cd;
                        await orderDtRepository.AddEntityAsync(childItem);
                    }

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

        public async Task<GetDataResponse> GetData(Guid order_id)
        {
            var entity = orderRepository
                        .GetQuery()
                        .ExcludeSoftDeleted().Include(x => x.company).Include(x => x.details)
                        .Where(x => x.id == order_id)
                        .Where(x => x.company.branch_f == 1 && x.company.code.Equals(x.company_cd))
                        .FirstOrDefault();
            

            var dataMapping = _mapper.Map<GetDataResponse>(entity);
            return dataMapping;
        }

        public async Task<IPagedList<ReceiveOrderResponse>> GetPaged(ReceiveOrderSearchRequest request)
        {
            var data = orderRepository
                       .GetQuery()
                       .ExcludeSoftDeleted()
                       .Where(x => x.order_date >= request.order_date_start_date)
                       .Where(x => string.IsNullOrEmpty(request.order_date_end) ? true : x.order_date <= request.order_date_end_date)
                       .Include(x => x.company)
                       .Where(x => x.company.customer_person_name.Contains(request.customer_person_name))
                       .Where(x => x.company_cd == request.company_cd)
                       .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<ReceiveOrderResponse>>(data);
            return dataMapping;
        }


    }
}


