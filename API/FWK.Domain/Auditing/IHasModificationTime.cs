using System;

namespace FWK.Domain.Auditing
{
  public interface IHasModificationTime
  {
    DateTime? LastUpdatedDate { get; set; }
  }
}
