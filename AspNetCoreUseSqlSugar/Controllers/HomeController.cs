using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async Task<List<User>> Get()
        {
            //_db.Queryable<Order>()
            //    .LeftJoin<User>((t1, t2) => t1.UserId == t2.Id)
            //    .Where(exp.ToExpression())
            //    .Select((t1, t2) => new OrderModel
            //    {
            //        UserName = t2.Name,
            //        OrderId = t1.Id,
            //        OrderNo = t1.OrderNo
            //    })
            //    .ToList();

            //await _userRepository.InsertAsync(new User
            //{
            //    Name = "lily2",
            //    Tags = ["司机", "父亲"],
            //    Address = new JObject
            //    {
            //        ["city"] = "北京",
            //        ["district"] = "昌平"
            //    }
            //});

            var exp = Expressionable.Create<User>();
            //exp.And(t1 => t1.Name == "tom");

            var whereTags = new List<string> { "父亲" };
            var tags = JsonConvert.SerializeObject(whereTags);

            return await _userRepository.AsQueryable()
                .Where(exp.ToExpression())
                //.WhereIF(true, "address ->> 'city' = @city", new { city = "北京" })
                .WhereIF(true, "tags @> @tags::jsonb", new { tags })
                .ToListAsync();
        }
    }
}
