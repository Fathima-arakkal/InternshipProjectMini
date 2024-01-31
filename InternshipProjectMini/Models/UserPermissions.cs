using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InternshipProjectMini.Models
{
    public class UserPermissions
    {
        [Key]
        public string UserId { get; set; }

        public bool Employee { get; set; }
        public bool Department { get; set; }
        public bool Machine { get; set; }
        public bool Location { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
