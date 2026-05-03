using PaginationApi.Models;

namespace PaginationApi.Services
{
    public interface IEmployeeService
    {
        PagedResult<Employee> GetEmployees(int pageNumber, int pageSize);
    }
}