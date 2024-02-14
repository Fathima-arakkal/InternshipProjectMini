using System;
using System.Collections.Generic;

namespace InternshipProjectMini.Models
{
    public class PermissionViewModel
    {
        public bool Employee { get; set; }
        public bool Department { get; set; }
        public bool Machine { get; set; }
        public bool Location { get; set; }

        public string SelectedPermissions { get; set; }

        public string RoleId { get; set; }
        public IList<RoleClaimsViewModel> RoleClaims { get; set; }
    }
   
       
    
    public class RoleClaimsViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }

}

    
