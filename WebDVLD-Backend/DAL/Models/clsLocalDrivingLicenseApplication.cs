using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsLocalDrivingLicenseApplication
{
    public int LocalDrivingLicenseApplicationId { get; set; }

    public int ApplicationId { get; set; }

    public int LicenseClassId { get; set; }

    public virtual clsApplication Application { get; set; } = null!;

    public virtual clsLicenseClass LicenseClass { get; set; } = null!;

    public virtual ICollection<clsTestAppointment> TestAppointments { get; set; } = new List<clsTestAppointment>();
}
