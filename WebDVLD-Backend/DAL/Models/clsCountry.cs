using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsCountry
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string? CountryNameArabic { get; set; }

    public virtual ICollection<clsPerson> People { get; set; } = new List<clsPerson>();
}
