using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InternshipProjectMini.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string SelectedPermissions { get; set; }

        public virtual UserPermissions UserPermissions { get; set; }
        
    }
}
