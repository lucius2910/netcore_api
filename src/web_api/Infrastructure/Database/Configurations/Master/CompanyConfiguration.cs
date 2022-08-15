using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("company", "public");

            builder.Property(t => t.code).HasMaxLength(15);
            builder.Property(t => t.place_f).IsRequired();
            builder.Property(t => t.destination_f).IsRequired();
            builder.Property(t => t.customer_f).IsRequired();
            builder.Property(t => t.supplier_f).IsRequired();
            builder.Property(t => t.outsourcer_f).IsRequired();
            builder.Property(t => t.branch_f).IsRequired();
            builder.Property(t => t.transpost_f).IsRequired();
            builder.Property(t => t.maker_f).IsRequired();
            builder.Property(t => t.group_cd).HasMaxLength(15);
            builder.Property(t => t.short_name_kana).HasMaxLength(32);
            builder.Property(t => t.area_cd).HasMaxLength(15);
            builder.Property(t => t.business_category_cd).HasMaxLength(15);
            builder.Property(t => t.place_div).HasMaxLength(15);
            builder.Property(t => t.emp_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.ex_edi_company_cd).HasMaxLength(15);
            builder.Property(t => t.company_short_name).HasMaxLength(64).IsRequired();
            builder.Property(t => t.company_name1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.company_name2).HasMaxLength(160).IsRequired();
            builder.Property(t => t.kana).HasMaxLength(80).IsRequired();
            builder.Property(t => t.postal_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.address1).HasMaxLength(160).IsRequired();
            builder.Property(t => t.address2).HasMaxLength(160);
            builder.Property(t => t.tel_no).HasMaxLength(30).IsRequired();
            builder.Property(t => t.extension_no).HasMaxLength(5);
            builder.Property(t => t.fax_no).HasMaxLength(30).IsRequired();
            builder.Property(t => t.customer_president_name).HasMaxLength(160).IsRequired();
            builder.Property(t => t.customer_person_name).HasMaxLength(160).IsRequired();
            builder.Property(t => t.remarks1).HasMaxLength(240);
            builder.Property(t => t.remarks2).HasMaxLength(240);
            builder.Property(t => t.sa_emp_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.cc_div).HasMaxLength(8);
            builder.Property(t => t.cc_bank_cd).HasMaxLength(15);
            builder.Property(t => t.billing_cd).HasMaxLength(15);
            builder.Property(t => t.cutoff_date1).IsRequired();
            builder.Property(t => t.sa_rd_div).HasMaxLength(15).IsRequired();
            builder.Property(t => t.sa_materiel_unit_div).HasMaxLength(15).IsRequired();
            builder.Property(t => t.sa_tax_div).HasMaxLength(15).IsRequired();
            builder.Property(t => t.sa_tax_calc_div).HasMaxLength(15).IsRequired();
            builder.Property(t => t.sa_tax_rd_div).HasMaxLength(15).IsRequired();
            builder.Property(t => t.carrier_div).HasMaxLength(15);
            builder.Property(t => t.pu_emp_cd).HasMaxLength(15);
            builder.Property(t => t.py_div).HasMaxLength(15);
            builder.Property(t => t.payee_bank_cd).HasMaxLength(15);
            builder.Property(t => t.peyee_bank_account_div).HasMaxLength(15);
            builder.Property(t => t.peyee_bank_account_cd).HasMaxLength(15);
            builder.Property(t => t.payer_bank_cd).HasMaxLength(15);
            builder.Property(t => t.pu_unitprice_ratio).IsRequired();
            builder.Property(t => t.pu_rd_div).HasMaxLength(15);
            builder.Property(t => t.payee_cd).HasMaxLength(15);
            builder.Property(t => t.pu_tax_rd_div).HasMaxLength(15);
            builder.Property(t => t.pu_tax_calc_div).HasMaxLength(15);
            builder.Property(t => t.emp_mail_address).HasMaxLength(240).IsRequired();
            builder.Property(t => t.emp_phone).HasMaxLength(240).IsRequired();
            builder.Property(t => t.carrier_div2).HasMaxLength(15);
            builder.Property(t => t.bank_commission_burden_f).HasMaxLength(1);
            builder.Property(t => t.commission_div).HasMaxLength(15);
            builder.Property(t => t.transf_div).HasMaxLength(15);
            builder.Property(t => t.post_div).HasMaxLength(15);
            builder.Property(t => t.plan_cc_display_f).HasMaxLength(1);
            builder.Property(t => t.plan_cc_div).HasMaxLength(1);
            builder.Property(t => t.detail_reconcile_f).HasMaxLength(1);
            builder.Property(t => t.plan_prod_del_f).HasMaxLength(1);
            builder.Property(t => t.po_if_div).HasMaxLength(1);
            builder.Property(t => t.edi_conv_cd).HasMaxLength(40);
            builder.Property(t => t.pu_tax_div).HasMaxLength(15);
            builder.Property(t => t.py_post_div).HasMaxLength(15);
            builder.Property(t => t.py_transf_div).HasMaxLength(15);
            builder.Property(t => t.commission_paid_div).HasMaxLength(15);
            builder.Property(t => t.ar_for_cur_amt_rd_div).HasMaxLength(15);
            builder.Property(t => t.ar_amt_conv_rd_div).HasMaxLength(15);
            builder.Property(t => t.ap_for_cur_amt_rd_div).HasMaxLength(15);
            builder.Property(t => t.ar_unitprice_conv_rd_div).HasMaxLength(15);
            builder.Property(t => t.ap_unitprice_conv_rd_div).HasMaxLength(15);
            builder.Property(t => t.ap_amt_conv_rd_div).HasMaxLength(15);
            builder.Property(t => t.ar_currency_cd).HasMaxLength(3);
            builder.Property(t => t.ap_currency_cd).HasMaxLength(3);
            builder.Property(t => t.plan_py_div).HasMaxLength(1);
            builder.Property(t => t.cover_handling_fee_div).HasMaxLength(1);
            builder.Property(t => t.bank_cd).HasMaxLength(15).IsRequired();
            builder.Property(t => t.bank_account_number).HasMaxLength(15).IsRequired();
            builder.Property(t => t.bank_account_name).HasMaxLength(15).IsRequired();
        }
    }
}
