using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipProjectMini.Models
{

    public class RoleViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Employee { get; set; }
        public bool Location { get; set; }
        public bool Machine { get; set; }
        public bool Department { get; set; }
    }

}
