using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Expression
{
    public class BooleanLiteralExpression : BooleanExpression
    {
        private bool value;

        public BooleanLiteralExpression(bool value)
        {
            this.value = value;
        }

        public override object Eval()
        {
            return value;
        }

        public override void CheckSemantics(ParsingContext context)
        {
            
        }

        public override void Accept(Visitor.VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
}
