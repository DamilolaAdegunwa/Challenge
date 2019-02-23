using FWK.AppService.Interface;
using FWK.Domain.Entities;
using FWK.Domain.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WAC.Admin.AppService.ModelDto;
using WAC.Domain.Entities;

namespace WAC.Domain.Interfaces.Services
{
    public  interface IAppInitializeService
    {
        void InitializeUser();
    }
}
