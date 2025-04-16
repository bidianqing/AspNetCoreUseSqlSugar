using AspNetCoreUseSqlSugar;
using SqlSugar;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ISqlSugarClient>(sp =>
{
    var loggerFactory = sp.GetService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<ISqlSugarClient>();

    var configs = new List<ConnectionConfig>
    {
        new ConnectionConfig
        {
            ConfigId = "default",
            DbType = DbType.MySqlConnector,
            ConnectionString = "server=127.0.0.1;port=3306;database=demo;user id=root;password=root;CharacterSet=utf8mb4;SslMode=None;Allow User Variables=true;",
            IsAutoCloseConnection = true,
        },
        new ConnectionConfig
        {
            ConfigId = "blog",
            DbType = DbType.SqlServer,
            ConnectionString = "server=127.0.0.1;user id=sa;password=sa;database=BLOG;MultipleActiveResultSets=true",
            IsAutoCloseConnection = true,
        }
    };

    var db = new SqlSugarClient(configs);

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
    }


    return db;
});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseChangeDatabase();

app.MapControllers();

app.Run();
