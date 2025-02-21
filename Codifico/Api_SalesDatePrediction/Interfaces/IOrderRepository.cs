using System.Collections.Generic;
using System.Threading.Tasks;
using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Models;

namespace Api_SalesDatePrediction.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrdersDto>> GetOrdersByCustomerIdAsync(int customerId);
        Task<IEnumerable<OrdersCustomersDto>> GetOrdersByCustomerId(int custId);
        Task<int> CreateOrderAsync(OrderDto order);
    }
}