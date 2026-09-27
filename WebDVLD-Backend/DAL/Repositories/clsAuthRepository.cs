using DAL.DTOs;
using DAL.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    
    public class clsAuthRepository : IAuthRepository
    {
        private static readonly ConcurrentDictionary<string, clsRefreshTokenDTO> _Store = new();

        public Task SaveRefreshTokenAsync(int UserId, string RefreshToken, DateTime ExpiresAtUtc)
        {
            var Key = Hash(RefreshToken);

            _Store[Key] = new clsRefreshTokenDTO
            {
                UserId = UserId,
                ExpiresAtUtc = ExpiresAtUtc,
                IsRevoked = false
            };

            return Task.CompletedTask;
        }

        public Task<clsRefreshTokenDTO?> GetRefreshTokenAsync(string RefreshToken)
        {
            var Key = Hash(RefreshToken);

            _Store.TryGetValue(Key, out var Result);

            return Task.FromResult(Result);
        }

        public Task<bool> RevokeRefreshTokenAsync(string RefreshToken)
        {
            var Key = Hash(RefreshToken);

            if (!_Store.TryGetValue(Key, out var Existing))
                return Task.FromResult(false);

            Existing.IsRevoked = true;

            return Task.FromResult(true);
        }

        private static string Hash(string Token)
        {
            var Bytes = SHA256.HashData(Encoding.UTF8.GetBytes(Token));
            return Convert.ToHexString(Bytes);
        }
    }
}
