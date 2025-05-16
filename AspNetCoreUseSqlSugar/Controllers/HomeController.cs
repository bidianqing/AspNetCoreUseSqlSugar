using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;

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
        public async Task<List<User>> Get()
        {
            var exp = Expressionable.Create<Order, User>();
            exp.And((t1, t2) => t1.OrderNo != null && t1.OrderNo != "");

            _db.Queryable<Order>()
                .LeftJoin<User>((t1, t2) => t1.UserId == t2.Id)
                .Where(exp.ToExpression())
                .Select((t1, t2) => new OrderModel
                {
                    UserName = t2.Name,
                    OrderId = t1.Id,
                    OrderNo = t1.OrderNo
                })
                .ToList();

            await _userRepository.InsertAsync(new User
            {
                Name = "tom"
            });

            await _userRepository.InsertRangeAsync(new List<User>
            {
                new() {
                    Name = "jerry"
                },
                new() {
                    Name = "lucy"
                }
            });

            return await _userRepository.GetListAsync();
        }
    }
}
