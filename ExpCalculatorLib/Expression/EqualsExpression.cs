using System;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class EqualsExpression : IExpression
    {
        private IExpression right;
        private IExpression left;

        public EqualsExpression(IExpression left, IExpression right)
        {
            this.right = right;
            this.left = left;
        }

        public object Eval()
        {
            return object.Equals(right.Eval(), left.Eval());
        }

        public Type ExpressionType
        {
            get { return typeof(bool); }
        }

        public void CheckSemantics(ParsingContext context)
        {
            left.CheckSemantics(context);
            right.CheckSemantics(context);

            if (left.ExpressionType == typeof(string) && right.ExpressionType.IsEnum)
                right = new ConvertToStringExpression(right);
            else if (left.ExpressionType.IsEnum && right.ExpressionType == typeof(string))
                left = new ConvertToStringExpression(left);
            else
            {
                if (!left.ExpressionType.Equals(typeof(double)) && ExpressionHelper.IsNumeric(left.ExpressionType))
                    left = new ConvertToDoubleExpression(left);
                if (!right.ExpressionType.Equals(typeof(double)) && ExpressionHelper.IsNumeric(right.ExpressionType))
                    right = new ConvertToDoubleExpression(right);
            }

            if (!left.ExpressionType.Equals(right.ExpressionType))
                throw new SemanticErrorException("Expressões não compatíveis para a operação =.");
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
