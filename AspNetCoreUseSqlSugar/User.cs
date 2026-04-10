using Newtonsoft.Json;
using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    [SugarTable("tb_user")]
    public class User : BaseAuditableEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [SugarColumn(IsJson = true)]
        public string[] Tags { get; set; }

        /// <summary>
        /// JsonObject、JsonArray
        /// 只要能序列化成json的所有对象都可以
        /// https://github.com/DotNetNext/SqlSugar/blob/master/Src/Asp.NetCore2/SqlSugar/IntegrationServices/SerializeService.cs
        /// </summary>
        [SugarColumn(IsJson = true)]
        public Address Address { get; set; }
    }

    public class Address
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }
    }
}
