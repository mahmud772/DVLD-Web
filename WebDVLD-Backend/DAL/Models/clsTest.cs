using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsTest
{
    public int TestId { get; set; }

    public int TestAppointmentId { get; set; }

    /// <summary>
    /// 0 - Fail 1-Pass
    /// </summary>
    public bool TestResult { get; set; }

    public string? Notes { get; set; }

    public int CreatedByUserId { get; set; }

    public virtual clsUser CreatedByUser { get; set; } = null!;

    public virtual clsTestAppointment TestAppointment { get; set; } = null!;
}
