using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FWK.ApiServices.Filters;
using FWK.ApiServices.Model;
using FWK.AppService.Interface;
using FWK.AppService.Model;
using FWK.Domain.Entities;
using FWK.Domain.Interfaces.Entities;

namespace FWK.ApiServices
{
    public abstract class ManagerController<TModel, TPrimaryKey, TDto, TFilter, TService> : ManagerControllerBase<TModel, TPrimaryKey, TFilter>
        where TModel : Entity<TPrimaryKey>, new()
        where TDto : EntityDto<TPrimaryKey>, new()
        where TFilter : FilterPagedListBase<TModel, TPrimaryKey>, IFilterPagedListBase<TModel, TPrimaryKey>, new()
        where TService : IAppServiceBase<TModel, TDto, TPrimaryKey>

    {

        protected ManagerController(TService service)
            : base()
        {
            _service = service;
        }

        TService _service;


        protected TService Service
        {
            get
            {
                return _service;
            }
        }



        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(TPrimaryKey id)
        {
            try
            {
                return ReturnData<TDto>(await this.Service.GetDtoByIdAsync(id));
            }
            catch (Exception ex)
            {
                return ReturnError<TDto>(ex);
            }
        }


        [HttpGet()]
        public virtual async Task<IActionResult> GetPagedList(TFilter filter)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var pList = await this.Service.GetDtoPagedListAsync<TFilter>(filter);
                    return ReturnData<PagedResult<TDto>>(pList);
                }
                else
                {
                    return ReturnError<TDto>(this.ModelState);
                }
            }
            catch (Exception ex)
            {
                return ReturnError<PagedResult<TDto>>(ex);
            }
        }



        [HttpPost]
        public virtual async Task<IActionResult> SaveNewEntity([FromBody] TDto dto)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    TPrimaryKey id = (await this.Service.AddAsync(dto)).IdValue;
                    return ReturnData<TPrimaryKey>(id);
                }
                else
                {
                    return ReturnError<TDto>(this.ModelState);
                }

            }
            catch (Exception ex)
            {
                return ReturnError<TDto>(ex);
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateEntity(TPrimaryKey id, [FromBody] TDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    id = (await this.Service.UpdateAsync(dto)).IdValue;
                    return ReturnData<TPrimaryKey>(id);
                }
                else
                {
                    return ReturnError<TDto>(this.ModelState);
                }

            }
            catch (Exception ex)
            {
                return ReturnError<TDto>(ex);
            }
        }


        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteById(TPrimaryKey id)
        {
            try
            {
                await this.Service.DeleteAsync(id);

                return ReturnData<string>("Deleted");
            }
            catch (Exception ex)
            {
                return ReturnError<TDto>(ex);
            }
        }


    }





}
