using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain.Interfaces.Entities;
using FWK.Domain.Interfaces.Services;

namespace WAC.Admin.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<SysUsers, int>
    {
        Task<SysUsers> Login(string Username, string Password);

        Task<List<SysUsersRoles>> GetUserRoles(int id);
        
        Task<List<SysPermissions>> GetGrantedPermissionsAsync(int userId);
        Task SetGrantedPermissionsAsync(int id, List<string> grantedPermissionNames);        
    }
}
