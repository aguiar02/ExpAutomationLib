using System.Text;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Tokenizer
{
    public class StringLiteralToken : IToken
    {
        public string Value { get; set; }

        public static StringLiteralToken ParseToken(string exp, ref int pos)
        {
            pos++;
            StringBuilder builder = new StringBuilder();
            int posOfAspas = exp.IndexOf('\'', pos);
            if (posOfAspas == -1)
                throw new SyntaxErrorException("Sequência de caracteres não terminada");
            StringLiteralToken result = new StringLiteralToken { Value = exp.Substring(pos, posOfAspas - pos) };
            pos = posOfAspas + 1;
            return result;
        }
    }
}
