using Api_SalesDatePrediction.Interfaces;
using Api_SalesDatePrediction.Persistences;
using Microsoft.AspNetCore.Mvc;

namespace Api_SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : Controller
    {
        private readonly IShipperRepository _shipperRepository;

        public ShippersController(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetShippers()
        {
            try
            {
                var result = await _shipperRepository.GetShippersAsync();
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
