using System;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class MultiplyExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public MultiplyExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public Type ExpressionType
        {
            get { return typeof(double); }
        }

        public object Eval()
        {
            return Convert.ToDouble(left.Eval()) * Convert.ToDouble(right.Eval());
        }

        public void CheckSemantics(ParsingContext context)
        {
            left.CheckSemantics(context);
            right.CheckSemantics(context);
            if (!ExpressionHelper.IsNumeric(left.ExpressionType)
                || !ExpressionHelper.IsNumeric(left.ExpressionType))
                throw new SemanticErrorException("Expressões não compatíveis com a operação *.");
        }

        public void Accept(Visitor.VisitorBase visitor)
        {
            if (visitor.VisitEnter(this))
            {
                left.Accept(visitor);
                visitor.Visit(this);
                right.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
