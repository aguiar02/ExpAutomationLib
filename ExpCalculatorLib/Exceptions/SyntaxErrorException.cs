using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;

namespace ExpCalculatorLib.Exceptions
{
    [Serializable]
    public class SyntaxErrorException : ExpressionException
    {
        public IExpression Expression { get; private set; }

        public IExpression RootExpression { get; private set; }

        public ParameterExpression[] Args { get; set; }

        public SyntaxErrorException() { }
        public SyntaxErrorException(string message) : base(message) { }
        public SyntaxErrorException(string message, Exception inner) : base(message, inner) { }
        protected SyntaxErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public SyntaxErrorException(string message, IExpression expression)
            : base(message)
        {
            this.Expression = expression;
        }

        public SyntaxErrorException(string message, IExpression expression, IExpression rootExpression)
            : base(message)
        {
            this.Expression = expression;
            this.RootExpression = rootExpression;
        }

        public SyntaxErrorException(string message, Exception inner, IExpression expression)
            : base(message, inner)
        {
            this.Expression = expression;
        }

        public SyntaxErrorException(string message, Exception inner, IExpression expression, IExpression rootExpression)
            : base(message, inner)
        {
            this.Expression = expression;
            this.RootExpression = rootExpression;
        }
    }
}
