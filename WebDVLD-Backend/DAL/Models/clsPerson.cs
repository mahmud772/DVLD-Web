using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class clsPerson
{
    public int PersonId { get; set; }

    public string NationalNo { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string ThirdName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// 0 Male , 1 Femail
    /// </summary>
    public byte Gendor { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public int NationalityCountryId { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<clsApplication> Applications { get; set; } = new List<clsApplication>();

    public virtual ICollection<clsDriver> Drivers { get; set; } = new List<clsDriver>();

    public virtual clsCountry NationalityCountry { get; set; } = null!;

    public virtual ICollection<clsUser> Users { get; set; } = new List<clsUser>();
}
