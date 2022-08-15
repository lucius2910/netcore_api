using Domain.Abstractions;

namespace Domain.Entities
{
    public class Item : BaseEntity
    {
        public string item_type { get; set; }

        public string code { get; set; }

        public int? llc { get; set; }

        public string short_name_kana { get; set; }

        public string name1 { get; set; }

        public string name2 { get; set; }

        public string? model_number { get; set; }

        public string account_cd { get; set; }

        public int? stock_qty_basis { get; set; }

        public int? min_stock_qty { get; set; }

        public string std_unit { get; set; }

        public int std_unitprice { get; set; }

        public string tax_div { get; set; }

        public string? procure_div { get; set; }

        public string? provide_div { get; set; }

        public int? prod_lot_qty { get; set; }

        public int? prod_lt { get; set; }

        public string? drawing_no { get; set; }

        public int? loss_qty { get; set; }

        public string? standard { get; set; }

        public int? pieces { get; set; }

        public string place_cd { get; set; }

        public string? account_cd2 { get; set; }

        public string? class_cd1 { get; set; }

        public string? class_cd2 { get; set; }

        public string? class_cd3 { get; set; }

        public string unit2 { get; set; }

        public int unit_conv_rate { get; set; }

        public string? stock_admin_div { get; set; }

        public string? admin_div { get; set; }

        public int per_weight { get; set; }

        public string? calc_div { get; set; }

        public int? specific_gravity { get; set; }

        public int? sa_unitprice { get; set; }

        public int? materiel_unitprice2 { get; set; }

        public int? materiel_unitprice3 { get; set; }

        public int? materiel_unitprice4 { get; set; }

        public int? materiel_unitprice5 { get; set; }

        public int? materiel_unitprice6 { get; set; }

        public int? materiel_unitprice7 { get; set; }

        public int? materiel_unitprice8 { get; set; }

        public int? materiel_unitprice9 { get; set; }

        public int? materiel_unitprice10 { get; set; }

        public int? materiel_unitprice11 { get; set; }

        public int? materiel_unitprice12 { get; set; }

        public int? materiel_unitprice13 { get; set; }

        public int? materiel_unitprice14 { get; set; }

        public int? materiel_unitprice15 { get; set; }

        public string? unit_conv_f { get; set; }

        public string? machine_cd { get; set; }

        public string? inspect_div { get; set; }

        public string? lot_admin_div { get; set; }

        public int term_calc_value { get; set; }

        public int? po_slip_issue_f { get; set; }

        public string company_cd { get; set; }

        public string? item_name3 { get; set; }

        public string? item_name4 { get; set; }

        public int? pp_counter { get; set; }

        public int? size1 { get; set; }

        public int? size2 { get; set; }

        public int? size3 { get; set; }

        public int? size4 { get; set; }

        public int? optsize1 { get; set; }

        public int? optsize2 { get; set; }

        public int? optsize3 { get; set; }

        public int? optsize4 { get; set; }

        public int? optsize5 { get; set; }

        public int? optsize6 { get; set; }

        public int? optsize7 { get; set; }

        public int? optsize8 { get; set; }

        public int? optsize9 { get; set; }

        public int? optsize10 { get; set; }

        public string care_div { get; set; }

        public string? class_cd4 { get; set; }

        public string? class_cd5 { get; set; }

        public string? class_cd6 { get; set; }

        public string? material_quality_cd { get; set; }

        public int? prod_max_lot_qty { get; set; }

        public int? calc_by_prod_f { get; set; }

        public string? prod_arrange_div { get; set; }

        public int? invalid_f { get; set; }

        public string? location { get; set; }

        public int? order { get; set; }

        public virtual Classification? type { get; set; }

        public virtual Machine? machine { get; set; }

        public virtual Company? place { get; set; }

        public virtual Company? company { get; set; }

        public virtual ICollection<ItemBuyPrices>? item_buy_prices { get; set; }

        public virtual ICollection<ItemSalePrices>? item_sale_prices { get; set; }

        public virtual ICollection<ItemPrice> item_prices { get; set; }

        public virtual ICollection<ReceiveOrder> item_receive_orders { get; set; }

        public virtual ICollection<SalePlan> item_sale_plans { get; set; }

        public virtual ICollection<ProductPlanDay>? product_plan_days { get; set; }

        public virtual ICollection<ProductPlanMonth>? product_plan_months { get; set; }

        public virtual ICollection<ItemEdi>? item_edis { get; set; }

        public virtual ICollection<InventoryTotal>? inventory_totals { get; set; }

        public virtual ICollection<AcceptanceInfo>? item_acceptance_infos { get; set; }

        public Item()
        {
            id = Guid.NewGuid();
        }
    }
}
