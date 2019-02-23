using WAC.Admin.Domain.Entities;
using WAC.Admin.Domain.Interfaces.Repositories;
using WAC.Admin.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using FWK.Domain.Services;

namespace WAC.Admin.Domain.Services
{
    public class LogService : ServiceBase<Logs,Int64, ILogRepository>, ILogService
    {

        public LogService(ILogRepository repository)
            : base(repository)
        {
            
        }

    }
    
}
