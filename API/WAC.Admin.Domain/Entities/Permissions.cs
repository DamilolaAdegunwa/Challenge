using System;
using System.Collections.Generic;
using FWK.Domain.Entities;

namespace WAC.Admin.Domain.Entities
{
    public partial class SysPermissions : Entity<long>
    {
        public SysPermissions()
        {
            PermissionRols = new HashSet<SysPermissionsRoles>();
            PermissionsUsers = new HashSet<SysPermissionsUsers>();
        }
        
        public string Area { get; set; }
        public string Page { get; set; }
        public string Action { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }


        public virtual SysAreas Areas { get; set; }

        public virtual SysPages Pages { get; set; }

        public ICollection<SysPermissionsUsers> PermissionsUsers { get; set; }
       
        public virtual ICollection<SysPermissionsRoles> PermissionRols { get; set; }
    }
}
