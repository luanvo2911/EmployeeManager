using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Common.Encryption
{
    public class Encryption
    {
        // Hash the password
        public static string HashPassword(string password)
        {
            // 'WorkFactor' determines how slow the hash is. 
            // 12 is a good balance between security and performance.
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        // Verify the password
        public static bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
