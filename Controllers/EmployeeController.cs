using Microsoft.AspNetCore.Mvc;
using PaginationApi.Models;
using PaginationApi.Services;

namespace PaginationApi.Controllers
{
   
    [Route("api/Employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        string a = "";
        private readonly IEmployeeService _employeeService;
        //private readonly RequestParams _requestParams;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            //_requestParams = requestParams;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult GetEmployees([FromQuery] RequestParams requestParams)
        {
            var result = _employeeService.GetEmployees(requestParams.PageNumber,requestParams.PageSize);
            return Ok(result);
        }
    }
}