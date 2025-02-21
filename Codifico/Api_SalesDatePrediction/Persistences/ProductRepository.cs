using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api_SalesDatePrediction.Persistences
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public ProductRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<ProductsDto>> GetProductsAsync()
        {
            return await _context.products
                .OrderBy(e => e.Productid) // Ordena de Z a A
                .Select(e => new ProductsDto
                {
                    Productid = e.Productid,
                    Productname = e.Productname
                })
                .ToListAsync();
        }
    }
}
