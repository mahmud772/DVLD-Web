using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsLicenseClass
{
    public int LicenseClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public string ClassDescription { get; set; } = null!;

    /// <summary>
    /// Minmum age allowed to apply for this license
    /// </summary>
    public byte MinimumAllowedAge { get; set; }

    /// <summary>
    /// How many years the licesnse will be valid.
    /// </summary>
    public byte DefaultValidityLength { get; set; }

    public decimal ClassFees { get; set; }

    public string? ClassNameArabic { get; set; }

    public virtual ICollection<clsLicense> Licenses { get; set; } = new List<clsLicense>();

    public virtual ICollection<clsLocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; } = new List<clsLocalDrivingLicenseApplication>();
}
