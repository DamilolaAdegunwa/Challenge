
using FWK.ApiServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAC.Admin.AppService.ModelDto;
using WAC.Domain.Entities;
using WAC.Domain.Entities.Filters;
using WAC.Domain.Interfaces.Services;

namespace WAC.WebService.Admin.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ManagerController<User, string, UserDto, UserFilter, IUserAppService>
    {
        public UserController(IUserAppService service)
            : base(service)
        {

        }
    }

}