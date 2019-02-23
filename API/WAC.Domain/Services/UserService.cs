using FWK.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAC.Domain.Entities;
using WAC.Domain.Interfaces.Repositories;
using WAC.Domain.Interfaces.Services;

namespace WAC.Domain.Services
{
    public class UserService : ServiceBase<User, string, IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public async Task AddRange(List<User> users)
        {
            await repository.AddRange(users);
        }
    }
}
