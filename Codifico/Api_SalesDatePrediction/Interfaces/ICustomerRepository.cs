using Api_SalesDatePrediction.Dtos;

namespace Api_SalesDatePrediction.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomersDto>> GetListCustomerbyOrdenAsync();
    }
}
