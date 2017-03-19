using System;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class UnaryMinusExpression : IExpression
    {
        private IExpression inner;

        public UnaryMinusExpression(IExpression inner)
        {
            this.inner = inner;
        }

        public Type ExpressionType
        {
            get { return typeof(double); }
        }

        public object Eval()
        {
            return -((double)inner.Eval());
        }


        public void CheckSemantics(ParsingContext context)
        {
            inner.CheckSemantics(context);
            if (!ExpressionHelper.IsNumeric(inner.ExpressionType))
                throw new SemanticErrorException("Expressão numérica esperada.");

            if (!inner.ExpressionType.Equals(typeof(double)))
                inner = new ConvertToDoubleExpression(inner);
        }

        public void Accept(Visitor.VisitorBase visitor)
        {
            if (visitor.VisitEnter(this))
            {
                visitor.Visit(this);
                inner.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
