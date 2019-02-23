using FWK.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAC.Domain.Entities;

namespace WAC.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User, string>
    {
        Task AddRange(List<User> users);
    }
}
