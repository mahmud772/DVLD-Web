using Microsoft.AspNetCore.Authorization;

namespace PL.Authorization
{
    public class UserOwnerOrAdminRequirement : IAuthorizationRequirement
    {
    }
}
