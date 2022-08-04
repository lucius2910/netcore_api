using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public class Company : BaseEntity
    {
        public string? code { get; set; }

        public int place_f { get; set; }

        public int destination_f { get; set; }

        public int customer_f { get; set; }

        public int supplier_f { get; set; }

        public int outsourcer_f { get; set; }

        public int branch_f { get; set; }

        public int transpost_f { get; set; }

        public int maker_f { get; set; }

        public string? group_cd { get; set; }

        public string? short_name_kana { get; set; }

        public string? area_cd { get; set; }

        public string? business_category_cd { get; set; }

        public string? place_div { get; set; }

        public string emp_cd { get; set; }

        public string? ex_edi_company_cd { get; set; }

        public string company_short_name { get; set; }

        public string company_name1 { get; set; }

        public string company_name2 { get; set; }

        public string kana { get; set; }

        public string postal_cd { get; set; }

        public string address1 { get; set; }

        public string? address2 { get; set; }

        public string tel_no { get; set; }

        public string? extension_no { get; set; }

        public string fax_no { get; set; }

        public int? flag6 { get; set; }

        public int? flag7 { get; set; }

        public int? flag5 { get; set; }

        public int? flag8 { get; set; }

        public int? flag4 { get; set; }

        public int? half_width_item_name { get; set; }

        public string customer_president_name { get; set; }

        public string customer_person_name { get; set; }

        public int? employee_count { get; set; }

        public int? hold_man_hour { get; set; }

        public int? capital { get; set; }

        public int? account_closing_month { get; set; }

        public string? remarks1 { get; set; }

        public string? remarks2 { get; set; }

        public string sa_emp_cd { get; set; }

        public string? cc_div { get; set; }

        public int? sight_notes_receivable { get; set; }

        public string? cc_bank_cd { get; set; }

        public int? sa_unitprice_ratio { get; set; }

        public int? total_bill_f { get; set; }

        public int? bill_detail_f { get; set; }

        public string? billing_cd { get; set; }

        public int cutoff_date1 { get; set; }

        public int? cutoff_date2 { get; set; }

        public int? cutoff_date3 { get; set; }

        public string sa_rd_div { get; set; }

        public string sa_materiel_unit_div { get; set; }

        public string sa_tax_div { get; set; }

        public string sa_tax_calc_div { get; set; }

        public string sa_tax_rd_div { get; set; }

        public string? carrier_div { get; set; }

        public int? plan_cc_days { get; set; }

        public int? ar_credit_limit_ratio { get; set; }

        public int? ar_credit_limit { get; set; }

        public int? sh_slip_row_count { get; set; }

        public string? pu_emp_cd { get; set; }

        public string? py_div { get; set; }

        public int? sight_notes_payable { get; set; }

        public string? payee_bank_cd { get; set; }

        public string? peyee_bank_account_div { get; set; }

        public string? peyee_bank_account_cd { get; set; }

        public string? payer_bank_cd { get; set; }

        public int? pu_unitprice_ratio { get; set; }

        public string? payee_cd { get; set; }

        public string? pu_rd_div { get; set; }

        public string? pu_tax_calc_div { get; set; }

        public string? pu_tax_rd_div { get; set; }

        public int? py_cutoff_date1 { get; set; }

        public int? provide_f { get; set; }

        public int? ap_credit_limit_ratio { get; set; }

        public int? ap_credit_limit { get; set; }

        public int? py_assess_f { get; set; }

        public int? plan_py_days { get; set; }

        public int? py_cash_limit1 { get; set; }

        public int? py_cash_limit2 { get; set; }

        public int? py_cash_ratio1 { get; set; }

        public int? py_cash_ratio2 { get; set; }

        public string emp_mail_address { get; set; }

        public string emp_phone { get; set; }

        public string? carrier_div2 { get; set; }

        public int? plan_cc_months { get; set; }

        public int? plan_py_months { get; set; }

        public int? cc_cash_limit1 { get; set; }

        public int? cc_cash_limit2 { get; set; }

        public int? cc_cash_ratio1 { get; set; }

        public int? cc_cash_ratio2 { get; set; }

        public string? bank_commission_burden_f { get; set; }

        public string? commission_div { get; set; }

        public string? transf_div { get; set; }

        public string? post_div { get; set; }

        public string? detail_reconcile_f { get; set; }

        public string? plan_cc_display_f { get; set; }

        public string? plan_cc_div { get; set; }

        public string? edi_conv_cd { get; set; }

        public string? plan_prod_del_f { get; set; }

        public int? transport_lt { get; set; }

        public int? dept_cost { get; set; }

        public int? plan_activity { get; set; }

        public int? work_unitprice { get; set; }

        public string? po_if_div { get; set; }

        public int? export_f { get; set; }

        public int? import_f { get; set; }

        public int? invalid_f { get; set; }

        public string? pu_tax_div { get; set; }

        public string? plan_py_div { get; set; }

        public string? py_transf_div { get; set; }

        public string? py_post_div { get; set; }

        public string? cover_handling_fee_div { get; set; }

        public string? commission_paid_div { get; set; }

        public string? ar_currency_cd { get; set; }

        public string? ar_for_cur_amt_rd_div { get; set; }

        public string? ar_unitprice_conv_rd_div { get; set; }

        public string? ar_amt_conv_rd_div { get; set; }

        public string? ap_currency_cd { get; set; }

        public string? ap_for_cur_amt_rd_div { get; set; }

        public string? ap_unitprice_conv_rd_div { get; set; }

        public string? ap_amt_conv_rd_div { get; set; }

        public string bank_cd { get; set; }

        public string bank_account_number { get; set; }

        public string bank_account_name { get; set; }

        public virtual ICollection<Machine> machines { get; set; }

        public virtual ICollection<User> users { get; set; }

        public virtual ICollection<ReceiveOrder> receive_order_companys { get; set; }

        public virtual ICollection<ReceiveOrder> receive_order_branchs { get; set; }

        public virtual ICollection<ReceiveOrderDt> receive_order_dt_warehouses { get; set; }

        public virtual ICollection<ReceiveOrderDt> receive_order_dt_deliverys { get; set; }

        public virtual ICollection<Item> item_places { get; set; }

        public virtual ICollection<Item> item_companys { get; set; }

        public virtual ICollection<Calendar> calendars { get; set; }

        public virtual ICollection<SalePlan> sale_plan_companys { get; set; }

        public virtual ICollection<ItemEdi>? item_edi_companys { get; set; }

        public Company()
        {
            id = Guid.NewGuid();
        }
    }
}
