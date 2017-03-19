using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Tokenizer
{
    public class IdentifierToken : IToken
    {
        public string IdentifierName { get; private set; }

        public int StartPos { get; internal set; }

        public static IdentifierToken ParseToken(string exp, ref int pos)
        {
            IdentifierToken token = new IdentifierToken();
            token.StartPos = pos;
            StringBuilder tokenBuilder = new StringBuilder();
            if (char.IsLetter(exp[pos]))
            {
                tokenBuilder.Append(exp[pos++]);
            }
            while (char.IsLetterOrDigit(exp[pos]))
            {
                tokenBuilder.Append(exp[pos++]);
            }
            token.IdentifierName = tokenBuilder.ToString();
            return token;
        }
    }
}
