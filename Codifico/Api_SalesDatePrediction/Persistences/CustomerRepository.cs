using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;
using Api_SalesDatePrediction.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace Api_SalesDatePrediction.Persistences
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public CustomerRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<CustomersDto>> GetListCustomerbyOrdenAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    c.CompanyName as CustomerName,
                    MAX(o.OrderDate) AS LastOrderDate,
                    CASE 
                        WHEN MAX(o.OrderDate) IS NOT NULL 
                        THEN DATEADD(DAY, 30, MAX(o.OrderDate))
                        ELSE NULL 
                    END AS PossibleNextOrderDate
                FROM Sales.Customers c
                LEFT JOIN Sales.Orders o ON c.[custid] = o.[custid]
                GROUP BY c.[custid], c.CompanyName;";

            return await connection.QueryAsync<CustomersDto>(query);

        /*return await _context.Customers
            .Select(c => new CustomersDto
            {
                CustomerName = c.Companyname,
                LastOrderdate = (DateTime)_context.Orders
                    .Where(o => o.Custid == c.Custid)
                    .Max(o => (DateTime?)o.Orderdate), // Maneja valores nulos correctamente
                NextPredictedOrder = (DateTime)(_context.Orders
                    .Where(o => o.Custid == c.Custid)
                    .Max(o => (DateTime?)o.Orderdate) != null
                    ? _context.Orders.Where(o => o.Custid == c.Custid)
                        .Max(o => (DateTime?)o.Orderdate).Value.AddDays(30)
                    : (DateTime?)null) // Evita el error si no hay órdenes
            })
            .ToListAsync();*/
    }

}
}
