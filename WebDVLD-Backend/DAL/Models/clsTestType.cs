using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsTestType
{
    public int TestTypeId { get; set; }

    public string TestTypeTitle { get; set; } = null!;

    public string TestTypeDescription { get; set; } = null!;

    public decimal TestTypeFees { get; set; }

    public string? TestTypeTitleArabic { get; set; }

    public virtual ICollection<clsTestAppointment> TestAppointments { get; set; } = new List<clsTestAppointment>();
}
