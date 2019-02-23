using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain.Interfaces.Repositories;

namespace WAC.Admin.Domain.Interfaces.Repositories
{
    public interface IPermissionRepository : IRepositoryBase<SysPermissions, long>
    {
        Task<string[]> GetPermissionForCurrentUser();
    }
}
