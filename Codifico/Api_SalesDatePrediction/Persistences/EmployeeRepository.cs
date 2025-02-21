using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api_SalesDatePrediction.Persistences
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public EmployeeRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<EmployeesDto>> Get_EmployeesAsync()
        {
            return await _context.Employees
                .OrderBy(e => e.Empid) // Ordena de Z a A
                .Select(e => new EmployeesDto
                {
                    Empid = e.Empid,
                    Lastname = e.Lastname,
                    Firstname = e.Firstname
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeesDto>> GetEmployeesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = "SELECT Empid, Firstname + ' ' + Lastname AS FullName FROM HR.Employees";
            return await connection.QueryAsync<EmployeesDto>(sql);
        }
    }
}
