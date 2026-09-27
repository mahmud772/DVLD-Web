using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsDetainedLicense
{
    public int DetainId { get; set; }

    public int LicenseId { get; set; }

    public DateTime DetainDate { get; set; }

    public decimal FineFees { get; set; }

    public int CreatedByUserId { get; set; }

    public bool IsReleased { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? ReleasedByUserId { get; set; }

    public int? ReleaseApplicationId { get; set; }

    public virtual clsUser CreatedByUser { get; set; } = null!;

    public virtual clsLicense License { get; set; } = null!;

    public virtual clsApplication? ReleaseApplication { get; set; }

    public virtual clsUser? ReleasedByUser { get; set; }
}
