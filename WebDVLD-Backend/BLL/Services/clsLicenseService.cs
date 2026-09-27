using BLL.DTOs.Licenses;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class clsLicenseService : ILicenseService
    {
        private readonly ILicenseRepository _LicenseRepository;

        public clsLicenseService(ILicenseRepository LicenseRepository)
        {
            _LicenseRepository = LicenseRepository;
        }

        public async Task<clsVerifyLicenseDTO?> VerifyLicenseAsync(string NationalNo ,int LicenseID)
        {
            var License =
                await _LicenseRepository.GetByIdAndNationalNoAsync(
                    LicenseID,
                    NationalNo);

            if (License == null)
                return null;

            License.IsDetained =
                await _LicenseRepository.IsLicenseDetainedAsync(LicenseID);

            return License;
        }
    }
}
