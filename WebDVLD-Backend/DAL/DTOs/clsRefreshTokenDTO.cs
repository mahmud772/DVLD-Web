using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class clsRefreshTokenDTO
    {
        public int UserId { get; set; }

        public DateTime ExpiresAtUtc { get; set; }

        public bool IsRevoked { get; set; }
    }
}
