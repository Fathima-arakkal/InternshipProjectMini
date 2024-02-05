using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipProjectMini.Models
{
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RoleName { get; set; }

        public List<string> AssignedModules { get; set; }

        public RolePermission()
        {
            AssignedModules = new List<string>();
        }
    }
}
