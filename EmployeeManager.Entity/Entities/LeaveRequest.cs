using System;
using System.Collections.Generic;

namespace EmployeeManager.Entity.Entities;

public partial class LeaveRequest
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
