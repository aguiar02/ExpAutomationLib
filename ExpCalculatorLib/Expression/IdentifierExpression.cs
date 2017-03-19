using System;
using ExpCalculatorLib.Exceptions;
using ExpCalculatorLib.Tokenizer;

namespace ExpCalculatorLib.Expression
{
    public class IdentifierExpression : IExpression
    {
        private Type expressionType = null;

        public IdentifierToken IdToken { get; private set; }

        public ParsingContext Context { get; private set; }

        public IdentifierExpression(IdentifierToken idToken)
        {
            IdToken = idToken;
        }

        public Type ExpressionType
        {
            get
            {
                if (Context == null)
                    return null;

                return expressionType;
            }
        }

        public object Eval()
        {
            if (Context.Parameters[IdToken.IdentifierName].ParameterType == typeof(IExpression))
            {
                try
                {
                    return (Context.Parameters[IdToken.IdentifierName].ParameterValue as IExpression).Eval();
                }
                catch (Exception e)
                {
                    throw new EvaluationErrorException(string.Format("Erro ao calcular o valor do parâmetro '{0}': {1}", this.IdToken.IdentifierName, e.Message), (Context.Parameters[IdToken.IdentifierName].ParameterValue as IExpression), e);
                }
            }
                
            return Context.Parameters[IdToken.IdentifierName].ParameterValue;
        }


        public void CheckSemantics(ParsingContext context)
        {
            this.Context = context;
            
            //se a variável for privada e não está num contexto privado, variável não acessível
            if (!context.Parameters.ContainsKey(IdToken.IdentifierName) || context.Parameters[IdToken.IdentifierName].IsPrivate && !context.IsPrivateContext)
                throw new SemanticErrorException(string.Format("O parâmetro '{0}' não existe, não está acessível ou contém erros", IdToken.IdentifierName));

            if (context.Parameters[IdToken.IdentifierName].ParameterType == typeof(IExpression))
            {
                ParsingContext innerContext = new ParsingContext(context, false);
                innerContext.IsPrivateContext = true;
                IExpression innerExpression = (this.Context.Parameters[IdToken.IdentifierName].ParameterValue as IExpression);
                innerExpression.CheckSemantics(innerContext);
                expressionType = innerExpression.ExpressionType;
            }
            else
            {
                expressionType = Context.Parameters[IdToken.IdentifierName].ParameterType;
            }
        }


        public void Accept(Visitor.VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
}
