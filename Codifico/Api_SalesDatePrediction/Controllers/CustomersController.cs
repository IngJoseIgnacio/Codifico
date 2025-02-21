using System.Collections.Generic;
using System.Threading.Tasks;
using Api_SalesDatePrediction.Interfaces;
using Api_SalesDatePrediction.Models;
using Api_SalesDatePrediction.Persistences;
using Microsoft.AspNetCore.Mvc;

namespace Api_SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var result = await _customerRepository.GetListCustomerbyOrdenAsync();
                if (result == null)
                {
                    return NotFound("No orders found for this customers.");
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
