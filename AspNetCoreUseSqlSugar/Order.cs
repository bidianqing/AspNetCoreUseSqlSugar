using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_order")]
    public class Order
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string OrderNo { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class OrderModel
    {
        public string UserName { get; set; }

        public int OrderId { get; set; }

        public string OrderNo { get; set; }
    }
}
