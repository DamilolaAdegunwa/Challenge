using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WAC.Admin.Domain
{
    public interface IUserEmailer
    {
        Task SendPasswordResetLinkAsync(SysUsers user, string link = null);

      
    }
}
