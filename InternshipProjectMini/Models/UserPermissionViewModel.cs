
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InternshipProjectMini.Models
{
    public class UserPermissionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserName { get; set; }
        public List<RolePermission> Roles { get; set; }

        public UserPermissionViewModel()
        {
            Roles = new List<RolePermission>();
        }
    }

   
}