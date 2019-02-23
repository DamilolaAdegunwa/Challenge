using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using FWK.Domain.Interfaces.Repositories;

namespace WAC.Admin.Domain.Interfaces.Repositories
{
    public interface IErrorRepository : IRepositoryBase<Error, Int64>
    {

    }
}
