using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Expression
{
    public abstract class StringExpression : IExpression
    {
        public Type ExpressionType
        {
            get { return typeof(string); }
        }

        public abstract object Eval();

        public abstract void CheckSemantics(ParsingContext context);

        public abstract void Accept(Visitor.VisitorBase visitor);
    }
}
