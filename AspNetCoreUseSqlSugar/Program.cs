using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ISqlSugarClient>(sp =>
{
    var config = new ConnectionConfig
    {
        DbType = DbType.MySql,
        ConnectionString = "server=127.0.0.1;port=3306;database=demo;user id=root;password=root;CharacterSet=utf8mb4;SslMode=None;Allow User Variables=true;",
        IsAutoCloseConnection = true,
        AopEvents = new AopEvents
        {
            OnLogExecuting = (sql, parameters) =>
            {
                Console.WriteLine(sql);
            }
        }
    };

    return new SqlSugarClient(config);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
