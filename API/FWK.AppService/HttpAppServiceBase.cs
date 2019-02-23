using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FWK.AppService.Interface;
using FWK.AppService.Model;
using FWK.Domain;
using FWK.Domain.Entities;
using FWK.Domain.Interfaces.Services;
using FWK.Domain.Interfaces.Entities;

namespace FWK.AppService
{
    public abstract class HttpAppServiceBase<TEntity, TDto, TPrimaryKey> : IHttpAppServiceBase<TEntity, TDto, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
        where TDto : EntityDto<TPrimaryKey>, new()

    {
        protected HttpCustomClient<TDto> httpClient;

        public abstract string EndPoint { get;  }

        protected readonly IAuthService authService;
        protected IConfiguration configuration;

        public HttpAppServiceBase(IAuthService _authService)
        {
            authService = _authService;
            configuration = ServiceProviderResolver.ServiceProvider.GetService<IConfiguration>();
            var urlPath = String.Format("{0}{1}/", configuration.GetValue<String>("LogsUrl"), this.EndPoint);
            this.httpClient = new HttpCustomClient<TDto>(urlPath, authService.GetCurretToken());
        }

        
        protected virtual IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> list)
        {
            return AutoMapper.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list);
        }

        protected virtual TDestination MapObject<TSource, TDestination>(TSource obj)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(obj);
        }

        protected virtual TDestination MapObject<TSource, TDestination>(TSource obj, TDestination dest)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(obj, dest);
        }

        

        public virtual TEntity Add(TEntity obj)
        {
            return null;
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            return await this.httpClient.PostRequest<TDto>("SaveNewEntity", Newtonsoft.Json.JsonConvert.SerializeObject(dto));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var dto = MapObject<TEntity,TDto>(entity);
            dto =  await this.httpClient.PostRequest<TDto>("SaveNewEntity", Newtonsoft.Json.JsonConvert.SerializeObject(dto));
            return MapObject<TDto, TEntity>(dto);
        }

        public TEntity AddOrUpdate(TEntity entity)
        {
            var dto = MapObject<TEntity, TDto>(entity);

            string action = "UpdateEntity";
            
            dto = this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(dto)).Result;

            return MapObject<TDto, TEntity>(dto);
        }

        public async Task<TDto> AddOrUpdateAsync(TDto dto)
        {
            string action = "UpdateEntity";

            dto = await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(dto));
            return dto;
        }

        public async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            var dto = MapObject<TEntity, TDto>(entity);

            string action = "UpdateEntity";

            dto = await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(dto));

            return MapObject<TDto, TEntity>(dto);

        }

        public async void Delete(TPrimaryKey id)
        {
            
            string action = "DeleteById";

            await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(id));
            
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            string action = "DeleteById";

            await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(id));

        }

        public void Dispose()
        {
            
        }

        public PagedResult<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, Object>>> includeExpression = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ItemDto<TPrimaryKey>>> GetItemsAsync<TFilter>(TFilter filter) where TFilter : FilterPagedListBase<TEntity, TPrimaryKey>, new()
        {
            string action = "GetItemsAsync";

            List<ItemDto<TPrimaryKey>> pList = await this.httpClient.PostRequest<List<ItemDto<TPrimaryKey>>>(action, Newtonsoft.Json.JsonConvert.SerializeObject(filter));

            return pList;
        }

        public TEntity GetById(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            
            string action = "GetByIdAsync";

            var dto = await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(id));

            return MapObject<TDto, TEntity>(dto);

        }

        public async Task<TDto> GetDtoByIdAsync(TPrimaryKey id)
        {
            string action = "GetByIdAsync";

            var dto = await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(id));

            return dto;

        }

        public PagedResult<TEntity> GetPagedList<TFilter>(TFilter filter) where TFilter : FilterPagedListBase<TEntity, TPrimaryKey>
        {
            string action = "GetPagedList";

            PagedResult<TDto> pList = this.httpClient.PostRequest<PagedResult<TDto>> (action, Newtonsoft.Json.JsonConvert.SerializeObject(filter)).Result;

            return new PagedResult<TEntity>(pList.TotalCount,this.MapList<TDto,TEntity>(pList.Items).ToList()) ;

        }

        public async Task<PagedResult<TEntity>> GetPagedListAsync<TFilter>(TFilter filter) where TFilter : FilterPagedListBase<TEntity, TPrimaryKey>, new()
        {
            string action = "GetPagedList";

            PagedResult<TDto> pList = await this.httpClient.PostRequest<PagedResult<TDto>>(action, Newtonsoft.Json.JsonConvert.SerializeObject(filter));

            return new PagedResult<TEntity>(pList.TotalCount, this.MapList<TDto, TEntity>(pList.Items).ToList());

        }

        public TEntity Update(TEntity entity)
        {
            var dto = MapObject<TEntity, TDto>(entity);

            string action = "UpdateEntity";

            dto = this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(dto)).Result;

            return MapObject<TDto, TEntity>(dto);

        }

        public async Task<TDto> UpdateAsync(TDto dto)
        {

            string action = "UpdateEntity";

            dto = await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(dto));

            return dto;

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var dto = MapObject<TEntity, TDto>(entity);

            string action = "UpdateEntity";

            dto = await this.httpClient.PostRequest<TDto>(action, Newtonsoft.Json.JsonConvert.SerializeObject(dto));

            return MapObject<TDto, TEntity>(dto);

        }

        public Task<PagedResult<TDto>> GetDtoAllAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includeExpression = null)
        {
            throw new NotImplementedException();
        }
 

        public async Task<PagedResult<TDto>> GetDtoAllFilterAsync<TFilter>(TFilter filter) where TFilter : FilterPagedListBase<TEntity, TPrimaryKey>
        {
            string action = "GetPagedList";

            PagedResult<TDto> pList = await this.httpClient.PostRequest<PagedResult<TDto>>(action, Newtonsoft.Json.JsonConvert.SerializeObject(filter));

            return pList;
        }

        public async Task<PagedResult<TDto>> GetDtoPagedListAsync<TFilter>(TFilter filter) where TFilter : FilterPagedListBase<TEntity, TPrimaryKey>, new()
        {
            string action = "GetPagedList";

            PagedResult<TDto> pList = await this.httpClient.PostRequest<PagedResult<TDto>>(action, Newtonsoft.Json.JsonConvert.SerializeObject(filter));

            return pList;
        }
    }
}
