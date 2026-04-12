using EmployeeManager.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManager.Entity.Entities;

namespace EmployeeManager.Domain.Repositories.Interface
{
    public interface IVLoginRepository : IBaseRepository<Vlogin>
    {
        Vlogin? GetLoginUser(string username, string encryptedPassword);
    }
}
