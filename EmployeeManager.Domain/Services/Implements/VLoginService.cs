using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Entity.Entities;
using EmployeeManager.Common.Encryption;

namespace EmployeeManager.Domain.Services.Implements
{
    public class VLoginService : IVLoginService
    {
        private readonly IVLoginRepository _vLoginRepository;
        public VLoginService(IVLoginRepository vLoginRepository) { 
            _vLoginRepository = vLoginRepository;
        }

        public Vlogin? Login (string username, string password)
        {
            Vlogin? authenticatedUser = _vLoginRepository.GetLoginUser(username, password);

            return authenticatedUser;

        }
    }
}
