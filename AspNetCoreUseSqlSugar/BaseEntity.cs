using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }

    public abstract class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public virtual Guid Id { get; set; }
    }
}
