using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FWK.Domain.Interfaces.Entities;

namespace FWK.Domain.Entities
{
    public class FilterPagedListBase<TModel, TPrimaryKey> : FilterCriteriaBase<TPrimaryKey>, IFilterPagedListBase<TModel, TPrimaryKey>
        where TModel : Entity<TPrimaryKey>
    {
        
        public FilterPagedListBase()
        {
         
        }



        public virtual Func<TModel, ItemDto<TPrimaryKey>> GetItmDTO()
        {
            return e => new ItemDto<TPrimaryKey>(e.IdValue, e.ToString()); 
        }



        public int? Page { get; set; }


        public virtual int? PageSize { get; set; }


        public String Sort { get; set; }


        public string FilterText { get; set; }

        public virtual Expression<Func<TModel, bool>> GetFilterExpression()
        {
            return e => true;

        }

        
        public virtual List<Expression<Func<TModel, Object>>> GetIncludesForGetById()
        {
            return new List<Expression<Func<TModel, object>>>();           
        }


        public virtual List<Expression<Func<TModel, Object>>> GetIncludesForPageList()
        {
            return new List<Expression<Func<TModel, object>>>();
            //Example
            //return new List<Expression<Func<TModel, object>>> {
            //    e=> e.AnyProperty,
            //    e=> e.OtherProperty
            //    e=> ((SysUsersRoles)e.SysUsersRoles).Role // multilevel
            //};
        }

        public virtual IQueryable<TModel> GetIncludesForPageList(IQueryable<TModel> query)
        {
            return query;
            //Example
            //return new List<Expression<Func<TModel, object>>> {
            //    e=> e.AnyProperty,
            //    e=> e.OtherProperty
            //};
        }

        public string GetIncludeString()
        {
            var StringInclude = string.Empty;

            var includeExpression = this.GetIncludesForPageList();

            if (includeExpression != null)
            {
                foreach (var include in includeExpression)
                {
                    StringInclude += include;
                } 
            } 
            return StringInclude;

        }

    }


    public class FilterPagedListSoftDelete<TModel, TPrimaryKey> : FilterPagedListBase<TModel, TPrimaryKey>
       where TModel : Entity<TPrimaryKey>, ISoftDelete
    {
        public override Expression<Func<TModel, bool>> GetFilterExpression()
        {
            return e => !e.IsDeleted;
        }
    }
}
