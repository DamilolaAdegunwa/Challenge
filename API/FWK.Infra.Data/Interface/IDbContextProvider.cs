using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FWK.Domain.Interfaces;
using FWK.Domain.Interfaces.Repositories;

namespace FWK.Infra.Data.Interface
{
    public interface IDbContextProvider<out TContext> 
        where TContext : DbContext
    {
        TContext GetDbContext(); 
    }
}
