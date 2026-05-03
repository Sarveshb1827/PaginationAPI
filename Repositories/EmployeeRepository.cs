using System.Data;
using Microsoft.Data.SqlClient;
using PaginationApi.Models;

namespace PaginationApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public PagedResult<Employee> GetEmployees(int pageNumber, int pageSize)
        {
            PagedResult<Employee> result = new PagedResult<Employee>();
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetEmployeesPagination", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // First Result Set
                        while (reader.Read())
                        {
                            Employee emp = new Employee
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FullName = reader["FullName"].ToString(),
                                Department = reader["Department"].ToString(),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                City = reader["City"].ToString()
                            };

                            employees.Add(emp);
                        }

                        // Second Result Set
                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                result.TotalRecords = Convert.ToInt32(reader["TotalRecords"]);
                            }
                        }
                    }
                }
            }

            result.Data = employees;
            result.PageNumber = pageNumber;
            result.PageSize = pageSize;
            result.TotalPages = (int)Math.Ceiling((double)result.TotalRecords / pageSize);

            return result;
        }
    }
}