using BLL.DTOs.Licenses;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ILicenseRepository
    {
        Task<clsLicense?> GetByIdAsync(int LicenseID);

        Task<clsVerifyLicenseDTO?> GetByIdAndNationalNoAsync(int LicenseID , string NationalNo);

        Task<int> AddAsync(clsLicense LicenseID);

        Task<bool> UpdateAsync(clsLicense LicenseID);
        Task<bool> IsLicenseDetainedAsync(int LicenseID);
    }
}
