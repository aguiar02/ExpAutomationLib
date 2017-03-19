using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Tokenizer
{
    public class NumberLiteralToken : IToken
    {
        public double Value { get; private set; }

        public static IToken ParseToken(string exp, ref int pos)
        {
            StringBuilder tokenBuilder = new StringBuilder();

            if (exp[pos] == '-')
                tokenBuilder.Append(exp[pos++]);

            while (char.IsDigit(exp[pos]))
            {
                tokenBuilder.Append(exp[pos++]);
            }
            if (exp[pos] == '.')
            {
                tokenBuilder.Append(exp[pos++]);

                while (char.IsDigit(exp[pos]))
                {
                    tokenBuilder.Append(exp[pos++]);
                }
            }
            double value = double.Parse(tokenBuilder.ToString(), CultureInfo.InvariantCulture);

            return new NumberLiteralToken { Value = value };
        }
    }
}
