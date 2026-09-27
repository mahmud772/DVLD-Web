using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<clsUserAuthDTO?> GetByUserNameAsync(string UserName);

        Task<clsUserAuthDTO?> GetByIdAsync(int UserId);
    }
}
