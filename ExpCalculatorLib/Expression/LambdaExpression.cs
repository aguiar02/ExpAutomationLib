using System;
using ExpCalculatorLib.Exceptions;
using ExpCalculatorLib.Tokenizer;

namespace ExpCalculatorLib.Expression
{
    public class LambdaExpressionFunc<TIn, TOut>
    {
        private ParsingContext context;
        private string paramIdentifier;
        private IExpression expression;

        public LambdaExpressionFunc(ParsingContext context, string paramIdentifier, IExpression expression)
        {
            this.context = context;
            this.paramIdentifier = paramIdentifier;
            this.expression = expression;
        }

        public TOut EvalLambda(TIn parameter)
        {
            this.context.Parameters[paramIdentifier].ParameterValue = parameter;
            return (TOut)expression.Eval();
        }
    }

    public class LambdaExpression : IExpectedTypeExpression
    {
        private ParsingContext context;
        private IdentifierToken idToken;
        private IExpression expression;
        private Type expressionType;
        private Type paramType;
        private Type returnType;



        public Type ExpectedType { get; set; }

        public Type ExpressionType
        {
            get { return expressionType; }
        }

        /// <summary>
        /// Identifier token that represents the lambda parameter
        /// </summary>
        public IdentifierToken IdParameterToken
        {
            get { return idToken; }
        }

        public LambdaExpression(IdentifierToken idToken, IExpression expression)
        {
            this.idToken = idToken;
            this.expression = expression;
        }

        public object Eval()
        {
            return Activator.CreateInstance(expressionType, context, idToken.IdentifierName, (IExpression)expression);
        }

        public void CheckSemantics(ParsingContext context)
        {
            this.context = new ParsingContext(context, false);
            if (ExpectedType == null)
                throw new GenericTypeNotResolvedException();

            if (!ExpectedType.IsGenericType || ExpectedType.GetGenericTypeDefinition() != typeof(LambdaExpressionFunc<,>))
                throw new GenericTypeNotResolvedException();

            paramType = ExpectedType.GetGenericArguments()[0];
            this.context.Parameters.Add(idToken.IdentifierName, Parameter.NewParameter(paramType));

            expression.CheckSemantics(this.context);
            returnType = expression.ExpressionType;
            if (returnType != typeof(double) && ExpressionHelper.IsNumeric(returnType))
            {
                returnType = typeof(double);
                expression = new ConvertToDoubleExpression(expression);
            }
            expressionType = typeof(LambdaExpressionFunc<,>).MakeGenericType(paramType, returnType);
        }

        public object EvalLambda(object parameter)
        {
            this.context.Parameters[idToken.IdentifierName].ParameterValue = parameter;
            return expression.Eval();
        }

        public void Accept(Visitor.VisitorBase visitor)
        {
            if (visitor.VisitEnter(this))
            {
                visitor.Visit(this);
                expression.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
