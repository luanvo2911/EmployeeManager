using System;
using System.Collections.Generic;

namespace EmployeeManager.Entity.Entities;

public partial class Vlogin
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;
}
