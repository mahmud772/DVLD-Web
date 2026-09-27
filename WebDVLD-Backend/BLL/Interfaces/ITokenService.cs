using DAL.DTOs;
using System;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(clsUserAuthDTO User);

        string GenerateRefreshToken();

        int AccessTokenExpirationMinutes { get; }

        int RefreshTokenExpirationDays { get; }
    }
}
