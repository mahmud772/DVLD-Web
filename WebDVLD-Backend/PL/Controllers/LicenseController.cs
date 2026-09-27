using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _LicenseService;

        public LicenseController(ILicenseService LicenseService)
        {
            _LicenseService = LicenseService;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyLicense([FromQuery] int LicenseID,[FromQuery] string NationalNo)
        {
            var Result =
                await _LicenseService.VerifyLicenseAsync(NationalNo , LicenseID);

            if (Result == null)
            {
                return NotFound(new
                {
                    Message = "License not found."
                });
            }

            return Ok(Result);
        }
    }

}
