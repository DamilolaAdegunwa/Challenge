using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FWK.Domain
{
    [Serializable]
    public class DomainValidationException : BaseException
    {
        public DomainValidationException(string message)
         : base(message)
        {
        }
    }
 
}
