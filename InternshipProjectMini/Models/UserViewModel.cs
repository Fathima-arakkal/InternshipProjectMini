using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InternshipProjectMini.Models
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [ForeignKey("RoleId")]
        
        public virtual RoleViewModel Role { get; set; }
        [Display(Name = "Role")]
        public string RoleId { get; set; }


        
        

        public bool EmployeeAccess { get; set; }
        public bool LocationAccess { get; set; }
        public bool MachineAccess { get; set; }
        public bool DepartmentAccess { get; set; }
    }
}
