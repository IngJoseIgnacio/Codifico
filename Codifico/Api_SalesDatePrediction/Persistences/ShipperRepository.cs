using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api_SalesDatePrediction.Persistences
{
    public class ShipperRepository: IShipperRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public ShipperRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<ShippersDto>> GetShippersAsync()
        {
            return await _context.Shippers
                .OrderBy(e => e.Shipperid) // Ordena de Z a A
                .Select(e => new ShippersDto
                {
                    Shipperid = e.Shipperid,
                    Companyname = e.Companyname
                })
                .ToListAsync();
        }
    }
}
