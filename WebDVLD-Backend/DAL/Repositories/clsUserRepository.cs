using DAL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class clsUserRepository : IUserRepository
    {
        private readonly DVLDContext _Context;

        public clsUserRepository(DVLDContext Context)
        {
            _Context = Context;
        }

        public async Task<clsUserAuthDTO?> GetByUserNameAsync(string UserName)
        {
            return await _Context.Users
                .AsNoTracking()
                .Where(U => U.UserName == UserName)
                .Select(U => new clsUserAuthDTO
                {
                    UserId = U.UserId,
                    UserName = U.UserName,
                    Password = U.Password,
                    IsActive = U.IsActive,
                    Permissions = U.Permissions
                })
                .FirstOrDefaultAsync();
        }

        public async Task<clsUserAuthDTO?> GetByIdAsync(int UserId)
        {
            return await _Context.Users
                .AsNoTracking()
                .Where(U => U.UserId == UserId)
                .Select(U => new clsUserAuthDTO
                {
                    UserId = U.UserId,
                    UserName = U.UserName,
                    Password = U.Password,
                    IsActive = U.IsActive,
                    Permissions = U.Permissions
                })
                .FirstOrDefaultAsync();
        }
    }
}
