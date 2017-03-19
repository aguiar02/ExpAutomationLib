using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Expression
{
    public abstract class NumericExpression : IExpression
    {
        public Type ExpressionType
        {
            get { return typeof(double); }
        }

        public abstract object Eval();

        public abstract void CheckSemantics(ParsingContext context);

        public abstract void Accept(Visitor.VisitorBase visitor);

    }
}
