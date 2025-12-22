using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace AspNetCoreUseSqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ISqlSugarClient _db;
        private readonly object _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public OrderController(ILogger<OrderController> logger, ISqlSugarClient db, IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository)
        {
            _logger = logger;
            _db = db;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var order = new Order
            {
                OrderNo = "ORDsjff789737389",
                OrderPrice = 299.99m,
                IsDeleted = false
            };
            await _orderRepository.InsertAsync(order);

            var orderItem1 = new OrderItem
            {
                OrderId = order.Id,
                ProductName = "抽纸",
                Unit = "抽",
                UnitPrice = 99.99m,
                Quantity = 10
            };
            var orderItem2 = new OrderItem
            {
                OrderId = order.Id,
                ProductName = "手机壳",
                Unit = "个",
                UnitPrice = 100.01m,
                Quantity = 2
            };
            await _orderItemRepository.InsertAsync(orderItem1);
            await _orderItemRepository.InsertAsync(orderItem2);

            /*
            SELECT DISTINCT o.*
            FROM tb_order o
            INNER JOIN tb_order_item i ON o.id = i.order_id
            where i.product_name like '%抽纸%'     
            order by o.id DESC
            LIMIT 10 OFFSET 20; -- 第3页（每页10条）
            //*/

            return Ok(new { order.Id, Message = "Order created successfully." });
        }
    }
}
