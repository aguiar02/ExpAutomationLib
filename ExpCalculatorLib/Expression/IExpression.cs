using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Tokenizer;
using ExpCalculatorLib.Visitor;

namespace ExpCalculatorLib.Expression
{
    public interface IExpression : IToken
    {
        Type ExpressionType { get; }
        object Eval();
        void CheckSemantics(ParsingContext context);

        void Accept(VisitorBase visitor);
    }

    public interface IExpression<T> : IExpression
    {
        new T Eval();
    }

    public class Expression<T> : IExpression<T>
    {
        private IExpression inner;
        public Expression(IExpression inner)
        {
            this.inner = inner;
        }

        public Type ExpressionType
        {
            get { return typeof(T); }
        }

        public T Eval()
        {
            return (T)inner.Eval();
        }

        object IExpression.Eval()
        {
            return inner.Eval();
        }

        public void CheckSemantics(ParsingContext context)
        {
            inner.CheckSemantics(context);
        }


        public void Accept(VisitorBase visitor)
        {
            inner.Accept(visitor);
        }
    }
}
