using SqlSugar;

namespace AspNetCoreUseSqlSugar
{
    public class Repository<T> : SimpleClient<T>, IRepository<T> where T : class, new()
    {
        public Repository(ISqlSugarClient context) : base(context)
        {

        }
    }
}
