using BLL.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<clsTokenResponse?> LoginAsync(clsLoginRequest Request);

        Task<clsTokenResponse?> RefreshTokenAsync(clsRefreshRequest Request);

        Task<bool> LogoutAsync(clsLogoutRequest Request);
    }
}
