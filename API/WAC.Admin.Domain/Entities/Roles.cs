using System;
using System.Collections.Generic;

namespace WAC.Admin.Domain.Entities
{
    public partial class SysRoles : TecsoRole<SysUsers>
    {
        public SysRoles()
        {
            PermissionRols = new HashSet<SysPermissionsRoles>();
            UserRoles = new HashSet<SysUsersRoles>();
        }

        public ICollection<SysPermissionsRoles> PermissionRols { get; set; }
        public ICollection<SysUsersRoles> UserRoles { get; set; }
    }
}
