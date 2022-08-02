using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class init_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "classifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code1 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    code2 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    name1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    name2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    remarks = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classifications", x => x.id);
                    table.UniqueConstraint("AK_classifications_code2", x => x.code2);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    place_f = table.Column<int>(type: "integer", nullable: false),
                    destination_f = table.Column<int>(type: "integer", nullable: false),
                    customer_f = table.Column<int>(type: "integer", nullable: false),
                    supplier_f = table.Column<int>(type: "integer", nullable: false),
                    outsourcer_f = table.Column<int>(type: "integer", nullable: false),
                    branch_f = table.Column<int>(type: "integer", nullable: false),
                    transpost_f = table.Column<int>(type: "integer", nullable: false),
                    maker_f = table.Column<int>(type: "integer", nullable: false),
                    group_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    short_name_kana = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    area_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    business_category_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    place_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    emp_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ex_edi_company_cd = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    company_short_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    company_name1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    company_name2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    kana = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    postal_cd = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    address1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    address2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    tel_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    extension_no = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    fax_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    flag6 = table.Column<int>(type: "integer", nullable: false),
                    flag7 = table.Column<int>(type: "integer", nullable: false),
                    flag5 = table.Column<int>(type: "integer", nullable: false),
                    flag8 = table.Column<int>(type: "integer", nullable: false),
                    flag4 = table.Column<int>(type: "integer", nullable: false),
                    half_width_item_name = table.Column<int>(type: "integer", nullable: false),
                    customer_president_name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    customer_person_name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    employee_count = table.Column<int>(type: "integer", nullable: false),
                    hold_man_hour = table.Column<int>(type: "integer", nullable: false),
                    capital = table.Column<int>(type: "integer", nullable: false),
                    account_closing_month = table.Column<int>(type: "integer", nullable: false),
                    remarks1 = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    remarks2 = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    sa_emp_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    cc_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    sight_notes_receivable = table.Column<int>(type: "integer", nullable: false),
                    cc_bank_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    sa_unitprice_ratio = table.Column<int>(type: "integer", nullable: false),
                    total_bill_f = table.Column<int>(type: "integer", nullable: false),
                    bill_detail_f = table.Column<int>(type: "integer", nullable: false),
                    billing_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    cutoff_date1 = table.Column<int>(type: "integer", nullable: false),
                    cutoff_date2 = table.Column<int>(type: "integer", nullable: false),
                    cutoff_date3 = table.Column<int>(type: "integer", nullable: false),
                    sa_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    sa_materiel_unit_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    sa_tax_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    sa_tax_calc_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    sa_tax_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    carrier_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    plan_cc_days = table.Column<int>(type: "integer", nullable: false),
                    ar_credit_limit_ratio = table.Column<int>(type: "integer", nullable: false),
                    ar_credit_limit = table.Column<int>(type: "integer", nullable: false),
                    sh_slip_row_count = table.Column<int>(type: "integer", nullable: false),
                    pu_emp_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    py_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    sight_notes_payable = table.Column<int>(type: "integer", nullable: false),
                    payee_bank_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    peyee_bank_account_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    peyee_bank_account_cd = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    payer_bank_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    pu_unitprice_ratio = table.Column<int>(type: "integer", nullable: false),
                    payee_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    pu_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    pu_tax_calc_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    pu_tax_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    py_cutoff_date1 = table.Column<int>(type: "integer", nullable: false),
                    provide_f = table.Column<int>(type: "integer", nullable: false),
                    ap_credit_limit_ratio = table.Column<int>(type: "integer", nullable: false),
                    ap_credit_limit = table.Column<int>(type: "integer", nullable: false),
                    py_assess_f = table.Column<int>(type: "integer", nullable: false),
                    plan_py_days = table.Column<int>(type: "integer", nullable: false),
                    py_cash_limit1 = table.Column<int>(type: "integer", nullable: false),
                    py_cash_limit2 = table.Column<int>(type: "integer", nullable: false),
                    py_cash_ratio1 = table.Column<int>(type: "integer", nullable: false),
                    py_cash_ratio2 = table.Column<int>(type: "integer", nullable: false),
                    emp_mail_address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    emp_phone = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    carrier_div2 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    plan_cc_months = table.Column<int>(type: "integer", nullable: false),
                    plan_py_months = table.Column<int>(type: "integer", nullable: false),
                    cc_cash_limit1 = table.Column<int>(type: "integer", nullable: false),
                    cc_cash_limit2 = table.Column<int>(type: "integer", nullable: false),
                    cc_cash_ratio1 = table.Column<int>(type: "integer", nullable: false),
                    cc_cash_ratio2 = table.Column<int>(type: "integer", nullable: false),
                    bank_commission_burden_f = table.Column<int>(type: "integer", nullable: false),
                    commission_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    transf_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    post_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    detail_reconcile_f = table.Column<int>(type: "integer", nullable: false),
                    plan_cc_display_f = table.Column<int>(type: "integer", nullable: false),
                    plan_cc_div = table.Column<int>(type: "integer", nullable: false),
                    edi_conv_cd = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    plan_prod_del_f = table.Column<int>(type: "integer", nullable: false),
                    transport_lt = table.Column<int>(type: "integer", nullable: false),
                    dept_cost = table.Column<int>(type: "integer", nullable: false),
                    plan_activity = table.Column<int>(type: "integer", nullable: false),
                    work_unitprice = table.Column<int>(type: "integer", nullable: false),
                    po_if_div = table.Column<int>(type: "integer", nullable: false),
                    export_f = table.Column<int>(type: "integer", nullable: false),
                    import_f = table.Column<int>(type: "integer", nullable: false),
                    invalid_f = table.Column<int>(type: "integer", nullable: false),
                    pu_tax_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    plan_py_div = table.Column<int>(type: "integer", nullable: false),
                    py_transf_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    py_post_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    cover_handling_fee_div = table.Column<int>(type: "integer", nullable: false),
                    commission_paid_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ar_currency_cd = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    ar_for_cur_amt_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ar_unitprice_conv_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ar_amt_conv_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ap_currency_cd = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    ap_for_cur_amt_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ap_unitprice_conv_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ap_amt_conv_rd_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    bank_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    bank_account_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    bank_account_name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                    table.UniqueConstraint("AK_company_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "function",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    module = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    text = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    path = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    method = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    parent_cd = table.Column<string>(type: "text", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    icon = table.Column<string>(type: "text", nullable: true),
                    parentid = table.Column<Guid>(type: "uuid", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_function", x => x.id);
                    table.ForeignKey(
                        name: "FK_function_function_parentid",
                        column: x => x.parentid,
                        principalTable: "function",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    item_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    llc = table.Column<int>(type: "integer", nullable: false),
                    short_name_kana = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    name2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    model_number = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    account_cd = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    stock_qty_basis = table.Column<int>(type: "integer", nullable: false),
                    min_stock_qty = table.Column<int>(type: "integer", nullable: false),
                    std_unit = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    std_unitprice = table.Column<int>(type: "integer", nullable: false),
                    tax_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    procure_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    provide_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    prod_lot_qty = table.Column<int>(type: "integer", nullable: false),
                    prod_lt = table.Column<int>(type: "integer", nullable: false),
                    drawing_no = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    loss_qty = table.Column<int>(type: "integer", nullable: false),
                    place_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    account_cd2 = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    class_cd1 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    class_cd2 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    class_cd3 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    unit2 = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    unit_conv_rate = table.Column<int>(type: "integer", nullable: false),
                    stock_admin_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    admin_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    per_weight = table.Column<int>(type: "integer", nullable: false),
                    calc_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    specific_gravity = table.Column<int>(type: "integer", nullable: false),
                    sa_unitprice = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice2 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice3 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice4 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice5 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice6 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice7 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice8 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice9 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice10 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice11 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice12 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice13 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice14 = table.Column<int>(type: "integer", nullable: false),
                    materiel_unitprice15 = table.Column<int>(type: "integer", nullable: false),
                    unit_conv_f = table.Column<int>(type: "integer", nullable: false),
                    machine_cd = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    inspect_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    lot_admin_div = table.Column<int>(type: "integer", nullable: false),
                    term_calc_value = table.Column<int>(type: "integer", nullable: false),
                    po_slip_issue_f = table.Column<int>(type: "integer", nullable: false),
                    company_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_name3 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    item_name4 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    pp_counter = table.Column<int>(type: "integer", nullable: false),
                    size1 = table.Column<int>(type: "integer", nullable: false),
                    size2 = table.Column<int>(type: "integer", nullable: false),
                    size3 = table.Column<int>(type: "integer", nullable: false),
                    size4 = table.Column<int>(type: "integer", nullable: false),
                    optsize1 = table.Column<int>(type: "integer", nullable: false),
                    optsize2 = table.Column<int>(type: "integer", nullable: false),
                    optsize3 = table.Column<int>(type: "integer", nullable: false),
                    optsize4 = table.Column<int>(type: "integer", nullable: false),
                    optsize5 = table.Column<int>(type: "integer", nullable: false),
                    optsize6 = table.Column<int>(type: "integer", nullable: false),
                    optsize7 = table.Column<int>(type: "integer", nullable: false),
                    optsize8 = table.Column<int>(type: "integer", nullable: false),
                    optsize9 = table.Column<int>(type: "integer", nullable: false),
                    optsize10 = table.Column<int>(type: "integer", nullable: false),
                    care_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    class_cd4 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    class_cd5 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    class_cd6 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    material_quality_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    prod_max_lot_qty = table.Column<int>(type: "integer", nullable: false),
                    calc_by_prod_f = table.Column<int>(type: "integer", nullable: false),
                    prod_arrange_div = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    invalid_f = table.Column<int>(type: "integer", nullable: false),
                    location = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item", x => x.id);
                    table.UniqueConstraint("AK_item_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "log_action",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    method = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    body = table.Column<string>(type: "text", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_action", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log_exception",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    function = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    stack_trace = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_exception", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "manufactory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    lat = table.Column<string>(type: "text", nullable: true),
                    lon = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufactory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "master_code",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_code", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "resource",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    lang = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    module = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    screen = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    text = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                    table.UniqueConstraint("AK_role_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "sale_plan",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_type = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_edi_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_name1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    item_name2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    order_unit = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    standad_unit_qty = table.Column<int>(type: "integer", nullable: false),
                    order_ym = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    order_qty = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_plan", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    control_company = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    control_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    postcode = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    address = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    capacity = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    rent_price = table.Column<int>(type: "integer", nullable: false),
                    item_info = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    person_cd1 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    person_cd2 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    remarks = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "calendars",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    calendar_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    company_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    open_status = table.Column<int>(type: "integer", nullable: false),
                    companyid = table.Column<Guid>(type: "uuid", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calendars", x => x.id);
                    table.ForeignKey(
                        name: "FK_calendars_company_companyid",
                        column: x => x.companyid,
                        principalTable: "company",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "item_buy_prices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    price = table.Column<int>(type: "integer", nullable: true),
                    unit = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    meterial_price = table.Column<int>(type: "integer", nullable: true),
                    process_price = table.Column<int>(type: "integer", nullable: true),
                    auxiliary_price = table.Column<int>(type: "integer", nullable: true),
                    effective_startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    effective_enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    customer = table.Column<Guid>(type: "uuid", nullable: true),
                    min_qty = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_buy_prices", x => x.id);
                    table.ForeignKey(
                        name: "FK_item_buy_prices_item_item_cd",
                        column: x => x.item_cd,
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "item_price",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    buy_price = table.Column<int>(type: "integer", nullable: true),
                    sale_price = table.Column<int>(type: "integer", nullable: true),
                    unit = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    basic_qty = table.Column<int>(type: "integer", nullable: true),
                    meterial_price = table.Column<int>(type: "integer", nullable: true),
                    process_price = table.Column<int>(type: "integer", nullable: true),
                    auxiliary_price = table.Column<int>(type: "integer", nullable: true),
                    effective_startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    effective_enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    customer_cd = table.Column<Guid>(type: "uuid", nullable: true),
                    min_qty = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_price", x => x.id);
                    table.ForeignKey(
                        name: "FK_item_price_item_item_cd",
                        column: x => x.item_cd,
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "item_sale_prices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    unit = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    effective_startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    effective_enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    customer = table.Column<Guid>(type: "uuid", nullable: true),
                    min_qty = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_sale_prices", x => x.id);
                    table.ForeignKey(
                        name: "FK_item_sale_prices_item_item_cd",
                        column: x => x.item_cd,
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_cd = table.Column<string>(type: "text", nullable: false),
                    roleid = table.Column<Guid>(type: "uuid", nullable: false),
                    function_cd = table.Column<string>(type: "text", nullable: false),
                    functionid = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_permission_function_functionid",
                        column: x => x.functionid,
                        principalTable: "function",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permission_role_roleid",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    hashpass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    salt = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: true),
                    deparment_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    branch_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    jobtitle_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    furigana = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.UniqueConstraint("AK_user_code", x => x.code);
                    table.ForeignKey(
                        name: "FK_user_classifications_deparment_cd",
                        column: x => x.deparment_cd,
                        principalTable: "classifications",
                        principalColumn: "code2",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_classifications_jobtitle_cd",
                        column: x => x.jobtitle_cd,
                        principalTable: "classifications",
                        principalColumn: "code2",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_company_branch_cd",
                        column: x => x.branch_cd,
                        principalTable: "company",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_role_role_cd",
                        column: x => x.role_cd,
                        principalTable: "role",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "machine",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    location = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    producer = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    serial = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    department_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    supplier_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    admin_cd = table.Column<string>(type: "text", nullable: false),
                    mold_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    type = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    size1 = table.Column<int>(type: "integer", nullable: true),
                    size2 = table.Column<int>(type: "integer", nullable: true),
                    size3 = table.Column<int>(type: "integer", nullable: true),
                    effective_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    remarks = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    adminid = table.Column<Guid>(type: "uuid", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_machine", x => x.id);
                    table.ForeignKey(
                        name: "FK_machine_classifications_department_cd",
                        column: x => x.department_cd,
                        principalTable: "classifications",
                        principalColumn: "code2",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_machine_company_supplier_cd",
                        column: x => x.supplier_cd,
                        principalTable: "company",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_machine_user_adminid",
                        column: x => x.adminid,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "receive_order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    company_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    user_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    s_total_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    branch_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    order_status = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    remark = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receive_order", x => x.id);
                    table.UniqueConstraint("AK_receive_order_code", x => x.code);
                    table.ForeignKey(
                        name: "FK_receive_order_company_branch_cd",
                        column: x => x.branch_cd,
                        principalTable: "company",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_receive_order_company_company_cd",
                        column: x => x.company_cd,
                        principalTable: "company",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_receive_order_user_user_cd",
                        column: x => x.user_cd,
                        principalTable: "user",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "receive_order_dt",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    r_order_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    company_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    warehouse_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    delivery_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    r_order_input = table.Column<decimal>(type: "numeric", nullable: false),
                    r_order_pieces_qty = table.Column<decimal>(type: "numeric", nullable: false),
                    r_order_qty = table.Column<decimal>(type: "numeric", nullable: false),
                    r_order_unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    r_order_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    r_order_dl = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    remarks = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receive_order_dt", x => x.id);
                    table.ForeignKey(
                        name: "FK_receive_order_dt_company_delivery_cd",
                        column: x => x.delivery_cd,
                        principalTable: "company",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_receive_order_dt_company_warehouse_cd",
                        column: x => x.warehouse_cd,
                        principalTable: "company",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_receive_order_dt_item_item_cd",
                        column: x => x.item_cd,
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_receive_order_dt_receive_order_r_order_cd",
                        column: x => x.r_order_cd,
                        principalTable: "receive_order",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_calendars_companyid",
                table: "calendars",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "IX_function_parentid",
                table: "function",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "IX_item_buy_prices_item_cd",
                table: "item_buy_prices",
                column: "item_cd");

            migrationBuilder.CreateIndex(
                name: "IX_item_price_item_cd",
                table: "item_price",
                column: "item_cd");

            migrationBuilder.CreateIndex(
                name: "IX_item_sale_prices_item_cd",
                table: "item_sale_prices",
                column: "item_cd");

            migrationBuilder.CreateIndex(
                name: "IX_machine_adminid",
                table: "machine",
                column: "adminid");

            migrationBuilder.CreateIndex(
                name: "IX_machine_department_cd",
                table: "machine",
                column: "department_cd");

            migrationBuilder.CreateIndex(
                name: "IX_machine_supplier_cd",
                table: "machine",
                column: "supplier_cd");

            migrationBuilder.CreateIndex(
                name: "IX_permission_functionid",
                table: "permission",
                column: "functionid");

            migrationBuilder.CreateIndex(
                name: "IX_permission_roleid",
                table: "permission",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "IX_receive_order_branch_cd",
                table: "receive_order",
                column: "branch_cd");

            migrationBuilder.CreateIndex(
                name: "IX_receive_order_company_cd",
                table: "receive_order",
                column: "company_cd");

            migrationBuilder.CreateIndex(
                name: "IX_receive_order_user_cd",
                table: "receive_order",
                column: "user_cd");

            migrationBuilder.CreateIndex(
                name: "IX_receive_order_dt_delivery_cd",
                table: "receive_order_dt",
                column: "delivery_cd");

            migrationBuilder.CreateIndex(
                name: "IX_receive_order_dt_item_cd",
                table: "receive_order_dt",
                column: "item_cd");

            migrationBuilder.CreateIndex(
                name: "IX_receive_order_dt_r_order_cd",
                table: "receive_order_dt",
                column: "r_order_cd");

            migrationBuilder.CreateIndex(
                name: "IX_receive_order_dt_warehouse_cd",
                table: "receive_order_dt",
                column: "warehouse_cd");

            migrationBuilder.CreateIndex(
                name: "IX_user_branch_cd",
                table: "user",
                column: "branch_cd");

            migrationBuilder.CreateIndex(
                name: "IX_user_deparment_cd",
                table: "user",
                column: "deparment_cd");

            migrationBuilder.CreateIndex(
                name: "IX_user_jobtitle_cd",
                table: "user",
                column: "jobtitle_cd");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_cd",
                table: "user",
                column: "role_cd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calendars");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "item_buy_prices");

            migrationBuilder.DropTable(
                name: "item_price");

            migrationBuilder.DropTable(
                name: "item_sale_prices");

            migrationBuilder.DropTable(
                name: "log_action");

            migrationBuilder.DropTable(
                name: "log_exception");

            migrationBuilder.DropTable(
                name: "machine");

            migrationBuilder.DropTable(
                name: "manufactory");

            migrationBuilder.DropTable(
                name: "master_code");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "receive_order_dt");

            migrationBuilder.DropTable(
                name: "resource");

            migrationBuilder.DropTable(
                name: "sale_plan");

            migrationBuilder.DropTable(
                name: "warehouses");

            migrationBuilder.DropTable(
                name: "function");

            migrationBuilder.DropTable(
                name: "item");

            migrationBuilder.DropTable(
                name: "receive_order");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "classifications");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
