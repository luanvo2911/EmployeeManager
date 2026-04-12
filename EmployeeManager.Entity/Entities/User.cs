using System;
using System.Collections.Generic;

namespace EmployeeManager.Entity.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
