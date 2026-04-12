using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Services.Implements
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee?> GetEmployees()
        {
            return _employeeRepository.GetAll().Result.ToList();
        }

        public Employee? GetEmployee(long id)
        {
            return _employeeRepository.GetById(id).Result;
        }

        public void InsertEmployee(Employee employee)
        {
            _employeeRepository.InsertAsync(employee).Wait();
        }

        public void UpdateEmployee(Employee employee, long id)
        {
            Employee? existingEmployee = _employeeRepository.GetById(id).Result;
            if (existingEmployee == null) {
                throw new Exception("Employee that need to be updated does not exist");
            }
            else
            {
                _employeeRepository.UpdateAsync(existingEmployee, employee).Wait();
            }
        }

        public void DeleteEmployee(long id)
        {
            Employee? deletedEmployee = _employeeRepository.GetById(id).Result;
            if (deletedEmployee == null)
            {
                throw new Exception("Employee that need to be updated does not exist");
            }
            else
            {
                _employeeRepository.DeleteAsync(deletedEmployee).Wait();
            }
        }
    }
}
