using System;
using System.Security.Claims;
using MarathonApp.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MarathonApp.BLL.Policies
{
    public class UserPolicyHandler : AuthorizationHandler<UserPolicy>
    {
        private UserManager<User> _userManager;

        public UserPolicyHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserPolicy requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email))
                return;

            var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email);
            var claims = new List<Claim>(context.User.FindAll(c => c.Type == ClaimTypes.Role));
            var user =  await _userManager.FindByEmailAsync(email.Value);
            bool isNew = user.NewUser;

            if (isNew == requirement.IsNew)
            {
                foreach (var claim in claims)
                {
                    if (claim.Value == "User")
                    {
                        user.NewUser = false;
                        await _userManager.UpdateAsync(user);
                    }

                }
                context.Succeed(requirement);
            }
            return;
        }
    }
}

