using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PL.Authorization
{
    public class UserOwnerOrAdminHandler
        : AuthorizationHandler<UserOwnerOrAdminRequirement, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UserOwnerOrAdminRequirement requirement,int UserId)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            
            var UserIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(UserIdClaim, out int AuthenticatedUserId) &&
                AuthenticatedUserId == UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
