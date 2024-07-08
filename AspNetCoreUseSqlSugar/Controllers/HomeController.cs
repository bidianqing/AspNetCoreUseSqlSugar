using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace AspNetCoreUseSqlSugar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISqlSugarClient _db;

        public HomeController(ILogger<HomeController> logger, ISqlSugarClient db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public List<User> Get()
        {
            return _db.Queryable<User>().ToList();
        }
    }
}
