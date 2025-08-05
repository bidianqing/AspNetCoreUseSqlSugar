using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    public interface IRepository<T> : ISugarRepository, ISimpleClient<T> where T : class, new()
    {
        
    }
}
