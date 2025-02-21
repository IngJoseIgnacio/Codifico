using Api_SalesDatePrediction.Dtos;

namespace Api_SalesDatePrediction.Interfaces
{
    public interface IShipperRepository
    {
        Task<List<ShippersDto>> GetShippersAsync();
    }
}
