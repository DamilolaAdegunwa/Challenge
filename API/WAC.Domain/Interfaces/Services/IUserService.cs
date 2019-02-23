using FWK.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAC.Domain.Entities;

namespace WAC.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<User, string>
    {
        Task AddRange(List<User> users);
    }
}
