using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsLicense
{
    public int LicenseId { get; set; }

    public int ApplicationId { get; set; }

    public int DriverId { get; set; }

    public int LicenseClassId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string? Notes { get; set; }

    public decimal PaidFees { get; set; }

    public bool IsActive { get; set; }

    /// <summary>
    /// 1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.
    /// </summary>
    public byte IssueReason { get; set; }

    public int CreatedByUserId { get; set; }

    public virtual clsApplication Application { get; set; } = null!;

    public virtual clsUser CreatedByUser { get; set; } = null!;

    public virtual ICollection<clsDetainedLicense> DetainedLicenses { get; set; } = new List<clsDetainedLicense>();

    public virtual clsDriver Driver { get; set; } = null!;

    public virtual ICollection<clsInternationalLicense> InternationalLicenses { get; set; } = new List<clsInternationalLicense>();

    public virtual clsLicenseClass LicenseClass { get; set; } = null!;

    private DVLDContext _context { get; set; }
    public clsLicense(DVLDContext context) { _context = context; }
    
}
