using Api_SalesDatePrediction.Dtos;

namespace Api_SalesDatePrediction.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductsDto>> GetProductsAsync();
    }
}
