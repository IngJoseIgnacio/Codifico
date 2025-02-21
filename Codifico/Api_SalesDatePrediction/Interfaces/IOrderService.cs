using Api_SalesDatePrediction.Dtos;

namespace Api_SalesDatePrediction.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrdersDto>> GetClientOrdersAsync(int custId);
    }
}
