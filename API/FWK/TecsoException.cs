using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FWK
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException(SerializationInfo serializationInfo, StreamingContext context)
          : base(serializationInfo, context)
        {
        }

        public BaseException(string message)
          : base(message)
        {
        }

        public BaseException(string message, Exception innerException)
          : base(message, innerException)
        {
        }
    }
}
