
using FWK.Domain.Interfaces.Entities;

namespace FWK.Domain.Auditing
{
    public interface IAudited : ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime
    {
    }
    public interface IAudited<TUser> : IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, ICreationAudited<TUser>, IModificationAudited<TUser> 
        where TUser : IEntity<int>
    {
    }
}
