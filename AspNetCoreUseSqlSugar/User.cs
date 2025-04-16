using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_user")]
    public class User : BaseAuditableEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
