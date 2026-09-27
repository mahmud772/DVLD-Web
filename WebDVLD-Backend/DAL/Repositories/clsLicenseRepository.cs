using BLL.DTOs.Licenses;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class clsLicenseRepository : ILicenseRepository
    {
        private readonly DVLDContext _Context;

        public clsLicenseRepository(DVLDContext Context)
        {
            _Context = Context;
        }

        public async Task<clsLicense?> GetByIdAsync(int LicenseID)
        {
            return await _Context.Licenses
                .FirstOrDefaultAsync(
                    L => L.LicenseId == LicenseID);
        }

        public async Task<List<clsLicense>> GetAllAsync()
        {
            return await _Context.Licenses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> AddAsync(clsLicense license)
        {
            _Context.Licenses.Add(license);

            await _Context.SaveChangesAsync();

            return license.LicenseId;
        }

        public async Task<bool> UpdateAsync(clsLicense license)
        {
            var ExistingLicense = await _Context.Licenses
                .FindAsync(license.ApplicationId);

            if (ExistingLicense == null)
                return false;

            

            ExistingLicense.PaidFees =
                license.PaidFees;

            await _Context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int LicenseID)
        {
            var License = await _Context.Licenses
                .FindAsync(LicenseID);

            if (License == null)
                return false;

            _Context.Licenses.Remove(License);

            await _Context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> IsLicenseDetainedAsync(int LicenseID)
        {
            bool IsDetainedBefore = await _Context.DetainedLicenses
                .AnyAsync(D => D.LicenseId == LicenseID);

            if (!IsDetainedBefore)
                return false;

            bool IsReleased = await _Context.DetainedLicenses
                .AnyAsync(D => D.LicenseId == LicenseID && D.IsReleased);

            return !IsReleased;
        }
        public async Task<clsVerifyLicenseDTO?> GetByIdAndNationalNoAsync(int LicenseID, string NationalNo)
        {
            var License = await _Context.Licenses
                .AsNoTracking()
                .Where(L =>
                    L.Driver.Person.NationalNo == NationalNo &&
                    L.LicenseId == LicenseID)
                .Select(L => new clsVerifyLicenseDTO
                {
                    ExpirationDate = L.ExpirationDate.ToString("yyyy-MM-dd"),
                    IsActive = L.IsActive,
                    LicenseClassTitel = L.LicenseClass.ClassNameArabic,
                })
                .FirstOrDefaultAsync();

            return License;
        }
    }
}
