using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsUser
{
    public int UserId { get; set; }

    public int PersonId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public int Permissions { get; set; }

    public virtual ICollection<clsApplication> Applications { get; set; } = new List<clsApplication>();

    public virtual ICollection<clsDetainedLicense> DetainedLicenseCreatedByUsers { get; set; } = new List<clsDetainedLicense>();

    public virtual ICollection<clsDetainedLicense> DetainedLicenseReleasedByUsers { get; set; } = new List<clsDetainedLicense>();

    public virtual ICollection<clsDriver> Drivers { get; set; } = new List<clsDriver>();

    public virtual ICollection<clsInternationalLicense> InternationalLicenses { get; set; } = new List<clsInternationalLicense>();

    public virtual ICollection<clsLicense> Licenses { get; set; } = new List<clsLicense>();

    public virtual clsPerson Person { get; set; } = null!;

    public virtual ICollection<clsTestAppointment> TestAppointments { get; set; } = new List<clsTestAppointment>();

    public virtual ICollection<clsTest> Tests { get; set; } = new List<clsTest>();
}
