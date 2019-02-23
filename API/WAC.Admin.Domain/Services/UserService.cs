using WAC.Admin.Domain.Entities;
using WAC.Admin.Domain.Interfaces.Repositories;
using WAC.Admin.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain;
using FWK.Domain.Interfaces.Entities;
using FWK.Domain.Services;

namespace WAC.Admin.Domain.Services
{
    public class UserService : ServiceBase<SysUsers, int, IUserRepository>, IUserService
    {


        public UserService(IUserRepository repository)
            : base(repository)
        {
             
        }

        public async Task<List<SysUsersRoles>> GetUserRoles(int id)
        {
            return await this.repository.GetUserRoles(id);
        }

        public async Task<SysUsers> Login(string Username, string Password)
        {
            var User = await this.repository.GetUser(Username);
             
            return User;
        }

  

        public async Task<List<SysPermissions>> GetGrantedPermissionsAsync(int userId)
        {
            return await repository.GetGrantedPermissionsAsync(userId);
        }

        public async Task SetGrantedPermissionsAsync(int userId, List<string> grantedPermissionNames)
        {
            await repository.SetGrantedPermissionsAsync(userId, grantedPermissionNames);
        }



        protected override void ValidateEntity(SysUsers entity, SaveMode mode)
        {
            if (mode == SaveMode.Add)
            {
                 SysUsers existuser = this.repository.GetAll(e => e.Name == entity.Name && !e.IsDeleted).Items.FirstOrDefault();

                if (existuser != null)
                    throw new DomainValidationException("El Usuario ya existe");

            }

            base.ValidateEntity(entity, mode);
        }
       
       
    }
}
