using PaginationApi.Models;
using PaginationApi.Repositories;

namespace PaginationApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public PagedResult<Employee> GetEmployees(int pageNumber, int pageSize)
        {
            // Business Rules / Validations

            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize <= 0)
                pageSize = 10;

            if (pageSize > 50)
                pageSize = 50;

            var result = _employeeRepository.GetEmployees(pageNumber, pageSize);

            return result;
        }
    }
}