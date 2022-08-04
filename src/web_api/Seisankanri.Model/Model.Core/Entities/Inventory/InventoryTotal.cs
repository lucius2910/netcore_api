using Framework.Core.Entities;

namespace Seisankanri.Model.Entities
{
    public class InventoryTotal : BaseEntity
    {
        public string branch_cd { get; set; }
        public string inventory_k { get; set; }
        public string inventory_cd { get; set; }
        public string item_cd { get; set; }
        public string item_div { get; set; }
        public bool inspection_k { get; set; }
        public string? location { get; set; }
        public string? lot_cd { get; set; }
        public DateTime? whin_dt { get; set; }
        public int? stock_qty { get; set; }
        public int? stock_amt { get; set; }
        public string? silo_k { get; set; }
        public string? remark { get; set; }
        public int? curmth_whin_qty1 { get; set; }
        public int? curmth_whin_qty2 { get; set; }
        public int? curmth_whin_qty3 { get; set; }
        public int? curmth_whin_qty4 { get; set; }
        public int? curmth_whin_qty5 { get; set; }
        public int? curmth_whin_qty6 { get; set; }
        public int? curmth_whin_qty7 { get; set; }
        public int? curmth_whin_qty8 { get; set; }
        public int? curmth_whin_qty9 { get; set; }
        public int? curmth_whin_qty10 { get; set; }
        public int? curmth_whout_qty1 { get; set; }
        public int? curmth_whout_qty2 { get; set; }
        public int? curmth_whout_qty3 { get; set; }
        public int? curmth_whout_qty4 { get; set; }
        public int? curmth_whout_qty5 { get; set; }
        public int? curmth_whout_qty6 { get; set; }
        public int? curmth_whout_qty7 { get; set; }
        public int? curmth_whout_qty8 { get; set; }
        public int? curmth_whout_qty9 { get; set; }
        public int? curmth_whout_qty10 { get; set; }
        public int? nextmth_whin_qty { get; set; }
        public int? nextmth_whout_qty { get; set; }
        public int? whin_qty_plan_pp { get; set; }
        public int? whin_qty_plan_po { get; set; }
        public string? last_whin_date { get; set; }
        public string? last_whout_date { get; set; }
        public string? last_invt_date { get; set; }
        public string? stock_cutoff_date { get; set; }
        public virtual Item item { get; set; }
        public InventoryTotal()
        {
            id = Guid.NewGuid();
        }
    }
}
