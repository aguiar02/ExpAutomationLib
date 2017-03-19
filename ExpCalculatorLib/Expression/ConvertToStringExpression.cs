using System;
using System.Globalization;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class ConvertToStringExpression : StringExpression
    {
        private IExpression innerExpression;

        public ConvertToStringExpression(IExpression inner)
        {
            this.innerExpression = inner;
        }

        public override object Eval()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}", innerExpression.Eval());
        }

        public override void CheckSemantics(ParsingContext context)
        {
            innerExpression.CheckSemantics(context);
        }

        public override void Accept(Visitor.VisitorBase visitor)
        {
            if (visitor.VisitEnter(this))
            {
                visitor.Visit(this);
                innerExpression.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
