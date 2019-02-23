using System;

namespace FWK.Domain.Auditing
{
  public interface IHasCreationTime
  {
    DateTime CreatedDate { get; set; }
  }
}
