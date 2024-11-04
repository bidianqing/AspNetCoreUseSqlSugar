using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_order")]
    public class Order
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
