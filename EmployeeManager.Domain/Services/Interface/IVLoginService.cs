using EmployeeManager.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Services.Interface
{
    public interface IVLoginService
    {
        Vlogin? Login(string username, string password);
    }
}
