using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsDriver
{
    public int DriverId { get; set; }

    public int PersonId { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual clsUser CreatedByUser { get; set; } = null!;

    public virtual ICollection<clsInternationalLicense> InternationalLicenses { get; set; } = new List<clsInternationalLicense>();

    public virtual ICollection<clsLicense> Licenses { get; set; } = new List<clsLicense>();

    public virtual clsPerson Person { get; set; } = null!;
}
