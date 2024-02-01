namespace InternshipProjectMini.Models
{
    
    public class Role
    {
        public String RoleId { get; set; }
        public string RoleName { get; set; }
        public bool CanAccessMachine { get; set; }
        public bool CanAccessDepartment { get; set; }
        public bool CanAccessEmployee { get; set; }
        public bool CanAccessLocation { get; set; }
    }

}
