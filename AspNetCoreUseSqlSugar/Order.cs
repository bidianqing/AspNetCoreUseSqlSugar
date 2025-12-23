using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_order")]
    public class Order : BaseAuditableEntity
    {
        [SugarColumn(ColumnName = "order_no")]
        public string OrderNo { get; set; }

        [SugarColumn(ColumnName = "order_price")]
        public decimal OrderPrice { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public Guid UserId { get; set; }

        [SugarColumn(ColumnName = "is_deleted")]
        public bool IsDeleted { get; set; }
    }

    [SugarTable("tb_order_item")]
    public class OrderItem : BaseAuditableEntity
    {
        [SugarColumn(ColumnName = "is_deleted")]
        public bool IsDeleted { get; set; }

        [SugarColumn(ColumnName = "order_id")]
        public Guid OrderId { get; set; }

        [SugarColumn(ColumnName = "product_name")]
        public string ProductName { get; set; }

        [SugarColumn(ColumnName = "unit")]
        public string Unit { get; set; }

        [SugarColumn(ColumnName = "unit_price")]
        public decimal UnitPrice { get; set; }

        [SugarColumn(ColumnName = "quantity")]
        public int Quantity { get; set; }
    }
}
