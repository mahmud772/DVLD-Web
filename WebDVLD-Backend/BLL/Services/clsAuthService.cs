using BLL.DTOs.Auth;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class clsAuthService : IAuthService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IAuthRepository _AuthRepository;
        private readonly ITokenService _TokenService;

        public clsAuthService(
            IUserRepository UserRepository,
            IAuthRepository AuthRepository,
            ITokenService TokenService)
        {
            _UserRepository = UserRepository;
            _AuthRepository = AuthRepository;
            _TokenService = TokenService;
        }

        public async Task<clsTokenResponse?> LoginAsync(clsLoginRequest Request)
        {
            var User = await _UserRepository.GetByUserNameAsync(Request.UserName);

            if (User == null)
                return null;

            if (!User.IsActive)
                return null;

            // ملاحظة أمنية مهمة: عمود كلمة المرور في قاعدة بيانات DVLD الحالية
            // (Password nvarchar(20)) يخزّن كلمة المرور كنص صريح غير مُشفّر،
            // ولا يوجد عمود PasswordHash. بما أن المطلوب هو إضافة JWT فقط دون
            // تغيير باقي الأمور (بما فيها هيكل قاعدة البيانات)، تم الإبقاء على
            // نفس آلية المقارنة الحالية للمحافظة على توافقية البيانات الموجودة.
            // يُنصح مستقبلاً بترقية العمود إلى Hash (مثل BCrypt) عند الاستعداد
            // لتعديل هيكل قاعدة البيانات.
            if (User.Password != Request.Password)
                return null;

            return await IssueTokensAsync(User.UserId, User.UserName, User.Permissions, User.IsActive);
        }

        public async Task<clsTokenResponse?> RefreshTokenAsync(clsRefreshRequest Request)
        {
            var StoredToken = await _AuthRepository.GetRefreshTokenAsync(Request.RefreshToken);

            if (StoredToken == null)
                return null;

            if (StoredToken.IsRevoked)
                return null;

            if (StoredToken.ExpiresAtUtc <= DateTime.UtcNow)
                return null;

            var User = await _UserRepository.GetByIdAsync(StoredToken.UserId);

            if (User == null || !User.IsActive)
                return null;

            // تدوير الـ Refresh Token (Rotation): إلغاء القديم فور استخدامه
            // ومنح واحد جديد، لتقليل أثر أي تسريب محتمل له.
            await _AuthRepository.RevokeRefreshTokenAsync(Request.RefreshToken);

            return await IssueTokensAsync(User.UserId, User.UserName, User.Permissions, User.IsActive);
        }

        public async Task<bool> LogoutAsync(clsLogoutRequest Request)
        {
            return await _AuthRepository.RevokeRefreshTokenAsync(Request.RefreshToken);
        }

        private async Task<clsTokenResponse> IssueTokensAsync(
            int UserId, string UserName, int Permissions, bool IsActive)
        {
            var UserForToken = new DAL.DTOs.clsUserAuthDTO
            {
                UserId = UserId,
                UserName = UserName,
                Permissions = Permissions,
                IsActive = IsActive
            };

            var AccessToken = _TokenService.GenerateAccessToken(UserForToken);
            var RefreshToken = _TokenService.GenerateRefreshToken();

            var RefreshTokenExpiresAtUtc =
                DateTime.UtcNow.AddDays(_TokenService.RefreshTokenExpirationDays);

            await _AuthRepository.SaveRefreshTokenAsync(
                UserId, RefreshToken, RefreshTokenExpiresAtUtc);

            return new clsTokenResponse
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                TokenType = "Bearer",
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_TokenService.AccessTokenExpirationMinutes)
            };
        }
    }
}
