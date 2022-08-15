using Domain.Abstractions;

namespace Domain.Entities
{
    public class Shipment : BaseEntity
    {
        /// <summary>
        /// 受注製番
        /// </summary>
        public string r_order_cd { get; set; }

        /// <summary>
        /// 得意先コード
        /// </summary>
        public string customer_cd { get; set; }

        /// <summary>
        /// 出荷SEQ
        /// </summary>
        public string shipment_cd { get; set; }

        /// <summary>
        /// 相手管理No
        /// </summary>
        public string? kakuteru_no { get; set; }

        /// <summary>
        /// 納品日
        /// </summary>
        public DateTime shipment_delivery_dt { get; set; }

        /// <summary>
        /// 出荷日
        /// </summary>
        public DateTime? shipment_dt { get; set; }

        /// <summary>
        /// 出荷先コード
        /// </summary>
        public string? delivery_cd { get; set; }

        /// <summary>
        /// 運賃個数
        /// </summary>
        public decimal? fare_qty { get; set; }

        /// <summary>
        /// 運賃単価
        /// </summary>
        public decimal? fare_cost { get; set; }

        /// <summary>
        /// 品目区分
        /// </summary>
        public int item_k { get; set; }

        /// <summary>
        /// 品目コード
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 出荷数
        /// </summary>
        public decimal? shipment_qty { get; set; }

        /// <summary>
        /// 納品先住所
        /// </summary>
        public string? delivery_address { get; set; }

        /// <summary>
        /// 運送種別
        /// </summary>
        public string transport_class { get; set; }

        /// <summary>
        /// 運送会社
        /// </summary>
        public string transport_cd { get; set; }

        /// <summary>
        /// 車種
        /// </summary>
        public string vehicle_tp { get; set; }

        /// <summary>
        /// 出荷倉庫コード
        /// </summary>
        public string warehouse_cd { get; set; }

        /// <summary>
        /// 出荷ステータス
        /// </summary>
        public string? shipment_status { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string? remark { get; set; }

        public virtual Company? shipment_company { get; set; }
        public virtual Warehouses? shipment_warehouse { get; set; }

        public Shipment()
        {
            id = Guid.NewGuid();
        }
    }
}
