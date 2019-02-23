using System;
using System.Threading.Tasks;
using FWK.Domain.Interfaces.Repositories;

namespace FWK.Domain.Interfaces
{
    public interface IResilientTransaction<Context>
        where Context : IDbContext
    {
        Task ExecuteAsync(Func<Task> action);

        bool IsResilientTransaction();
    }
}
