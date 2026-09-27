using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsApplicationType
{
    public int ApplicationTypeId { get; set; }

    public string ApplicationTypeTitle { get; set; } = null!;

    public decimal ApplicationFees { get; set; }

    public string? ApplicationTypeTitleArabic { get; set; }

    public virtual ICollection<clsApplication> Applications { get; set; } = new List<clsApplication>();
}
