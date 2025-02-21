using Api_SalesDatePrediction.Dtos;

namespace Api_SalesDatePrediction.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeesDto>> GetEmployeesAsync();
        Task<List<EmployeesDto>> Get_EmployeesAsync();
    }
}
