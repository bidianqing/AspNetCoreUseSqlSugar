using Newtonsoft.Json;
using SqlSugar;
using System.Text.Json.Serialization;

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
        /// 可以是JSON对应的对象
        /// 只要能序列化成json的所有对象都可以
        /// 默认使用Newtonsoft.Json进行序列化
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
