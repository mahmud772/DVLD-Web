using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsInternationalLicense
{
    public int InternationalLicenseId { get; set; }

    public int ApplicationId { get; set; }

    public int DriverId { get; set; }

    public int IssuedUsingLocalLicenseId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsActive { get; set; }

    public int CreatedByUserId { get; set; }

    public virtual clsApplication Application { get; set; } = null!;

    public virtual clsUser CreatedByUser { get; set; } = null!;

    public virtual clsDriver Driver { get; set; } = null!;

    public virtual clsLicense IssuedUsingLocalLicense { get; set; } = null!;
}
