using System;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class OrExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public OrExpression(IExpression left, IExpression right)
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
            return (bool)left.Eval() || (bool)right.Eval();
        }


        public void CheckSemantics(ParsingContext context)
        {
            left.CheckSemantics(context);
            right.CheckSemantics(context);
            if (!left.ExpressionType.Equals(typeof(bool)))
                throw new SemanticErrorException("Expressão booleana esperada.");
            if (!right.ExpressionType.Equals(typeof(bool)))
                throw new SemanticErrorException("Expressão booleana esperada.");
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
