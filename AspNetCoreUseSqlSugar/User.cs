using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_user")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
