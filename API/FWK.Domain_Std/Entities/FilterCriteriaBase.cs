using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace FWK.Domain.Entities
{
    public class FilterCriteriaBase<TPrimaryKey>: GenericFilterCriteria
    {
        
        public TPrimaryKey Id { get; set; }
        
    }

    public class GenericFilterCriteria
    {

    }


}
