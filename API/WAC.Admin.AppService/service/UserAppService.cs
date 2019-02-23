using FWK.AppService;
using System;
using System.Collections.Generic;
using System.Text;
using WAC.Admin.AppService.ModelDto;
using WAC.Domain.Entities;

namespace WAC.Domain.Interfaces.Services
{
    public class UserAppService : AppServiceBase<User, UserDto, string, IUserService>, IUserAppService
    {
        public UserAppService(IUserService serviceBase) : base(serviceBase)
        { 
        }
    }
}
