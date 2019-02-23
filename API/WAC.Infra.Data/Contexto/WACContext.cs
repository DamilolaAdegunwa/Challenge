using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking; 
using WAC.Admin.Domain.Interfaces.Repositories;
using WAC.infra.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FWK.Domain;
using FWK.Domain.Auditing;
using FWK.Domain.bus;
using FWK.Domain.Entities;
using FWK.Domain.Event;
using FWK.Domain.Interfaces.Entities;
using FWK.Domain.Interfaces.Services;
using FWK.Domain.UOW;
using Microsoft.Extensions.DependencyInjection;
using WAC.Admin.Domain; 
using Microsoft.EntityFrameworkCore.Storage;
using FWK.Domain.Interfaces.Repositories;
using FWK.Infra.Data;
using WAC.Domain.Entities;

namespace WAC.infra.Data.Contexto
{
    public class WACContext : BaseContext, IWACCContext
    { 
        //private IAdminDBResilientTransaction resilientTransaction;

        //private IAdminDBResilientTransaction ResilientTransaction
        //{
        //    get
        //    {
        //        return this.resilientTransaction ?? (this.resilientTransaction = ServiceProviderResolver.ServiceProvider.GetService<IAdminDBResilientTransaction>());
        //    }

        //}


        public WACContext(DbContextOptions<WACContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            //En package Manager Console lo siguiente
            //scaffold-dbcontext 'Server=server;Database=database;User Id=sa; Password=root+123'  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force 
            

            modelBuilder.ApplyConfigurations();
            

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //TODO: implementar transacciones anidadas UOF
            //if (ResilientTransaction.IsResilientTransaction())
            //{
            //    return Task.FromResult(0);
            //}
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //if (ResilientTransaction.IsResilientTransaction())
            //{
            //    return Task.FromResult(0);
            //} 
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Location> Locations { get; set; }

    }
     

}
