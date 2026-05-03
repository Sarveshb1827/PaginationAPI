using PaginationApi.Models;

namespace PaginationApi.Repositories
{
    public interface IEmployeeRepository
    {
        PagedResult<Employee> GetEmployees(int pageNumber, int pageSize);
    }
}