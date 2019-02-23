using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using FWK.Domain.Interfaces.Repositories;

namespace WAC.Admin.Domain.Interfaces.Repositories
{
    public interface ILogRepository : IRepositoryBase<Logs, Int64>
    {

    }
}
