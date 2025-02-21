using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;

namespace Api_SalesDatePrediction.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetClientOrdersAsync(int custId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(custId);
        }
    }
}
