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
        private readonly IRepository<User> _userRepository;

        public HomeController(ILogger<HomeController> logger, ISqlSugarClient db, IRepository<User> userRepository)
        {
            _logger = logger;
            _db = db;
            _userRepository = userRepository;
        }

        [HttpGet]
        public List<User> Get()
        {
            //return _userRepository.GetList();

            _db.Queryable<Order>()
                .LeftJoin<User>((t1, t2) => t1.UserId == t2.Id)
                .Select((t1, t2) => new Order
                {
                    UserId = t1.Id
                })
                .ToList();
            return _db.Queryable<User>().Where(x => x.Id == 1).ToList();
        }
    }
}
