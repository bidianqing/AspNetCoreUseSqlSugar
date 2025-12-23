using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        [SugarColumn(ColumnName = "createdon")]
        public DateTime CreatedOn { get; set; }

        [SugarColumn(ColumnName = "createdby")]
        public string CreatedBy { get; set; }

        [SugarColumn(ColumnName = "updatedon")]
        public DateTime UpdatedOn { get; set; }

        [SugarColumn(ColumnName = "updatedby")]
        public string UpdatedBy { get; set; }
    }

    public abstract class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public virtual Guid Id { get; set; }
    }
}
