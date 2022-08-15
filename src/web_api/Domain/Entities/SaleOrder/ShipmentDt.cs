using Domain.Abstractions;

namespace Domain.Entities
{
    public class ShipmentDt : BaseEntity
    {
        /// <summary>
        /// 受注製番
        /// </summary>
        public string r_order_cd { get; set; }

        /// <summary>
        /// 出荷SEQ
        /// </summary>
        public string shipment_cd { get; set; }

        /// <summary>
        /// ロット番号From
        /// </summary>
        public decimal? lot_from { get; set; }

        /// <summary>
        /// ロット番号To
        /// </summary>
        public decimal? lot_to { get; set; }

        /// <summary>
        /// 数量（本）
        /// </summary>
        public decimal? case_qty { get; set; }

        /// <summary>
        /// 数量（ｍ）
        /// </summary>
        public decimal? case_m { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string? remark { get; set; }

    }
}
