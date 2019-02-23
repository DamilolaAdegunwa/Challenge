using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain.Interfaces.Services;

namespace WAC.Admin.Domain.Interfaces.Services
{
    public interface IRoleService : IServiceBase<SysRoles, int>
    {
        Task<List<SysPermissions>> GetGrantedPermissionsAsync(int RolId);
        Task SetGrantedPermissionsAsync(int RolId, List<string> grantedPermissionNames);
    }
}
