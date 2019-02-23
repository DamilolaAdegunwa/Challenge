using System;
using System.Collections.Generic;
using System.Text;
using FWK.Domain.Interfaces.Entities;

namespace FWK.Domain.Entities
{
    public abstract class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    { 
        public EntityDto()
        {
            
        } 
        public TPrimaryKey IdValue { get; set; } 
    }

    public interface IEntityDto<TPrimaryKey>: IGenericDto
    {
        TPrimaryKey IdValue { get; set; }
        
    } 

    public interface IGenericDto
    { 
    }

}
