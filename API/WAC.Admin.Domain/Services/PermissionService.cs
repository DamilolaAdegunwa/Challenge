using WAC.Admin.Domain.Entities;
using WAC.Admin.Domain.Interfaces.Repositories;
using WAC.Admin.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain.Services;

namespace WAC.Admin.Domain.Services
{
    public class PermissionService : ServiceBase<SysPermissions, long, IPermissionRepository>, IPermissionService
    {       
        public PermissionService(IPermissionRepository repository)
            : base(repository)
        {
            
        }

        public async Task<string[]> GetPermissionForCurrentUser()
        {
            return await this.repository.GetPermissionForCurrentUser();
        }
    }
    
}
