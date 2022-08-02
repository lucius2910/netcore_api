using AutoMapper;
using Business.Core.Interfaces;
using Business.Plan.Contracts;
using Business.Plan.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Seisankanri.Model.Entities;

namespace Business.Plan.Services
{
    public class SalePlanServices : BaseServices, ISalePlanServices
    {
        private readonly IRepository<SalePlan> _salePlanRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<ItemEdi> _itemEdiRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<Classification> _classificationRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IItemEdiServices _itemEdiServices;
        protected readonly IServiceContext _serviceContext;

        public SalePlanServices(IUnitOfWork _unitOfWork, IMapper _mapper, IItemEdiServices itemEdiServices, IServiceContext serviceContext) : base(_unitOfWork, _mapper)
        {
            _salePlanRepository = _unitOfWork.GetRepository<SalePlan>();
            _itemRepository = _unitOfWork.GetRepository<Item>();
            _companyRepository = _unitOfWork.GetRepository<Company>();
            _classificationRepository = _unitOfWork.GetRepository<Classification>();
            _itemEdiRepository = _unitOfWork.GetRepository<ItemEdi>();
            _userRepository = _unitOfWork.GetRepository<User>();
            _itemEdiServices = itemEdiServices;
            _serviceContext = serviceContext;
        }

        public async Task<IList<SalePlanByMonthResponse>> GetByMonth(SalePlanByMonthRequest request)
        {
            var next_month = 4;
            var start_month = new DateTime(request.order_ym_fomated.Value.Year, request.order_ym_fomated.Value.Month, 1);
            var last_month = start_month.AddMonths(next_month).AddDays(1).AddSeconds(-1);

            // get edi_cd by item and company
            var item_edis = await _itemEdiServices.GetByItemAndCompany(null, request.company_cd);

            // get item
            var items = _itemRepository
                               .GetQuery()
                               .ExcludeSoftDeleted()
                               .Include(x => x.item_sale_plans
                                               .Where(y => y.del_flg == false)
                                               .Where(y => y.order_ym >= start_month && y.order_ym <= last_month)
                                               .Where(y => string.IsNullOrEmpty(request.company_cd) || y.company_cd.ToLower().Contains(request.company_cd.ToLower()))
                                        )
                               .Include(x => x.item_edis)
                               .Where(x => string.IsNullOrEmpty(request.item_edi_cd) || x.item_edis.Where(y => y.edi_cd.ToLower().Contains(request.item_edi_cd.ToLower().Trim())).Any())
                               .Where(x => string.IsNullOrEmpty(request.item_type) || x.item_type.ToLower() == request.item_type.ToLower())
                               .Where(x => string.IsNullOrEmpty(request.item_class_cd2) || x.class_cd2.ToLower() == (request.item_class_cd2.ToLower()))
                               .Where(x => string.IsNullOrEmpty(request.item_name1) || x.name1.ToLower().Contains(request.item_name1.ToLower().Trim()))
                               .OrderBy(x => x.order)
                               .ThenBy(x => x.code)
                               .ToList();
            
            // get classification
            var classifications = _classificationRepository.GetQuery().ExcludeSoftDeleted();

            var data_res = new List<SalePlanByMonthResponse>();
            foreach (var item in items)
            {
                var class_name2 = classifications.Where(x => x.code2 == item.class_cd2).Select(x => x.name1).FirstOrDefault();
                var item_edi_cd = item_edis.Where(x => x.item_cd == item.code && x.company_cd == request.company_cd).Select(x => x.edi_cd).FirstOrDefault();
                var sale_item = new SalePlanByMonthResponse()
                {
                    company_cd = request.company_cd,
                    item_type = item.item_type,
                    order_unit = item.std_unit,
                    item_cd = item.code,
                    item_edi_cd = item_edi_cd,
                    item_name1 = item.name1,
                    item_name2 = item.name2,
                    standad_unit_qty = item.unit_conv_rate,
                    item_class_name2 = class_name2
                };

                var plan_month = item.item_sale_plans
                                    .GroupBy(x => new { request.company_cd, x.item_cd, order_ym = x.order_ym.ToString("yyyy/MM") })
                                    .Select(x => new SalePlanByMonthItemResponse 
                                        {
                                            order_qty = x.Select(y => y.order_qty).FirstOrDefault() != null ?  x.Sum(y => y.order_qty):null,
                                            order_ym = x.Key.order_ym.ToDate().ToDateString(),
                                    })
                                    .ToList();

                var date = start_month;
                while (date.Year < last_month.Year || (date.Year == last_month.Year && date.Month <= last_month.Month))
                {
                    if (!item.item_sale_plans.Where(x => x.order_ym.Year == date.Year && x.order_ym.Month == date.Month).Any())
                    {
                        plan_month.Add(new SalePlanByMonthItemResponse() { order_ym = date.ToDateString(), order_qty = null, });
                    }
                    date = date.AddMonths(1);
                }
                sale_item.data = plan_month.OrderBy(x => x.order_ym).ToList();
                data_res.Add(sale_item);
            }

            return data_res;
        }

        public async Task<IList<SalePlanByBrandResponse>> GetByBranch(SalePlanByBranchRequest request)
        {
            var month_req = request.order_ym_fomated.Value;
            // get item
            var items = _itemRepository
                               .GetQuery()
                               .ExcludeSoftDeleted()
                               .Include(x => x.item_sale_plans
                                            .Where(y => y.del_flg == false)
                                            .Where(y => y.order_ym.Year == month_req.Year && y.order_ym.Month == month_req.Month)
                                        )
                               .ThenInclude(x => x.company)
                               .Where(x => string.IsNullOrEmpty(request.item_cd) || x.code.ToLower().Contains(request.item_cd.ToLower().Trim()))
                               .Where(x => string.IsNullOrEmpty(request.item_type) || x.item_type.ToLower() == request.item_type.ToLower())
                               .Where(x => string.IsNullOrEmpty(request.item_class_cd2) || x.class_cd2.ToLower() == request.item_class_cd2.ToLower())
                               .Where(x => string.IsNullOrEmpty(request.item_name1) || x.name1.ToLower().Contains(request.item_name1.ToLower().Trim()))
                               .OrderBy(x => x.order)
                               .ThenBy(x => x.code)
                               .ThenBy(x=> x.name1)
                               .ToList();
            // get all brach active
            var branchs = _companyRepository.GetQuery().ExcludeSoftDeleted().Where(x => x.branch_f == 1).ToList();

            // get classification
            var classifications = _classificationRepository.GetQuery().ExcludeSoftDeleted();

            // get item_edi
            var item_edis = _itemEdiRepository.GetQuery().ExcludeSoftDeleted();

            var data_res = new List<SalePlanByBrandResponse>();
            foreach (var item in items)
            {
                var class_name2 = classifications.Where(x => x.code2 == item.class_cd2).Select(x => x.name1).FirstOrDefault();
                var sale_plan_branch = new SalePlanByBrandResponse()
                {
                    item_type = item.item_type,
                    item_cd = item.code,
                    item_name1 = item.name1,
                    item_name2 = item.name2,
                    item_class_name2 = class_name2,
                    standad_unit_qty = item.unit_conv_rate,
                    order_unit = item.std_unit,
                    order_ym = request.order_ym,
                };

                // ignore company is deleted
                var item_branchs = item.item_sale_plans
                                    .Where(x => x.company.del_flg == false)
                                    .Select(x => new SalePlanByBrandItemResponse() 
                                    {
                                        company_cd = x.company_cd, 
                                        company_name = x.company.company_name1, 
                                        order_qty = x.order_qty 
                                    })
                                    .OrderBy(x => x.company_name)
                                    .ToList();

                // get branch not exist in sale_plan
                var branch_not_exist = branchs.Where(x => !item_branchs.Select(y => y.company_cd).Contains(x.code))
                                        .Select(x => new SalePlanByBrandItemResponse() 
                                        {
                                            company_cd = x.code, 
                                            company_name = x.company_name1,
                                            order_qty = null
                                        })
                                        .OrderBy(x=>x.company_name)
                                        .ToList();

                item_branchs.AddRange(branch_not_exist);
                sale_plan_branch.data = item_branchs.OrderBy(x => x.company_name).ToList();
                data_res.Add(sale_plan_branch);
            }
            
            return data_res;
        }

        public async Task<int> CreateUpdate(List<SalePlanRequest> request)
        {
            var user = _userRepository.GetQuery().ExcludeSoftDeleted().Where(x => x.id == _serviceContext._userId).FirstOrDefault();
            if (user.branch_cd != request.FirstOrDefault().company_cd)
            {
                return 0;
            }
            int count = 0;
            var items = _itemRepository.GetQuery().ExcludeSoftDeleted().Include(x => x.item_sale_plans).ToList();
            var sale_plan_old = _salePlanRepository.GetQuery().ExcludeSoftDeleted()
                                    .Where(x => request.Select(y => y.item_cd).Contains(x.item_cd))
                                    .Where(x => request.Select(y => y.company_cd).Contains(x.company_cd))
                                    .ToList();

            foreach (var request_item in request)
            {
                foreach (var request_item_month in request_item.data)
                {
                    var sale_plan_item = sale_plan_old
                            .Where(x => x.item_cd == request_item.item_cd && x.company_cd == request_item.company_cd)
                            .Where(x => x.order_ym.ToString("yyyy/MM") == request_item_month.order_ym_fomated.Value.ToString("yyyy/MM"))
                            .FirstOrDefault();


                    if (sale_plan_item == null && request_item_month.order_qty != null)
                    {
                        sale_plan_item = new SalePlan();
                        var item = items.FirstOrDefault(x => x.code == request_item.item_cd);
                        sale_plan_item.company_cd = request_item.company_cd;
                        sale_plan_item.item_type = request_item.item_type;
                        sale_plan_item.item_cd = request_item.item_cd;
                        sale_plan_item.item_edi_cd = request_item.item_edi_cd;
                        sale_plan_item.item_name1 = request_item.item_name1;
                        sale_plan_item.item_name2 = request_item.item_name2;
                        sale_plan_item.order_unit = request_item.order_unit;
                        sale_plan_item.standad_unit_qty = request_item.standad_unit_qty;
                        sale_plan_item.order_ym = (DateTime)request_item_month.order_ym_fomated;
                        sale_plan_item.order_qty = request_item_month.order_qty;

                        var saleplans = _mapper.Map<SalePlan>(sale_plan_item);
                        await _salePlanRepository.AddEntityAsync(saleplans);
                        count++;
                    }  
                    else if(sale_plan_item != null)
                    {
                        sale_plan_item.order_qty = request_item_month.order_qty;
                        await _salePlanRepository.UpdateEntityAsync(sale_plan_item);
                        
                        count++;
                    }
                }
            }
            await _unitOfWork.SaveChangesAsync();

            return count;
        }
    }
}
