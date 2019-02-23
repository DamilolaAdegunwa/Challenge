using System;
using FWK.Domain.Interfaces.Entities;

namespace FWK.Domain.Auditing
{
  public interface IHasDeletionTime : ISoftDelete
  {
    DateTime? DeletedDate { get; set; }
  }
}
