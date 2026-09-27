using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsApplication
{
    public int ApplicationId { get; set; }

    public int ApplicantPersonId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public int ApplicationTypeId { get; set; }

    /// <summary>
    /// 1-New 2-Cancelled 3-Completed
    /// </summary>
    public byte ApplicationStatus { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    public int CreatedByUserId { get; set; }

    public virtual clsPerson ApplicantPerson { get; set; } = null!;

    public virtual clsApplicationType ApplicationType { get; set; } = null!;

    public virtual clsUser CreatedByUser { get; set; } = null!;

    public virtual ICollection<clsDetainedLicense> DetainedLicenses { get; set; } = new List<clsDetainedLicense>();

    public virtual ICollection<clsInternationalLicense> InternationalLicenses { get; set; } = new List<clsInternationalLicense>();

    public virtual ICollection<clsLicense> Licenses { get; set; } = new List<clsLicense>();

    public virtual ICollection<clsLocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; } = new List<clsLocalDrivingLicenseApplication>();

    public virtual ICollection<clsTestAppointment> TestAppointments { get; set; } = new List<clsTestAppointment>();
}
