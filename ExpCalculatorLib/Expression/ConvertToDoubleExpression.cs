using System;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class ConvertToDoubleExpression : NumericExpression
    {
        private IExpression innerExpression;

        public ConvertToDoubleExpression(IExpression inner)
        {
            this.innerExpression = inner;
        }

        public override object Eval()
        {
            return Convert.ToDouble(innerExpression.Eval());
        }

        public override void CheckSemantics(ParsingContext context)
        {
            innerExpression.CheckSemantics(context);
            if (!ExpressionHelper.IsNumeric(innerExpression.ExpressionType))
                throw new SemanticErrorException("Expressão não é numérica.");
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
