using System;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class DifferentExpresstion : IExpression
    {
        private IExpression left;
        private IExpression right;

        public DifferentExpresstion(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public Type ExpressionType
        {
            get { return typeof(bool); }
        }

        public object Eval()
        {
            return !left.Eval().Equals(right.Eval());
        }


        public void CheckSemantics(ParsingContext context)
        {
            left.CheckSemantics(context);
            right.CheckSemantics(context);
            if (!left.ExpressionType.Equals(right.ExpressionType))
                throw new SemanticErrorException("Expressões não compatíveis para a operação <>.");
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
