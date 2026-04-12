using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManager.Base.Implementation;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Entity.Entities;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Domain.Repositories.Implements
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeManagerContext dbContext, ILogger<Employee> logger) : base(dbContext, logger)
        {
        }
    }
}
