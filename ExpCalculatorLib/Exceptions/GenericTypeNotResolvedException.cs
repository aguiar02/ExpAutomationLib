using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Exceptions
{
    [Serializable]
    public class GenericTypeNotResolvedException : SemanticErrorException
    {
        public GenericTypeNotResolvedException() { }
        public GenericTypeNotResolvedException(string message) : base(message) { }
        public GenericTypeNotResolvedException(string message, Exception inner) : base(message, inner) { }
        protected GenericTypeNotResolvedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
