using AutoMapper;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Business.Core.Contracts;
using Seisankanri.Model.Entities;
using Business.Inventories.Contracts;
using Framework.Core.Extensions;
using Business.SaleOrder.Contracts;
using Business.Plan.Contracts;

namespace Seisankanri.Api.ProfileMapper
{
    public class CoreProfile : Profile
    {
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

            CreateMap<InventoryRequest, Inventory>();
            CreateMap<IPagedList<Inventory>, PagedList<InventoryResponse>>();
            CreateMap<Inventory, InventoryResponse>();

            CreateMap<MachineRequest, Machine>();
            CreateMap<IPagedList<Machine>, PagedList<MachineResponse>>();
            CreateMap<Machine, MachineResponse>()
                .ForMember(dest => dest.effective_date, opt => opt.MapFrom(src => src.effective_date.ToDateString()));

            CreateMap<CategoryRequest, Category>();
            CreateMap<IPagedList<Category>, PagedList<CategoryResponse>>();
            CreateMap<Category, CategoryResponse>();

            CreateMap<WarehouseRequest, Warehouses>();
            CreateMap<Warehouses, WarehouseResponse>();
            CreateMap<IPagedList<Warehouses>, PagedList<WarehouseResponse>>();

            CreateMap<IPagedList<Item>, PagedList<ItemResponse>>();
            CreateMap<ItemRequest, Item>();
            CreateMap<Item, ItemResponse>();
            CreateMap<IPagedList<Item>, PagedList<GetItemByTypeResponse>>();
            CreateMap<Item, GetItemByTypeResponse>();

            CreateMap<ItemEdi, ItemEdiResponse>();
            CreateMap<ItemEdiCreateRequest, ItemEdi>();
            CreateMap<ItemEdiUpdateRequest, ItemEdi>();
            CreateMap<IPagedList<ItemEdi>, PagedList<ItemEdiResponse>>();

            CreateMap<IPagedList<Company>, PagedList<ClassificationResponse>>();
            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponse>();

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
            CreateMap<Calendar, CalendarResponse>()
                .ForMember(dest => dest.calendar_date, opt => opt.MapFrom(src => src.calendar_date.ToDateString()));

            CreateMap<ClassificationRequest, Classification>();
            CreateMap<Classification, ClassificationResponse>();
            CreateMap<IPagedList<Classification>, PagedList<ClassificationResponse>>();

            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponse>();
            CreateMap<IPagedList<Company>, PagedList<CompanyResponse>>();
            CreateMap<ReceiveOrderDtRequest, ReceiveOrderDt>();
            CreateMap<ReceiveOrderDt, ReceiveOrderDtResoponse>()
                .ForMember(dest => dest.r_order_dl, opt => opt.MapFrom(src => src.r_order_dl.ToDateString()));
            CreateMap<IPagedList<ReceiveOrderDt>, PagedList<ReceiveOrderDtResoponse>>();
            CreateMap<ReceiveOrder, GetDataResponse>()
                .ForMember(dest => dest.order_company_name1, src => src.MapFrom(x => x.company.company_name1))
                .ForMember(dest => dest.branch_f, src => src.MapFrom(x => x.company.branch_f))
                .ForMember(dest => dest.order_post_code, src => src.MapFrom(x => x.company.postal_cd))
                .ForMember(dest => dest.order_address1, src => src.MapFrom(x => x.company.address1))
                .ForMember(dest => dest.order_tel_no, src => src.MapFrom(x => x.company.tel_no))
                .ForMember(dest => dest.order_fax_no, src => src.MapFrom(x => x.company.fax_no))

                .ForMember(dest => dest.delivery_company_name1, src => src.MapFrom(x => x.company.company_name1))
                .ForMember(dest => dest.customer_person_name, src => src.MapFrom(x => x.company.customer_person_name))
                .ForMember(dest => dest.delivey_post_code, src => src.MapFrom(x => x.company.postal_cd))
                .ForMember(dest => dest.delivery_address1, src => src.MapFrom(x => x.company.address1))
                .ForMember(dest => dest.address2, src => src.MapFrom(x => x.company.address2))
                .ForMember(dest => dest.delivery_tel_no, src => src.MapFrom(x => x.company.tel_no))
                .ForMember(dest => dest.delivery_fax_no, src => src.MapFrom(x => x.company.fax_no))

                .ForMember(dest => dest.r_order_date, opt => opt.MapFrom(src => src.order_date.ToDateString()));

            CreateMap<ReceiveOrderRequest, ReceiveOrder>()
                .ForMember(dest => dest.order_date, src => src.MapFrom(x => x.order_date_fomated))
                .ForMember(dest => dest.details, opt => opt.Ignore());
            CreateMap<ReceiveOrderDtRequest, ReceiveOrderDt>()
                .ForMember(dest => dest.r_order_dl, src => src.MapFrom(x => x.r_order_dl_fomated));
            CreateMap<ReceiveOrder, ReceiveOrderResponse>()
                 .ForMember(dest => dest.order_date, opt => opt.MapFrom(src => src.order_date.ToDateString()))
                 .ForMember(dest => dest.company_name, src => src.MapFrom(x => x.company.company_name1))
                 .ForMember(dest => dest.customer_person_name, src => src.MapFrom(x => x.company.customer_person_name))
                 .ForMember(dest => dest.company_tel_no, src => src.MapFrom(x => x.company.tel_no))
                 .ForMember(dest => dest.company_fax_no, src => src.MapFrom(x => x.company.fax_no));
            CreateMap<IPagedList<ReceiveOrder>, PagedList<ReceiveOrderResponse>>();

            CreateMap<SeqRequest, Seq>();
            CreateMap<Seq, SeqResponse>();
            CreateMap<IPagedList<Seq>, PagedList<SeqResponse>>();

            CreateMap<SalePlanRequest, SalePlan>();

            CreateMap<SalePlan, SalePlanByBrandItemResponse>()
                .ForMember(dest => dest.company_cd, src => src.MapFrom(x => x.company.code))
                .ForMember(dest => dest.company_name, src => src.MapFrom(x => x.company.company_name1))
                .ForMember(dest => dest.order_qty, src => src.MapFrom(x => x.order_qty));

            CreateMap<SalePlan, SalePlanByBrandResponse>()
                .ForMember(dest => dest.order_ym, opt => opt.MapFrom(src => src.order_ym.ToString()));

            CreateMap<SalePlan, SalePlanByMonthItemResponse>()
                .ForMember(dest => dest.order_ym, opt => opt.MapFrom(src => src.order_ym.ToDateString()));
            CreateMap<Item, SalePlanByMonthResponse>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.item_sale_plans.ToList()))
                .ForMember(dest => dest.order_unit, opt => opt.MapFrom(src => src.std_unit))
                .ForMember(dest => dest.item_cd, opt => opt.MapFrom(src => src.code))
                .ForMember(dest => dest.item_edi_cd, opt => opt.MapFrom(src => src.std_unit))
                .ForMember(dest => dest.item_name1, opt => opt.MapFrom(src => src.name1))
                .ForMember(dest => dest.item_name2, opt => opt.MapFrom(src => src.name2));

            CreateMap<ProductPlanUpdateRequest, ProductPlanDay>();
            CreateMap<ProductPlanDayRequest, ProductPlanDay>();

            CreateMap<IPagedList<SystemSettings>, PagedList<SystemSettingsResponse>>();
            CreateMap<SystemSettingsRequest, SystemSettings>();
            CreateMap<SystemSettings, SystemSettingsResponse>();

            CreateMap<ProductPlanDay, ProductPlanByDayResponse>()
             .ForMember(dest => dest.item_cd, opt => opt.MapFrom(src => src.item_cd))
             .ForMember(dest => dest.item_name1, opt => opt.MapFrom(src => src.item_name1));

            CreateMap<InitDataByDayResponse, ProductPlanDay>()
                .ForMember(dest => dest.plan_day, opt => opt.MapFrom(src => src.plan_day_fomated));
            CreateMap<InitDataMonthResponse, ProductPlanMonth>()
                .ForMember(dest => dest.plan_month, opt => opt.MapFrom(src => src.plan_month_fomated));

            //CreateMap<IList<ProductPlanDay>,List<ProductPlanResponse>>();
        }
    }
}
