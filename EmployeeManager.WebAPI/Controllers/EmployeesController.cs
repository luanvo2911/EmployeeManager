using AutoMapper;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.WebAPI.Model;
using EmployeeManager.Entity.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _automapper;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService employeeService, IMapper automapper, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _automapper = automapper;
            _logger = logger;
            //_logger = logger;
        }

        [Authorize]
        [HttpGet("employees")]
        public IActionResult GetEmployees()
        {
            _logger.LogInformation("Getting all employees");
            EmployeeModel[] employees = _automapper.Map<EmployeeModel[]>(_employeeService.GetEmployees());
            return Ok(employees);
        }

        [Authorize]
        [HttpGet("employee")]
        public IActionResult GetEmployee([FromQuery] long id)
        {
            EmployeeModel employee = _automapper.Map<EmployeeModel>(_employeeService.GetEmployee(id));

            return Ok(employee);
        }

        [Authorize(Roles = "ADMIN, HR")]
        [HttpPost]
        public IActionResult Insert([FromBody]Employee? employee)
        {
            if(employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            else
            {
                _employeeService.InsertEmployee(employee);

                return Ok("New employee has been added");
            }
            
        }

        [Authorize(Roles = "ADMIN, HR")]
        [HttpPut]
        public IActionResult Update([FromBody] Employee? employee)
        {
            if(employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            else
            {
                long id = employee.Id;
                _employeeService.UpdateEmployee(employee, id);
                return Ok($"Update employee {id} successfully");
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete]
        public IActionResult Delete([FromQuery]long id)
        {
            _employeeService.DeleteEmployee(id);

            return Ok($"Employee with id {id} is deleted");
        }

    }
}
