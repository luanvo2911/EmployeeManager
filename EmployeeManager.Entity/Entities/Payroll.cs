using System;
using System.Collections.Generic;

namespace EmployeeManager.Entity.Entities;

public partial class Payroll
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public DateOnly PayDate { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? Deductions { get; set; }

    public decimal? NetSalary { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
