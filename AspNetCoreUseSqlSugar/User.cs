using Newtonsoft.Json.Linq;
using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_user")]
    public class User : BaseAuditableEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [SugarColumn(IsJson = true)]
        public JArray Tags { get; set; }

        [SugarColumn(IsJson = true)]
        public JObject Address { get; set; }
    }
}
