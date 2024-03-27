using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            this._productsService = productsService;
        }

        [HttpPost("activate/{id}")]
        public IActionResult ActivateProduct(int id)
        {
            var success = _productsService.ActivateProduct(id);
            if (success)
                return Ok("Product activated successfully.");
            else
                return BadRequest("Failed to activate product.");
        }

        [HttpPost("add")]
        public IActionResult AddNewProduct([FromBody] ProductAddRequestDTO productDTO)
        {
            var success = _productsService.AddNewProduct(productDTO);
            if (success)
                return Ok("Product added successfully.");
            else
                return BadRequest("Failed to add product.");
        }

        [HttpPost("deactivate/{id}")]
        public IActionResult DeactivateProduct(int id)
        {
            var success = _productsService.DeactivateProduct(id);
            if (success)
                return Ok("Product deactivated successfully.");
            else
                return BadRequest("Failed to deactivate product.");
        }

        [HttpPost("list")]
        public IActionResult GetProductsList([FromBody] ProductListRequestDTO productDTO)
        {
            var products = _productsService.GetProductsList(productDTO);
            return Ok(products);
        }
    }
}
