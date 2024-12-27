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
            DbType = DbType.MySql,
            ConnectionString = "server=127.0.0.1;port=3306;database=demo;user id=root;password=root;CharacterSet=utf8mb4;SslMode=None;Allow User Variables=true;",
            IsAutoCloseConnection = true,
            AopEvents = new AopEvents
            {
                OnLogExecuting = (sql, parameters) =>
                {
                    if (logger.IsEnabled(LogLevel.Debug))
                    {
                        //var totalExecutedTime = client.Ado.SqlExecutionTime.TotalMilliseconds;
                        logger.LogDebug("Sql Executed in {time} ms \r\n {sql}", 1000, UtilMethods.GetSqlString(DbType.SqlServer, sql, parameters));
                    }
                },
                DataExecuting = (obj, model) =>
                {
                    logger.LogInformation(model.EntityName);
                },
            },
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

    db.QueryFilter.AddTableFilter<User>(u => !u.IsDeleted);
    db.QueryFilter.AddTableFilter<Order>(u => !u.IsDeleted);

    return db;
});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseChangeDatabase();

app.MapControllers();

app.Run();
