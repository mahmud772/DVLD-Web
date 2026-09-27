using BLL.Interfaces;
using DAL.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class clsTokenService : ITokenService
    {
        private readonly string _Issuer;
        private readonly string _Audience;
        private readonly string _SecretKey;

        public int AccessTokenExpirationMinutes { get; }

        public int RefreshTokenExpirationDays { get; }

        public clsTokenService(IConfiguration Configuration)
        {
            var JwtSettings = Configuration.GetSection("JwtSettings");

            _Issuer = JwtSettings["Issuer"]
                ?? throw new InvalidOperationException("JwtSettings:Issuer Is Not Found In appsettings.json");

            _Audience = JwtSettings["Audience"]
                ?? throw new InvalidOperationException("JwtSettings:Audience Is Not Found In appsettings.json");

            _SecretKey = JwtSettings["SecretKey"]
                ?? throw new InvalidOperationException("JwtSettings:SecretKey Is Not Found In appsettings.json");

            AccessTokenExpirationMinutes =
                int.TryParse(JwtSettings["AccessTokenExpirationMinutes"], out var AccessMinutes)
                    ? AccessMinutes
                    : 15;

            RefreshTokenExpirationDays =
                int.TryParse(JwtSettings["RefreshTokenExpirationDays"], out var RefreshDays)
                    ? RefreshDays
                    : 7;
        }

        public string GenerateAccessToken(clsUserAuthDTO User)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, User.UserId.ToString()),
                new Claim(ClaimTypes.Name, User.UserName),
                new Claim("Permissions", User.Permissions.ToString())
            };

            Claims.Add(new Claim(ClaimTypes.Role, User.Permissions == -1 ? "Admin" : "User"));

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_SecretKey));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _Issuer,
                audience: _Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddMinutes(AccessTokenExpirationMinutes),
                signingCredentials: Credentials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public string GenerateRefreshToken()
        {
            var Bytes = new byte[64];
            using var Rng = RandomNumberGenerator.Create();
            Rng.GetBytes(Bytes);
            return Convert.ToBase64String(Bytes);
        }
    }
}
