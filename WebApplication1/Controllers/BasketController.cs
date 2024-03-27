using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("change-amount")]
        public IActionResult ChangeAmountOfProductsInBasket(int basketPositionId, int amount)
        {
            var result = _basketService.ChangeAmountOfProductsInBasket(basketPositionId, amount);
            if (result)
            {
                return Ok("Amount changed successfully.");
            }
            else
            {
                return BadRequest("Failed to change amount.");
            }
        }

        [HttpDelete("delete/{basketPositionId}")]
        public IActionResult DeleteBasketPosition(int basketPositionId)
        {
            var result = _basketService.DeleteBasketPosition(basketPositionId);
            if (result)
            {
                return Ok("Basket position deleted successfully.");
            }
            else
            {
                return BadRequest("Failed to delete basket position.");
            }
        }

        [HttpPost("add")]
        public IActionResult GenerateBasketPosition(BasketAddRequestDTO basketDTO)
        {
            var result = _basketService.GenerateBasketPosition(basketDTO);
            if (result)
            {
                return Ok("Basket position generated successfully.");
            }
            else
            {
                return BadRequest("Failed to generate basket position.");
            }
        }
    }
}
