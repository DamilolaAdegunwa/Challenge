using FWK.Domain.Entities;
using FWK.Domain.Interfaces.Entities;

namespace FWK.ApiServices
{
    public abstract class ManagerControllerBase<TModel,TPrimaryKey, TFilter>:ControllerBase
        where TModel : Entity<TPrimaryKey>, new()
        where TFilter : FilterCriteriaBase<TPrimaryKey>
    {
        public ManagerControllerBase()
            :base()
        {

        }
    }
}