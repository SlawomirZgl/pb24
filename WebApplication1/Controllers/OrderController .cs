using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateOrder(int userId)
        {
            var orderResponse = _orderService.GenerateOrder(userId);
            if (orderResponse != null && orderResponse.Id > 0)
            {
                return Ok(orderResponse);
            }
            else
            {
                return BadRequest("Failed to generate order.");
            }
        }

        [HttpPost("pay")]
        public IActionResult PayForOrder(double value)
        {
            var result = _orderService.PayForOrder(value);
            if (result)
            {
                return Ok("Order payment successful.");
            }
            else
            {
                return BadRequest("Failed to pay for order.");
            }
        }
    }
}
