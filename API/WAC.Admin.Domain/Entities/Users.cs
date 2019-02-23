using System;
using System.Collections.Generic;
using FWK.Domain.Extensions;
using FWK.Extensions;

namespace WAC.Admin.Domain.Entities
{
    public partial class SysUsers : TecsoUser<SysUsers>
    {
        public SysUsers()
        {            
            PermissionsUsers = new HashSet<SysPermissionsUsers>();
            UserRoles = new HashSet<SysUsersRoles>();            
        }
        

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; } 
        public string PasswordHash { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int? AccessFailedCount { get; set; }
        public string PasswordResetCode { get; set; }



        public ICollection<SysPermissionsUsers> PermissionsUsers { get; set; }
        public ICollection<SysUsersRoles> UserRoles { get; set; }


       


        public void SetNewPasswordResetCode()
        {            
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(10).ToUpperInvariant();
        }

    }
}
