using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
    }

    public abstract class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public virtual Guid Id { get; set; }
    }
}
