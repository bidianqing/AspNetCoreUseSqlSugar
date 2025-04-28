using AspNetCoreUseSqlSugar;
using SqlSugar;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ISqlSugarClient>(sp =>
{
    var loggerFactory = sp.GetService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<ISqlSugarClient>();

    var httpContextAccessor = sp.GetService<IHttpContextAccessor>();

    var configs = builder.Configuration.GetSection("DbConfigs").Get<ConnectionConfig[]>();

    var db = new SqlSugarClient([.. configs]);

    //db.QueryFilter.AddTableFilter<User>(u => !u.IsDeleted, QueryFilterProvider.FilterJoinPosition.Where);
    //db.QueryFilter.AddTableFilter<Order>(u => !u.IsDeleted);

    foreach (var config in configs)
    {
        db.GetConnection(config.ConfigId).Aop.OnLogExecuted = (sql, parameters) =>
        {
            var totalExecutedTime = db.Ado.SqlExecutionTime.TotalMilliseconds;
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Sql Executed in {time} ms \r\n{sql}", totalExecutedTime, UtilMethods.GetSqlString(config.DbType, sql, parameters));
            }

            if (totalExecutedTime > 1000)
            {
                logger.LogWarning("Sql Executed in {time} ms \r\n{sql}", totalExecutedTime, UtilMethods.GetSqlString(config.DbType, sql, parameters));
            }
        };

        db.GetConnection(config.ConfigId).Aop.DataExecuting = (oldValue, entityInfo) =>
        {
            if (entityInfo.OperationType == DataFilterType.InsertByObject)
            {
                // ����Id���ɲ���
                if (entityInfo.PropertyName == nameof(BaseEntity.Id) 
                    && entityInfo.EntityValue is BaseEntity baseEntity
                    && baseEntity.Id == Guid.Empty)
                {
                    entityInfo.SetValue(Guid.CreateVersion7(DateTimeOffset.Now));
                }

                // ����ʱ�䡢����ʱ�����ɲ���
                if (entityInfo.PropertyName == nameof(BaseAuditableEntity.Created) || entityInfo.PropertyName == nameof(BaseAuditableEntity.LastModified))
                {
                    entityInfo.SetValue(DateTimeOffset.Now);
                }

                // �����ˡ����������ɲ���
                if (entityInfo.PropertyName == nameof(BaseAuditableEntity.CreatedBy) || entityInfo.PropertyName == nameof(BaseAuditableEntity.LastModifiedBy))
                {
                    entityInfo.SetValue(httpContextAccessor.HttpContext?.User.Identity.Name ?? "�����û�");
                }
            }
            else if (entityInfo.OperationType == DataFilterType.UpdateByObject)
            {
                // ����ʱ�����ɲ���
                if (entityInfo.PropertyName == nameof(BaseAuditableEntity.LastModified))
                {
                    entityInfo.SetValue(DateTimeOffset.Now);
                }

                // ���������ɲ���
                if (entityInfo.PropertyName == nameof(BaseAuditableEntity.LastModifiedBy))
                {
                    entityInfo.SetValue(httpContextAccessor.HttpContext?.User.Identity.Name ?? "�����û�");
                }
            }
        };
    }


    return db;
});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseChangeDatabase();

app.MapControllers();

app.Run();
