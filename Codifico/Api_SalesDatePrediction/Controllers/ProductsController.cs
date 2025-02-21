using Api_SalesDatePrediction.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetShippers()
        {
            try
            {
                var result = await _productRepository.GetProductsAsync();
                if (result == null)
                {
                    return NotFound("No orders found for this shippers.");
                }

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
