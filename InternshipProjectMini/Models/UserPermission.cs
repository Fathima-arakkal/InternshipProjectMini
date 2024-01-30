namespace InternshipProjectMini.Models
{
    public class UserPermission
    {
       public int Id { get; set; }
       public string UserId { get; set; }
       public string AllowedModulesJson { get; set; }
    }
}
