using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FWK.Domain.Entities;
using System.Linq;
using FWK.Domain.Extensions;

namespace WAC.Admin.Domain.Entities.Filters
{
    public class UserFilter : FilterPagedListFullAudited<SysUsers, int>
    {
        public int? RoleId { get; set; }

        public string Name { get; set; }


        public override List<Expression<Func<SysUsers, object>>> GetIncludesForPageList()
        {
            return new List<Expression<Func<SysUsers, object>>>
            {
                e=> e.UserRoles
               // e=> e.Grupolinea
            };
        }


        public override Expression<Func<SysUsers, bool>> GetFilterExpression()
        {

            Expression<Func<SysUsers, bool>> baseFE = base.GetFilterExpression();

            if (!String.IsNullOrEmpty(this.FilterText))
            {
                Expression<Func<SysUsers, bool>> filterTextExp = e => e.Name.Contains(this.FilterText) || e.Email.Contains(this.FilterText)
                || e.DisplayName.Contains(this.FilterText);
                baseFE = baseFE.And(filterTextExp);
            }

            if (!String.IsNullOrEmpty(this.Name))
            {
                baseFE = baseFE.And(e => e.Name == Name);
            } 

            if (this.RoleId.HasValue)
            {
                var _RoleId = RoleId.Value;
                Expression<Func<SysUsers, bool>> RoleIdExp = e => e.UserRoles.Any(a => a.RoleId == _RoleId);
                baseFE = baseFE.And(RoleIdExp);
            }

            return baseFE;
        }
    }





}
