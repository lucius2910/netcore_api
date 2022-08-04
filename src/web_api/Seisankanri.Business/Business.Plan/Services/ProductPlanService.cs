using AutoMapper;
using Business.Core.Extensions;
using Business.Plan.Contracts;
using Business.Plan.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Seisankanri.Model.Entities;
using System.Transactions;

namespace Business.Plan.Services
{
    public class ProductPlanService : BaseServices, IProductPlanService
    {
        private readonly IRepository<Classification> classificationRepository;
        private readonly IRepository<ProductPlanDay> productPlanDayRepository;
        private readonly IRepository<ProductPlanMonth> productPlanMonthRepository;
        private readonly IRepository<SystemSettings> systemSettingsRepository;
        private readonly IRepository<SalePlan> salePlanRepository;
        private readonly IRepository<Item> itemRepository;
        private readonly IRepository<ItemEdi> itemEdiRepository;
        private readonly IRepository<InventoryTotal> inventoryTotalRepository;
        private readonly IRepository<Machine> machineRepository;
        public ProductPlanService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            classificationRepository = _unitOfWork.GetRepository<Classification>();
            productPlanDayRepository = _unitOfWork.GetRepository<ProductPlanDay>();
            productPlanMonthRepository = _unitOfWork.GetRepository<ProductPlanMonth>();
            systemSettingsRepository = _unitOfWork.GetRepository<SystemSettings>();
            salePlanRepository = _unitOfWork.GetRepository<SalePlan>();
            itemRepository = _unitOfWork.GetRepository<Item>();
            itemEdiRepository = _unitOfWork.GetRepository<ItemEdi>();
            inventoryTotalRepository = _unitOfWork.GetRepository<InventoryTotal>();
            machineRepository = _unitOfWork.GetRepository<Machine>();
        }

        public async Task<int> DeleteByItem(ProductPlanDeleteByItemRequest request)
        {

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    int count = 0;
                    var product_plan_days = productPlanDayRepository
                                                                    .GetQuery()
                                                                    .ExcludeSoftDeleted()
                                                                    .Where(x => x.plan_day.Month == request.plan_month_fomated.Value.Month && x.plan_day.Year == request.plan_month_fomated.Value.Year)
                                                                    .Where(x => string.IsNullOrEmpty(request.item_cd) || x.item_cd.ToLower() == request.item_cd.ToLower())
                                                                    .ToList();
                    if (product_plan_days.Count == 0)
                        return count = -1;

                    foreach (var itemplanday in product_plan_days)
                    {
                        await productPlanDayRepository.DeleteEntityAsync(itemplanday);
                    }

                    var product_plan_months = productPlanMonthRepository
                                                                    .GetQuery()
                                                                    .ExcludeSoftDeleted()
                                                                    .Where(x => x.plan_month.Month == request.plan_month_fomated.Value.Month && x.plan_month.Year == request.plan_month_fomated.Value.Year)
                                                                    .Where(x => string.IsNullOrEmpty(request.item_cd) || x.item_cd.ToLower() == request.item_cd.ToLower())
                                                                    .ToList();

                    foreach (var itemplanmonth in product_plan_months)
                    {
                        await productPlanMonthRepository.DeleteEntityAsync(itemplanmonth);
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

        public async Task<IList<ProductPlanByMonthResponse>> GetByMonth(ProductPlanMonthRequest request)
        {
            //get product plan
            var product_plan_months = productPlanMonthRepository
                                                    .GetQuery()
                                                    .ExcludeSoftDeleted()
                                                    .Where(x => x.plan_month.Month == request.plan_month_fomated.Value.Month && x.plan_month.Year == request.plan_month_fomated.Value.Year)
                                                    .Where(x => string.IsNullOrEmpty(request.item_cd) || x.item_cd.ToLower().Contains(request.item_cd.ToLower().Trim()))
                                                    .Where(x => string.IsNullOrEmpty(request.item_name) || x.item_name1.ToLower().Contains(request.item_name.ToLower().Trim()))
                                                    .Include(x => x.item)
                                                    .OrderBy(x => x.item.order)
                                                    .ToList();

            //get item
            var items = itemRepository.GetQuery().ExcludeSoftDeleted()
                                      .Where(x => string.IsNullOrEmpty(request.item_type) || x.item_type.ToLower() == request.item_type.ToLower())
                                      .ToList();

            //get item edi
            var item_edis = itemEdiRepository.GetQuery().ExcludeSoftDeleted().ToList();

            var result_product_plans = product_plan_months
                                    .Where(x => items.Select(y => y.code).Contains(x.item_cd))
                                    .Select(x => new ProductPlanByMonthResponse
                                    {
                                        item_cd = x.item_cd,
                                        item_name1 = x.item_name1,
                                        machine_name = x.machine_names,
                                        pre_month_inventory = x.pre_month_inventory,
                                        to_be_sold = x.to_be_sold,
                                        production_schedule = x.production_schedule,
                                        production_results = x.production_results,
                                        shipment_results = x.shipment_results,
                                        estimate_inventory = x.estimate_inventory,
                                        item_edi_cd = item_edis.Where(x => x.item_cd == x.item_cd && x.company_cd == x.company_cd).FirstOrDefault()?.edi_cd
                                    }).ToList();
            return result_product_plans;
        }

        public async Task<IList<ProductPlanByDayResponse>> GetByDay(ProductPlanDaySearchRequest request)
        {
            List<ProductPlanByDayResponse> productPlanByDays = new List<ProductPlanByDayResponse>();
            productPlanByDays = GetDataItemPlan(request);

            return productPlanByDays;
        }

        public async Task<int> UpdateByMonth(List<ProductPlanUpdateRequest> requests)
        {
            var count = 0;
            var productplandays = productPlanDayRepository.GetQuery().ExcludeSoftDeleted().ToList();
            var product_plan_months = productPlanMonthRepository.GetQuery().ExcludeSoftDeleted().ToList();
            var machines = machineRepository.GetQuery().ExcludeSoftDeleted().ToList();
            var items = itemRepository.GetQuery().ExcludeSoftDeleted().ToList();

            foreach (var item in requests)
            {
                var itemcd = items.Where(x => x.code == item.item_cd).FirstOrDefault();

                //check item null
                if (items.Where(x => x.code == item.item_cd).FirstOrDefault() == null)
                    break;
                //get product month
                var list_product_plan_month = product_plan_months.Where(x => x.item_cd.Equals(item.item_cd)).ToList();

                foreach (var itemplanday in item.data)
                {
                    var productplanday = productplandays.Where(x => x.plan_day.Day == itemplanday.plan_day_fomated.Value.Day && x.plan_day.Month == itemplanday.plan_day_fomated.Value.Month && x.plan_day.Year == itemplanday.plan_day_fomated.Value.Year)
                                                             .Where(x => string.IsNullOrEmpty(item.item_cd) || x.item_cd.ToLower() == item.item_cd.ToLower())
                                                             .FirstOrDefault();
                    if (productplanday == null && itemplanday.machine_cd != null && itemplanday.to_be_sold != null && itemplanday.production_schedule != null)
                    {
                        var entity = _mapper.Map<ProductPlanDay>(itemplanday);
                        entity.item_cd = item.item_cd;
                        entity.item_type = itemcd.item_type;
                        entity.item_name1 = itemcd.name1;
                        entity.item_name2 = itemcd.name2;
                        await productPlanDayRepository.AddEntityAsync(entity);
                    }
                    else if (productplanday != null)
                    {
                        _mapper.Map(itemplanday, productplanday);
                        await productPlanDayRepository.UpdateEntityAsync(productplanday);
                    }
                }

                //get product day
                var machine_cds = productplandays.Where(x => x.item_cd == item.item_cd).Select(x => new { x.machine_cd, x.plan_day });
                var list_productplanday = productplandays.Where(x => x.item_cd == item.item_cd && item.data.Any(y => x.plan_day.Year.Equals(y.plan_day_fomated.Value.Year) && x.plan_day.Month.Equals(y.plan_day_fomated.Value.Month)));

                foreach (var month in list_product_plan_month)
                {
                    var list_machine_cd = machine_cds.Where(x => x.plan_day.Month == month.plan_month.Month && x.plan_day.Year == month.plan_month.Year).ToList();
                    month.machine_names = string.Join(",", machines.Where(sublist => list_machine_cd.Any(it => sublist.code == it.machine_cd)).Select(x => x.name).ToList());
                    month.to_be_sold = list_productplanday.Sum(x => x.to_be_sold);
                    month.production_schedule = list_productplanday.Sum(x => x.production_schedule);
                    await productPlanMonthRepository.UpdateEntityAsync(month);
                }
                var planMonth = list_product_plan_month.Where(x => x.item_cd == item.item_cd).OrderBy(x => x.plan_month).ToList();

                for (int i = 0; i < planMonth.Count(); i++)
                {
                    var currentPlanMonth = planMonth[i];
                    int month = 1;
                    ProductPlanMonth? nextPlanMonth = new ProductPlanMonth();
                    //get plan month (T+1)   
                    if (currentPlanMonth.plan_month.Month != 12)
                    {
                        nextPlanMonth = planMonth.Where(x => x.plan_month.Month == currentPlanMonth.plan_month.AddMonths(month).Month && x.plan_month.Year == currentPlanMonth.plan_month.Year).FirstOrDefault();
                    }
                    else
                    {
                        nextPlanMonth = planMonth.Where(x => x.plan_month.Month == currentPlanMonth.plan_month.AddMonths(month).Month && x.plan_month.Year == currentPlanMonth.plan_month.AddYears(1).Year).FirstOrDefault();
                    }
                    var plan_month = planMonth.Where(x => x.plan_month.Month == currentPlanMonth.plan_month.AddMonths(month - 1).Month && x.plan_month.Year == currentPlanMonth.plan_month.Year)
                           .Select(x => new { pre_month_inventory = x.pre_month_inventory, to_be_sold = x.to_be_sold, production_schedule = x.production_schedule }).FirstOrDefault();

                    if (nextPlanMonth == null)
                    {
                        var productPlanMonth = new ProductPlanMonth()
                        {
                            item_cd = item.item_cd,
                            plan_month = currentPlanMonth.plan_month.AddMonths(month),
                            item_type = itemcd.item_type,
                            item_name1 = itemcd.name1,
                            item_name2 = itemcd.name2,
                            pre_month_inventory = plan_month != null ? plan_month.pre_month_inventory.ToInt() + plan_month.production_schedule.ToInt() - plan_month.to_be_sold.ToInt() : null,
                            to_be_sold = null,
                            production_schedule = null
                        };
                        await productPlanMonthRepository.AddEntityAsync(productPlanMonth);
                    }
                    else
                    {
                        nextPlanMonth.pre_month_inventory = plan_month != null ? plan_month.pre_month_inventory.ToInt() + plan_month.production_schedule.ToInt() - plan_month.to_be_sold.ToInt() : null;
                        await productPlanMonthRepository.UpdateEntityAsync(nextPlanMonth);
                    }
                }
            }

            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> InitDataByMonth(InitDataByMonthRequest request)
        {

            try
            {
                _unitOfWork.BeginTransaction();
                var count = 0;

                // Get end date of month 
                DateTime enddayofmonth = request.order_ym_fomated.Value;
                enddayofmonth = enddayofmonth.AddMonths(1);
                enddayofmonth = enddayofmonth.AddDays(-(enddayofmonth.Day));

                // Get first date of month
                DateTime firstdayofmonth = request.order_ym_fomated.Value;
                firstdayofmonth = firstdayofmonth.AddDays(-(firstdayofmonth.Day) + 1);

                int dayofmonth = enddayofmonth.Day;

                if (CheckDataExit(request.order_ym_fomated.Value))
                    return 0;

                var saleplans = salePlanRepository
                    .GetQuery(
                    ).ExcludeSoftDeleted()
                    .Where(x => x.order_ym.Month == request.order_ym_fomated.Value.Month 
                    && x.order_ym.Year == request.order_ym_fomated.Value.Year)
                    .ToList();

                if (saleplans.Count > 1)
                {
                    saleplans = saleplans.DistinctBy(x => x.item_cd).ToList();
                }

                //get data inventory total
                var inventory_totals = inventoryTotalRepository
                    .GetQuery()
                    .ExcludeSoftDeleted()
                    .GroupBy(x => new { x.item_cd })
                    .Select(x => new { item_cd = x.Key.item_cd, stock_qty = x.Sum(y => y.stock_qty) })
                    .ToList();

                var totals = salePlanRepository
                    .GetQuery()
                    .ExcludeSoftDeleted()
                    .Where(x => x.order_ym.Month == request.order_ym_fomated.Value.Month 
                    && x.order_ym.Year == request.order_ym_fomated.Value.Year)
                    .GroupBy(x => new { x.item_cd })
                    .Select(x => new { item_cd = x.Key.item_cd, order_qty = x.Sum(y => y.order_qty) })
                    .ToList();

                var saleplandaymonths = from sale in saleplans
                                        join total in totals on sale.item_cd equals total.item_cd
                                        select new
                                        {
                                            item_name1 = sale.item_name1,
                                            item_cd = sale.item_cd,
                                            item_type = sale.item_type,
                                            item_name2 = sale.item_name2,
                                            to_be_sold = total.order_qty
                                        };

                DateTime last_month = request.order_ym_fomated.Value.AddMonths(-1);
                var product_plan_months = productPlanMonthRepository.GetQuery()
                                                                    .ExcludeSoftDeleted();

                foreach (var item in saleplandaymonths)
                {
                    for (int i = 0; i < dayofmonth; i++)
                    {
                        InitDataByDayResponse intdatabyday = new InitDataByDayResponse();
                        intdatabyday.plan_day = firstdayofmonth.AddDays(i).ToString();
                        intdatabyday.item_name1 = item.item_name1;
                        intdatabyday.item_type = item.item_type;
                        intdatabyday.item_cd = item.item_cd;
                        intdatabyday.item_name2 = item.item_name2;

                        var insertplanday = _mapper.Map<ProductPlanDay>(intdatabyday);
                        await productPlanDayRepository.AddEntityAsync(insertplanday);
                    }

                    InitDataMonthResponse intdatabymonth = new InitDataMonthResponse();
                    intdatabymonth.plan_month = request.order_ym;
                    intdatabymonth.item_cd = item.item_cd;
                    intdatabymonth.item_type = item.item_type;
                    intdatabymonth.item_name1 = item.item_name1;
                    intdatabymonth.item_name2 = item.item_name2;
                    intdatabymonth.to_be_sold = totals.Where(x => x.item_cd == item.item_cd).Select(x => x.order_qty).FirstOrDefault();

                    var product_plan_month = product_plan_months.Where(x => x.item_cd == item.item_cd && x.plan_month.Month == last_month.Month && x.plan_month.Year == last_month.Year).FirstOrDefault();
                    if (product_plan_month != null)
                    {
                        intdatabymonth.pre_month_inventory = (product_plan_month.pre_month_inventory ?? 0) + (product_plan_month.production_schedule ?? 0) - (product_plan_month.to_be_sold ?? 0);
                    }
                    else
                    {
                        intdatabymonth.pre_month_inventory = inventory_totals.Where(x => x.item_cd == item.item_cd).Select(x => x.stock_qty).FirstOrDefault();
                    }
                    var insertplanmonths = _mapper.Map<ProductPlanMonth>(intdatabymonth);
                    await productPlanMonthRepository.AddEntityAsync(insertplanmonths);
                }
                count = await _unitOfWork.SaveChangesAsync();

                _unitOfWork.Commit();

                return count;
            }
            catch (Exception ex)
            {
                _unitOfWork.RoleBack();
                throw ex;
            }
        }


        public List<ProductPlanByDayResponse> GetDataItemPlan(ProductPlanDaySearchRequest request)
        {
            var rkd_cd = classificationRepository.GetQuery().ExcludeSoftDeleted().Where(x=> x.name1.Equals("RDK") && x.code1=="102").FirstOrDefault().code2;
            var product_plan_month = productPlanMonthRepository
                .GetQuery()
                .ExcludeSoftDeleted()
                .Where(x => x.plan_month.Month == request.plan_month_formated.Value.Month && x.plan_month.Year == request.plan_month_formated.Value.Year)
                .ToList();

            var plan_by_month = productPlanDayRepository
                .GetQuery()
                .ExcludeSoftDeleted()
                .Where(x => x.plan_day.Month == request.plan_month_formated.Value.Month && x.plan_day.Year == request.plan_month_formated.Value.Year)
                .Where(x => string.IsNullOrEmpty(request.item_cd) || x.item_cd == request.item_cd)
                .Where(x => string.IsNullOrEmpty(request.item_name) || x.item_name1.Contains(request.item_name.Trim()))
                .Include(x => x.item)
                .Where(x => string.IsNullOrEmpty(request.item_class_cd2) || x.item.class_cd2 == request.item_class_cd2)
                .Where(x => x.item.item_type.Equals(rkd_cd))
                .OrderBy( x => x.item.order)
                .ToList();

            if (plan_by_month == null || plan_by_month.Count == 0)
                return new List<ProductPlanByDayResponse>();

            // distinct item
            var items = plan_by_month.Select(x => new
            {
                item_cd = x.item_cd,
                item_name1 = x.item_name1
            }).Distinct().ToList();

            //loop => lays data
            var item_plans = new List<ProductPlanByDayResponse>();
            if (items == null || items.Count() == 0)
                return item_plans;

            // Get total sale plan by item
            var sale_plan_item = salePlanRepository
                .GetQuery()
                .ExcludeSoftDeleted()
                .Where(x => x.order_ym.Month == request.plan_month_formated.Value.Month && x.order_ym.Year == request.plan_month_formated.Value.Year)
                .GroupBy(x => new { x.item_cd })
                .Select(x => new { item_cd = x.Key.item_cd, order_qty = x.Sum(y => y.order_qty) })
                .ToList();

            foreach (var item in items)
            {
                var item_product_plan = new ProductPlanByDayResponse
                {
                    item_cd = item.item_cd,
                    item_name1 = item.item_name1,
                };

                item_product_plan.data = new List<ProductPlanByDayItemResponse>();

                // neu co ngay thi copy du lieu sang
                var item_plan_data = plan_by_month
                    .Where(x => x.item_cd == item.item_cd)
                    .Select(x => new ProductPlanByDayItemResponse
                    {
                        machine_cd = x.machine_cd,
                        plan_day = x.plan_day.ToDateString(),
                        to_be_sold = x.to_be_sold,
                        production_schedule = x.production_schedule,
                        production_results = x.production_results,
                        estimate_inventory = x.estimate_inventory,
                    })
                    .ToList();
                item_product_plan.data.AddRange(item_plan_data);

                // chua co thi tao moi
                foreach (var date_item in DateTimeExtensions.GetDates(request.plan_month_formated.Value.Year, request.plan_month_formated.Value.Month))
                {
                    if (!item_product_plan.data.Any(x => x.plan_day == date_item.ToDateString()))
                    {
                        item_product_plan.data.Add(new ProductPlanByDayItemResponse()
                        {
                            machine_cd = null,
                            plan_day = date_item.ToDateString(),
                            to_be_sold = null,
                            production_schedule = null,
                            production_results = null,
                            estimate_inventory = null,
                        });
                    }
                }

                item_product_plan.estimate_sold = item_product_plan.data.Sum(x => x.to_be_sold);
                item_product_plan.to_be_sold = sale_plan_item
                    .Where(x => x.item_cd == item_product_plan.item_cd)
                    .Select(x => x.order_qty)
                    .FirstOrDefault();

                item_product_plan.pre_month_inventory = product_plan_month
                    .Where(x => x.item_cd == item.item_cd)
                    .Select(x => x.pre_month_inventory)
                    .FirstOrDefault();

                item_product_plan.data = item_product_plan.data.OrderBy(x => x.plan_day).ToList();
                item_plans.Add(item_product_plan);

                foreach (var product_plan_day in item_product_plan.data)
                {
                    if (product_plan_day.plan_day.ToDate().Value.Day == 1)
                    {
                        int estInventory = item_product_plan.pre_month_inventory.ToInt() + product_plan_day.production_schedule.ToInt() - product_plan_day.to_be_sold.ToInt();
                        product_plan_day.estimate_inventory = estInventory;
                    }
                    else
                    {
                        var estInventory = item_product_plan.data
                            .Where(x => x.plan_day.ToDate() == DateTime.Parse(product_plan_day.plan_day)
                            .Subtract(TimeSpan.FromDays(1)).ToString().ToDate())
                            .Select(x => x.estimate_inventory)
                            .FirstOrDefault();
                        product_plan_day.estimate_inventory = estInventory != null ? estInventory.ToInt() + product_plan_day.production_schedule.ToInt() - product_plan_day.to_be_sold.ToInt() : null;
                    }
                }
            }

            //
            return item_plans.OrderBy(x => x.item_cd ).ToList();
        }

        public bool CheckDataExit(DateTime plan_month)
        {
            var checkday = productPlanDayRepository
                .GetQuery()
                .ExcludeSoftDeleted()        
                .FirstOrDefault(x => x.plan_day.Month == plan_month.Month && x.plan_day.Year == plan_month.Year);

            var checkmonth = productPlanMonthRepository
                .GetQuery()
                .ExcludeSoftDeleted()
                .FirstOrDefault(x => x.plan_month.Month == plan_month.Month && x.plan_month.Year == plan_month.Year);

            return checkday != null && checkmonth != null;
        }
    }
}
