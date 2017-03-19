using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Expression
{
    public class ParameterExpression : IExpression
    {
        public IExpression InnerExpression { get; private set; }

        public Type ParameterType { get; set; }

        public ParameterExpression(IExpression innerExpression)
        {
            this.InnerExpression = innerExpression;
        }

        public Type ExpressionType
        {
            get { return InnerExpression.ExpressionType; }
        }

        public object Eval()
        {
            return InnerExpression.Eval();
        }

        public void CheckSemantics(ParsingContext context)
        {
            if (InnerExpression is IExpectedTypeExpression)
                (InnerExpression as IExpectedTypeExpression).ExpectedType = context.GetResolvedType(ParameterType);
            InnerExpression.CheckSemantics(context);
            if (InnerExpression.ExpressionType != typeof(double) && ExpressionHelper.IsNumeric(InnerExpression.ExpressionType))
                InnerExpression = new ConvertToDoubleExpression(InnerExpression);
            if (InnerExpression.ExpressionType.IsEnum && ParameterType.Equals(typeof(string)))
                InnerExpression = new ConvertToStringExpression(InnerExpression);
        }

        public void Accept(Visitor.VisitorBase visitor)
        {
            if (visitor.VisitEnter(this))
            {
                visitor.Visit(this);
                InnerExpression.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
