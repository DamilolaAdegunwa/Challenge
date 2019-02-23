using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FWK.Domain.Entities;
using System.Linq;
using FWK.Domain.Extensions;


namespace WAC.Domain.Entities.Filters
{
    public class UserFilter : FilterPagedListBase<User, string>
    { 
        public string Location { get; set; }
        public override List<Expression<Func<User, object>>> GetIncludesForPageList()
        {
            return base.GetIncludesForPageList();
        }

        public override List<Expression<Func<User, object>>> GetIncludesForGetById()
        {
            return new List<Expression<Func<User, object>>>
            {
                e => e.Location
            };
        }


        public override Expression<Func<User, bool>> GetFilterExpression()
        {

            Expression<Func<User, bool>> baseExp = base.GetFilterExpression();

            if (!String.IsNullOrEmpty(this.FilterText))
            {
                Expression<Func<User, bool>> filterTextExp = e =>
                e.FirstName.Contains(this.FilterText)
                || e.UserName.Contains(this.FilterText)
                || e.LastName.Contains(this.FilterText);

                baseExp = baseExp.And(filterTextExp);
            }


            return baseExp;
        }
    }
}
