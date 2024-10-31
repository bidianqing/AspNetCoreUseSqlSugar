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
                        logger.LogDebug($"Sql£º{sql} | Parameters£º{ string.Join(",", parameters.Select(x => $"{x.ParameterName} = {x.Value}")) }");
                    }
                },
                DataExecuting = (obj, model) =>
                {
                    logger.LogInformation(model.EntityName);
                }
            }
        }
    };
    
    return new SqlSugarClient(configs);
});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
