using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    public class ChangeDatabaseMiddleware
    {
        private readonly RequestDelegate _next;

        public ChangeDatabaseMiddleware(RequestDelegate next, ILogger<ChangeDatabaseMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISqlSugarClient db)
        {
            // 根据配置文件或者业务逻辑，动态切换数据库
            db.AsTenant().ChangeDatabase("blog");

            await _next(context);
        }
    }

    public static class ChangeDatabaseMiddlewareMiddlewareExtensions
    {
        public static IApplicationBuilder UseChangeDatabase(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ChangeDatabaseMiddleware>();
        }
    }
}
