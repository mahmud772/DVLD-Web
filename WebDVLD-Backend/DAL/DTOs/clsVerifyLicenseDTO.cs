using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Licenses
{
    public class clsVerifyLicenseDTO
    {
        
        public string LicenseClassTitel { get; set; }

        public string ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDetained { get; set; }
    }
}
