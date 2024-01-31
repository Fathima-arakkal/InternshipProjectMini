using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InternshipProjectMini.Models
{
    public class UserClaimsTransformation : IClaimsTransformation
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserClaimsTransformation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(principal);
                if (user != null)
                {
                    // Add user claims to principal
                    var claims = await _userManager.GetClaimsAsync(user);
                    ((ClaimsIdentity)principal.Identity).AddClaims(claims);
                }
            }

            return principal;
        }
    }

}
