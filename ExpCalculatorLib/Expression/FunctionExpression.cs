using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExpCalculatorLib.Exceptions;
using ExpCalculatorLib.Tokenizer;

namespace ExpCalculatorLib.Expression
{
    public class FunctionExpression : IExpression
    {
        protected IdentifierToken IdToken { get; private set; }

        protected IList<ParameterExpression> Parameters { get; private set; }
        private ParsingContext context;

        public FunctionExpression(IdentifierToken idToken, params ParameterExpression[] parameters)
        {
            IdToken = idToken;
            Parameters = parameters;
        }

        public Type ExpressionType
        {
            get
            {
                if (context == null)
                    return null;

                if (!this.context.Functions.ContainsKey(IdToken.IdentifierName))
                    return null;

                if (context.Functions[IdToken.IdentifierName].Method.IsGenericMethodDefinition)
                    return context.GetResolvedMethodInfo(context.Functions[IdToken.IdentifierName].Method).ReturnType;
                return context.Functions[IdToken.IdentifierName].Method.ReturnType;
            }
        }

        public object Eval()
        {
            MethodInvoker method = this.context.Functions[IdToken.IdentifierName];
            List<object> evaluatedParams = new List<object>();
            var methodParameters = method.Method.GetParameters();
            for (int i = 0; i < Parameters.Count; i++)
            {
                ParameterInfo paramInfo = methodParameters[i];
                IExpression paramExp = Parameters[i];
                Type resolvedType = this.context.GetResolvedType(paramInfo.ParameterType);
                if (resolvedType.IsAssignableFrom(paramExp.ExpressionType))
                    evaluatedParams.Add(paramExp.Eval());
                else
                    evaluatedParams.Add(resolvedType.GetConstructor(new Type[] { typeof(IExpression) }).Invoke(new object[] { paramExp }));
                    
            }

            try
            {
                return context.Functions[IdToken.IdentifierName].Invoke(context, evaluatedParams.ToArray());
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        private object[] EvalParameters()
        {
            return Parameters.Select(p => p.Eval()).ToArray();
        }

        public void CheckSemantics(ParsingContext context)
        {
            this.context = new ParsingContext(context);

            if (!this.context.Functions.ContainsKey(IdToken.IdentifierName))
                throw new SemanticErrorException("Função não encontrada:" + IdToken.IdentifierName);

            MethodInvoker method = this.context.Functions[IdToken.IdentifierName];

            if (method.Method.IsGenericMethodDefinition)
            {
                Type[] genericArgs = method.Method.GetGenericArguments();
                foreach (Type type in genericArgs)
                {
                    if (!this.context.GenericArgs.ContainsKey(type.Name))
                        this.context.GenericArgs.Add(type.Name, type);
                }
            }

            var methodParameters = method.Method.GetParameters();
            for (int i = 0; i < Parameters.Count; i++)
            {
                if (methodParameters.Length <= i)
                    throw new SemanticErrorException("Número de parametros incorreto. Esperado: " + methodParameters.Length);

                ParameterExpression parameter = Parameters[i];
                ParameterInfo paramInfo = methodParameters[i];

                parameter.ParameterType = paramInfo.ParameterType;
                parameter.CheckSemantics(this.context);
                this.context.ResolveType(paramInfo.ParameterType, parameter.ExpressionType);

            }

            if (Parameters.Count != methodParameters.Length)
                throw new SemanticErrorException("Número de parametros incorreto. Esperado: " + methodParameters.Length);

            for (int i = 0; i < Parameters.Count; i++)
            {
                ParameterInfo paramInfo = methodParameters[i];
                IExpression paramExp = Parameters[i];
                Type resolvedType = this.context.GetResolvedType(paramInfo.ParameterType);
                if (!resolvedType.IsAssignableFrom(paramExp.ExpressionType) && !resolvedType.Equals(typeof(Expression<>).MakeGenericType(paramExp.ExpressionType)))
                    throw new SemanticErrorException(string.Format("O {0}º parâmetro não é do tipo correto", i + 1));
            }
        }


        public void Accept(Visitor.VisitorBase visitor)
        {
            if (visitor.VisitEnter(this))
            {
                visitor.Visit(this);
                foreach (var parameter in this.Parameters)
                {
                    parameter.Accept(visitor);
                }
            }
            visitor.VisitLeave(this);
        }
    }
}
