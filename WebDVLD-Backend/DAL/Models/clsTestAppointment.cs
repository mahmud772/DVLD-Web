using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsTestAppointment
{
    public int TestAppointmentId { get; set; }

    public int TestTypeId { get; set; }

    public int LocalDrivingLicenseApplicationId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public decimal PaidFees { get; set; }

    public int CreatedByUserId { get; set; }

    public bool IsLocked { get; set; }

    public int? RetakeTestApplicationId { get; set; }

    public virtual clsUser CreatedByUser { get; set; } = null!;

    public virtual clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; } = null!;

    public virtual clsApplication? RetakeTestApplication { get; set; }

    public virtual clsTestType TestType { get; set; } = null!;

    public virtual ICollection<clsTest> Tests { get; set; } = new List<clsTest>();
}
