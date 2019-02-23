using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain.Interfaces.Entities;
using FWK.Domain.Interfaces.Repositories;

namespace WAC.Admin.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<SysUsers,int>
    {
        Task<List<SysUsersRoles>> GetUserRoles(int id);
        Task<SysUsers> GetUser(string Username);
        Task<List<SysPermissions>> GetGrantedPermissionsAsync(int UserId);
        Task SetGrantedPermissionsAsync(int userId, List<string> grantedPermissionNames);
        
        
    }
}
