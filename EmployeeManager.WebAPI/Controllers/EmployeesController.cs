using AutoMapper;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.WebAPI.Model;
using EmployeeManager.Entity.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeManager.Common.ResourceUtils;

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
            try
            {
                _logger.LogInformation("Getting all employees");
                EmployeeModel[] employees = _automapper.Map<EmployeeModel[]>(_employeeService.GetEmployees());
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all employees");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("employee")]
        public IActionResult GetEmployee([FromQuery] long id)
        {
            try
            {
                EmployeeModel employee = _automapper.Map<EmployeeModel>(_employeeService.GetEmployee(id));
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN, HR")]
        [HttpPost]
        public IActionResult Insert([FromBody]Employee? employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee));
                }
                else
                {
                    _employeeService.InsertEmployee(employee);

                    return Ok(Message.RES0001);
                }
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize(Roles = "ADMIN, HR")]
        [HttpPut]
        public IActionResult Update([FromBody] Employee? employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee));
                }
                else
                {
                    long id = employee.Id;
                    _employeeService.UpdateEmployee(employee, id);
                    return Ok(string.Format(Message.RES0002, id));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete]
        public IActionResult Delete([FromQuery]long id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return Ok(string.Format(Message.RES0003, id));
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

    }
}
