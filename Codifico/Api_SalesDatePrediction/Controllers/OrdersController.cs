using System.Collections.Generic;
using System.Threading.Tasks;
using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;
using Api_SalesDatePrediction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrders(int customerId)
        {
            try
            {
                var result = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
                if (result == null)
                {
                    return NotFound("No orders found for this customer.");
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

        [HttpGet("customer/{custId}/details")]
        public async Task<IActionResult> GetOrdersWithCustomerDetails(int custId)
        {
            try
            {
                var result = await _orderRepository.GetOrdersByCustomerId(custId);
                if (result == null)
                {
                    return NotFound("No orders found for this customer.");
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
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (orderDto == null || orderDto.OrderDetails == null || orderDto.OrderDetails.Count == 0)
            {
                return BadRequest(new { Message = "Invalid order data." });
            }

            try
            {
                int newOrderId = await _orderRepository.CreateOrderAsync(orderDto);
                return Ok(new { Message = "Order created successfully!", OrderID = newOrderId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
            }
        }
    }
}