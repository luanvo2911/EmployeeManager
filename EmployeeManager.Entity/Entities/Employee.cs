using System;
using System.Collections.Generic;

namespace EmployeeManager.Entity.Entities;

public partial class Employee
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Department { get; set; }

    public string? Position { get; set; }

    public DateOnly? HireDate { get; set; }

    public decimal? Salary { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
