using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;

namespace ExpCalculatorLib.Exceptions
{
    [Serializable]
    public class SemanticErrorException : ExpressionException
    {
        public IExpression Expression { get; private set; }

        public SemanticErrorException() { }
        public SemanticErrorException(string message) : base(message) { }
        public SemanticErrorException(string message, Exception inner) : base(message, inner) { }
        protected SemanticErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public SemanticErrorException(string message, IExpression expression)
            : base (message)
        {
            this.Expression = expression;
        }
    }
}
