using FWK.Infra.Data;
using FWK.Infra.Data.Interface;
using FWK.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WAC.Admin.Domain.Interfaces.Repositories;
using WAC.Domain.Entities;
using WAC.Domain.Interfaces.Repositories;
using WAC.infra.Data.Contexto;

namespace WAC.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<WACContext, User, string>, IUserRepository
    {
        public UserRepository(IWACCContext _context)
         : base(new DbContextProvider<WACContext>(_context))
        {
        }

        protected override IQueryable<User> AddIncludeForGet(DbSet<User> dbSet)
        {
            return base.AddIncludeForGet(dbSet).Include(e=> e.Location);
        }


        public async Task AddRange(List<User> users)
        {
            try
            {
                users.ForEach(e => e.LocationId = e.Location.IdValue);

                await Context.AddRangeAsync(users);
                await this.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                throw;
            }
        }

        //TODO : usar el Find en el fwk para no hacer esto
        public override Expression<Func<User, bool>> GetFilterById(string id)
        {
            return e => e.IdValue == id;
        }
    }
}
