using FWK.Domain.Interfaces.Entities;

namespace FWK.Domain.Auditing
{
  public interface IModificationAudited : IHasModificationTime
  {
    int? LastUpdatedUserId { get; set; }
  }
    public interface IModificationAudited<TUser> : IModificationAudited, IHasModificationTime 
        where TUser : IEntity<int>
    {
        TUser LastUpdatedUser { get; set; }
    }

}
