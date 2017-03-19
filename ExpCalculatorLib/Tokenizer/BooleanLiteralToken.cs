using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Tokenizer
{
    public class BooleanLiteralToken : IToken
    {
        public bool Value { get; private set; }

        public BooleanLiteralToken(bool value)
        {
            Value = value;
        }
    }
}
