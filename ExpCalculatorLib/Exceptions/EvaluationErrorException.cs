using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;

namespace ExpCalculatorLib.Exceptions
{
    [Serializable]
    public class EvaluationErrorException : ExpressionException
    {
        public EvaluationErrorException() { }
        public EvaluationErrorException(string message) : base(message) { }
        public EvaluationErrorException(string message, Exception inner) : base(message, inner) { }
        protected EvaluationErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public EvaluationErrorException(string message, IExpression expression) : base(message)
        {
            Expression = expression;
        }
        public EvaluationErrorException(string message, IExpression expression, Exception inner) : base(message, inner)
        {
            Expression = expression;
        }

        public IExpression Expression { get; set; }
    }
}
