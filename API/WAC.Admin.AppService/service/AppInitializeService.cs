using FWK.AppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAC.Admin.AppService.ModelDto;
using WAC.Domain.Entities;

namespace WAC.Domain.Interfaces.Services
{
    public class AppInitializeService : IAppInitializeService
    {
        private readonly IUserService _userService;
        private readonly IRandomUserService _randomUserService;

        public AppInitializeService(IUserService userService, IRandomUserService randomUserService)
        {
            this._userService = userService;
            this._randomUserService = randomUserService;
        }

        protected virtual TDestination MapObject<TSource, TDestination>(TSource obj)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(obj);
        }

        protected virtual IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> list)
        {
            return AutoMapper.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list);
        }


        /// <summary>
        /// TODO pasar async;
        /// </summary>
        public async void InitializeUser()
        {
            var users = MapList<Model.Result, User>(this._randomUserService.Get().results.ToList()).ToList(); 
            await this._userService.AddRange(users);

        }

    }
}
