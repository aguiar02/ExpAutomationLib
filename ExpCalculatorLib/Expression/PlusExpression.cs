using System;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class PlusExpression : IExpression
    {
        private enum Operation { Add, StringConcat }

        private IExpression left;
        private IExpression right;
        private Operation operation;

        public PlusExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public Type ExpressionType { get; private set; }

        public object Eval()
        {
            if (operation == Operation.Add)
                return (double)left.Eval() + (double)right.Eval();
            else
                return string.Concat(left.Eval(), right.Eval());
        }

        public void CheckSemantics(ParsingContext context)
        {
            left.CheckSemantics(context);
            right.CheckSemantics(context);

            if (left.ExpressionType.Equals(typeof(string)) || right.ExpressionType.Equals(typeof(string)))
            {
                ExpressionType = typeof(string);
                operation = Operation.StringConcat;
                if (!left.ExpressionType.Equals(typeof(string)))
                    left = new ConvertToStringExpression(left);
                if (!right.ExpressionType.Equals(typeof(string)))
                    right = new ConvertToStringExpression(right);
            }
            else
            {
                if (!ExpressionHelper.IsNumeric(left.ExpressionType)
                    || !ExpressionHelper.IsNumeric(left.ExpressionType))
                    throw new SemanticErrorException("Expressões não compatíveis com a operação +.");

                ExpressionType = typeof(double);
                operation = Operation.Add;
                if (!left.ExpressionType.Equals(typeof(double)))
                    left = new ConvertToDoubleExpression(left);
                if (!right.ExpressionType.Equals(typeof(double)))
                    right = new ConvertToDoubleExpression(right);
            }
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
