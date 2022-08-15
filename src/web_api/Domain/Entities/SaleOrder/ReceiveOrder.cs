using Domain.Abstractions;

namespace Domain.Entities
{
    public class ReceiveOrder : BaseEntity
    {
        /// <summary>
        /// 受注日
        /// </summary>
        public DateTime order_date { get; set; }

        /// <summary>
        /// 受注製番
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 相手管理No
        /// </summary>
        public string? kakuteru_no { get; set; }

        /// <summary>
        /// 得意先コード
        /// </summary>
        public string customer_cd { get; set; }

        /// <summary>
        /// 出荷先コード
        /// </summary>
        public string destination_cd { get; set; }

        /// <summary>
        /// 品目区分
        /// </summary>
        public string item_k { get; set; }

        /// <summary>
        /// 品目コード
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 受注数
        /// </summary>
        public decimal order_qty { get; set; }

        /// <summary>
        /// 数量（本）
        /// </summary>
        public decimal? case_qty { get; set; }

        /// <summary>
        /// 数量（ｍ）
        /// </summary>
        public decimal? case_m { get; set; }

        /// <summary>
        /// 受注ステータス（新登場＝③登録済）
        /// </summary>
        public int order_status_k { get; set; }

        /// <summary>
        /// 出荷日
        /// </summary>
        public DateTime order_ship_dt { get; set; }

        /// <summary>
        /// 納品日
        /// </summary>
        public DateTime order_delivery_dt { get; set; }

        /// <summary>
        /// 生産残
        /// </summary>
        public decimal? product_remain_qty { get; set; }

        /// <summary>
        /// 出荷残数
        /// </summary>
        public decimal? destination_remain_qty { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string? remarks { get; set; }

        public virtual Company? company_customer_cd { get; set; }

        public virtual Company? company_destination_cd { get; set; }

        public virtual Item? item { get; set; }

        public ReceiveOrder()
        {
            id = Guid.NewGuid();
        }
    }
}
