using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Expression
{
    public interface IExpectedTypeExpression : IExpression
    {
        Type ExpectedType { get; set; }
    }
}
