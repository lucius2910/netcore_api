using AutoMapper;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Framework.Core.Collections;
using Domain.Entities;
using Application.Common.Abstractions;

namespace Application.Core.Services
{
    public class ItemPriceMasterService : BaseService, IItemPriceMasterService
    {
        private readonly IRepository<ItemPrice> itemPriceRepository;
        private readonly IRepository<Item> itemRepository;
        private readonly IRepository<ItemBuyPrices> itemBuyPricesRepository; 
        private readonly IRepository<ItemSalePrices> itemSalePricesRepository;
        public ItemPriceMasterService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            itemRepository = _unitOfWork.GetRepository<Item>();
            itemPriceRepository = _unitOfWork.GetRepository<ItemPrice>();
            itemBuyPricesRepository = _unitOfWork.GetRepository<ItemBuyPrices>();
            itemSalePricesRepository = _unitOfWork.GetRepository<ItemSalePrices>();
        }

        public async Task<ItemPriceMasterRespones> GetByCode(string code)
        {
            var itempricemaster = itemRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  //.Include(x => x.item_sale_prices.Where(y => y.del_flg == false))
                                  //.Include(x => x.item_buy_prices.Where(y => y.del_flg == false))
                                  .FirstOrDefault(x => x.code == code);

            var data = _mapper.Map<ItemPriceMasterRespones>(itempricemaster);
            return data;
        }

        public async Task<IPagedList<ItemPriceResponse>> GetPaged(ItemPricePagedRequest request)
        {
            var data = itemPriceRepository
                              .GetQuery()
                              .ExcludeSoftDeleted()
                              .Include(x => x.item)
                              .SortBy(request.sort)
                              .Where(y => y.item.del_flg == false)
                              .Where(x => !string.IsNullOrEmpty(request.item_type) ? (x.item.item_type.ToLower().Contains(request.item_type.ToLower())) : true)
                              .Where(x => !string.IsNullOrEmpty(request.item_code) ? (x.item.code.ToLower().Contains(request.item_code.ToLower())) : true)
                              .Where(x => !string.IsNullOrEmpty(request.item_name1) ? (x.item.name1.ToLower().Contains(request.item_name1.ToLower())) : true)
                              .Where(x => !string.IsNullOrEmpty(request.item_name2) ? (x.item.name2.ToLower().Contains(request.item_name2.ToLower())) : true)
                              .ToPagedList(request.page, request.size);
            var dataMapping = _mapper.Map<PagedList<ItemPriceResponse>>(data);
            
            return dataMapping;
        }
        public async Task<int> CreateUpdate(ItemPriceMasterRequest request)
        {
            string itemcd ="";
            foreach (var item in request.buy_prices)
            {
                if (item.customer != null)
                {
                    var itembuyprices = _mapper.Map<ItemBuyPrices>(item);
                    await itemBuyPricesRepository.AddEntityAsync(itembuyprices);
                }
                else
                {
                    request.item_price.item_cd = item.item_cd;
                    request.item_price.buy_price = item.price;
                    request.item_price.meterial_price = item.meterial_price;
                    request.item_price.process_price = item.process_price;
                    request.item_price.auxiliary_price = item.auxiliary_price;
                    itemcd = item.item_cd;
                }  
            }

            foreach (var item in request.sale_prices)
            {
                if (item.customer != null)
                {
                    var itemsaleprices = _mapper.Map<ItemSalePrices>(item);
                    await itemSalePricesRepository.AddEntityAsync(itemsaleprices);
                }
                else
                {
                    request.item_price.sale_price = item.price;
                    request.item_price.unit = item.unit;
                    request.item_price.effective_startdate = item.effective_startdate;
                    request.item_price.effective_enddate = item.effective_enddate;
                    request.item_price.currency = item.currency;
                    request.item_price.customer = item.customer;
                    request.item_price.min_qty = item.min_qty;
                }
            }

            var itemprice = itemPriceRepository.GetQuery()
                                              .ExcludeSoftDeleted()
                                              .FirstOrDefault(x => x.item_cd == itemcd);
            if (itemprice != null)
            {
                if(itemprice.effective_enddate > request.item_price.effective_enddate)
                {
                    request.item_price.effective_enddate = itemprice.effective_enddate;
                }  
                else if(itemprice.effective_startdate > request.item_price.effective_enddate)
                {
                    request.item_price.effective_startdate = itemprice.effective_startdate;
                }    
                _mapper.Map(request.item_price, itemprice);
                await itemPriceRepository.UpdateEntityAsync(itemprice);
            }    
            else
            {
                var itemprices = _mapper.Map<ItemPrice>(request.item_price);
                await itemPriceRepository.AddEntityAsync(itemprices);
            }    
            var count = await _unitOfWork.SaveChangesAsync();

            return count;
        }
        public async Task<int> Delete(Guid id)
        {
            var entity = itemSalePricesRepository.GetQuery().FindActiveById(id).FirstOrDefault();

            await itemSalePricesRepository.DeleteEntityAsync(entity);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
    }
}
