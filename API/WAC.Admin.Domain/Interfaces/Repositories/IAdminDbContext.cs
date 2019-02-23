using System;
using System.Collections.Generic;
using System.Text;
using FWK.Domain.Interfaces;
using FWK.Domain.Interfaces.Repositories;

namespace WAC.Admin.Domain.Interfaces.Repositories
{
    public interface IAdminDbContext : IDbContext
    {
    }

    public interface IAdminDBResilientTransaction : IResilientTransaction<IAdminDbContext> 
    {
        
    }

}
