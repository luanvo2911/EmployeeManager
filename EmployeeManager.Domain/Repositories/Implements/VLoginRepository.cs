using EmployeeManager.Base.Implementation;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Common.Encryption;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManager.Entity.Entities;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Domain.Repositories.Implements
{
    public class VLoginRepository : BaseRepository<Vlogin>, IVLoginRepository
    {
        private readonly EmployeeManagerContext _dbContext;

        public VLoginRepository(EmployeeManagerContext dbContext, ILogger<Employee> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
        }

        public Vlogin? GetLoginUser(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Username and Password cannot be null or empty");
            }
            else
            {
                Vlogin? loginInfo = _dbContext.Vlogins.FirstOrDefault(v => v.Username == username);
                if (loginInfo == null || !Encryption.VerifyPassword(password, loginInfo.Password)) {
                    return null;
                }
                else
                {
                    return loginInfo;
                }
            }
            

        }
    }
}
