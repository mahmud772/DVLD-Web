using BLL.DTOs.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILicenseService
    {
        Task<clsVerifyLicenseDTO?> VerifyLicenseAsync(string NationalNo,int LicenseID);
    }
}
