using Microsoft.AspNetCore.Authorization;

namespace InternshipProjectMini.Models
{
    public class SuperAdminRequirement : IAuthorizationRequirement
    {
        // Marker interface for the requirement
    }
}