using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAuthRepository
    {
        Task SaveRefreshTokenAsync(
            int UserId,
            string RefreshToken,
            DateTime ExpiresAtUtc);

        Task<clsRefreshTokenDTO?> GetRefreshTokenAsync(
            string RefreshToken);

        Task<bool> RevokeRefreshTokenAsync(
            string RefreshToken);
    }
}
