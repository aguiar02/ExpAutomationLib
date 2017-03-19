using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Exceptions
{
    [Serializable]
    public class ExpressionException : Exception
    {
        public ExpressionException()
        {
        }
        public ExpressionException(string message)
            : base(message)
        {
        }
        public ExpressionException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected ExpressionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
