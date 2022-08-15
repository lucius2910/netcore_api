using AutoMapper;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Application.Core.Contracts;
using Domain.Entities;
using Application.Sale.Contracts;
using Application.Inventories.Contracts;
using Framework.Core.Extensions;

namespace WebAPI.ProfileMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class CoreProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public CoreProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<IPagedList<User>, PagedList<UserResponse>>();
            CreateMap<User, UserResponse>();

            CreateMap<RoleRequest, Role>()
                .ForMember(dest => dest.permissions, opt => opt.Ignore());
            CreateMap<IPagedList<Role>, PagedList<RoleResponse>>();
            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.permissions, opt => opt.MapFrom(src => src.permissions.Select(x => x.function.code).ToList()));

            CreateMap<FunctionRequest, Function>();
            CreateMap<IPagedList<Function>, PagedList<FunctionResponse>>();
            CreateMap<Function, FunctionResponse>()
                .ForMember(dest => dest.parent, opt => opt.MapFrom(src => src.parent.code));

            CreateMap<ResourceRequest, Resource>();
            CreateMap<IPagedList<Resource>, PagedList<ResourceResponse>>();
            CreateMap<Resource, ResourceResponse>();
            
            CreateMap<MasterCodeRequest, MasterCode>();
            CreateMap<IPagedList<MasterCode>, PagedList<MasterCodeRespose>>();
            CreateMap<MasterCode, MasterCodeRespose>();

            CreateMap<MachineRequest, Machine>();
            CreateMap<IPagedList<Machine>, PagedList<MachineResponse>>();
            CreateMap<Machine, MachineResponse>();

            CreateMap<CategoryRequest, Category>();
            CreateMap<IPagedList<Category>, PagedList<CategoryResponse>>();
            CreateMap<Category, CategoryResponse>();

            CreateMap<WarehouseRequest, Warehouses>();
            CreateMap<Warehouses, WarehouseResponse>();
            CreateMap<IPagedList<Warehouses>, PagedList<WarehouseResponse>>();

            CreateMap<IPagedList<Item>, PagedList<ItemResponse>>();
            CreateMap<ItemRequest, Item>();
            CreateMap<Item, ItemResponse>();

            CreateMap<IPagedList<Company>, PagedList<ClassificationResponse>>();
            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponse>();

            CreateMap<IPagedList<SalePlan>, PagedList<SalePlanResponse>>();
            CreateMap<SalePlanRequest, SalePlan>();
            CreateMap<SalePlan, SalePlanResponse>();

            //CreateMap<ItemPriceMasterRequest, Item>()
            //    .ForMember(dest => dest.item_buy_prices, opt => opt.Ignore())
            //    .ForMember(dest => dest.item_sale_prices, opt => opt.Ignore());
            CreateMap<IPagedList<Item>, PagedList<ItemPriceMasterRespones>>();
            CreateMap<ItemBuyPrices, ItemBuyPriceRespones>();
            CreateMap<ItemPrice, ItemPriceResponse>();
            CreateMap<IPagedList<ItemPrice>, PagedList<ItemPriceResponse>>();
            CreateMap<ItemSalePrices, ItemSalePriceRespones>();

            CreateMap<ItemSalePriceRequest, ItemSalePrices>();
            CreateMap<ItemBuyPriceRequest, ItemBuyPrices>();
            CreateMap<ItemPriceRequest, ItemPrice>();

            //CreateMap<Item, ItemPriceMasterRespones>()
            //    .ForMember(dest => dest.buy_prices, opt => opt.MapFrom(src => src.item_buy_prices.ToList()))
            //    .ForMember(dest => dest.sale_prices, opt => opt.MapFrom(src => src.item_sale_prices.ToList()));

            CreateMap<CalendarRequest, Calendar>();
            CreateMap<Calendar, CalendarResponse>();
            CreateMap<IPagedList<Calendar>, PagedList<CalendarResponse>>();

            CreateMap<ClassificationRequest, Classification>();
            CreateMap<Classification, ClassificationResponse>();
            CreateMap<IPagedList<Classification>, PagedList<ClassificationResponse>>();

            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponse>();
            CreateMap<IPagedList<Company>, PagedList<CompanyResponse>>();
            
            CreateMap<IPagedList<ReceiveOrder>, PagedList<ReceiveOrderResponse>>();

        }
    }
}
