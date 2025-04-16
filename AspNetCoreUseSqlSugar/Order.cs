using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_order")]
    public class Order : BaseAuditableEntity
    {
        public Guid UserId { get; set; }

        public string OrderNo { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class OrderModel
    {
        public string UserName { get; set; }

        public Guid OrderId { get; set; }

        public string OrderNo { get; set; }
    }
}
