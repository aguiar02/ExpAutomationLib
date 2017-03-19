using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Tokenizer
{
    public class TokenParser
    {
        private static int maxTokens = 2000;

        private const string OPERATORS = "[+\\-\\*/^=<>%]";
        private const char ENDEXPRESSION = '$';
        private static string[] keywords = { "e", "ou", "verdadeiro", "falso" };

        private int pos;
        private int tokens;

        private string expString;

        public TokenParser(string expString)
        {
            this.expString = expString;
        }

        public IToken GetNextToken()
        {
            if (tokens >= maxTokens)
                throw new ExpressionException("Número máximo de tokens excedido.");

            tokens++;

            if (pos == expString.Length)
                return new EndToken();

            SkipWhiteSpaces(expString, ref pos);

            if (char.IsDigit(expString[pos]))
            {
                return NumberLiteralToken.ParseToken(expString, ref pos);
            }
            else if (char.IsLetter(expString[pos]))
            {
                IdentifierToken itoken = IdentifierToken.ParseToken(expString, ref pos);
                switch (itoken.IdentifierName)
                {
                    case "e":
                        return new EToken();
                    case "ou":
                        return new OuToken();
                    case "verdadeiro":
                        return new BooleanLiteralToken(true);
                    case "falso":
                        return new BooleanLiteralToken(false);
                    default:
                        return itoken;
                }
            }
            else if (Regex.IsMatch(expString.Substring(pos, 1), OPERATORS))
            {
                if (expString[pos] == '=' && expString[pos + 1] == '>')
                {
                    pos += 2;
                    return new LambdaInvokeToken();
                }

                return OperatorToken.ParseToken(expString, ref pos);
            }
            else if (expString[pos] == '(')
            {
                pos++;
                return new OpenParenthesisToken();
            }
            else if (expString[pos] == ')')
            {
                pos++;
                return new CloseParenthesisToken();
            }
            else if (expString[pos] == ',')
            {
                pos++;
                return new CommaToken();
            }
            else if (expString[pos] == '.')
            {
                pos++;
                return new DotToken();
            }
            else if (expString[pos] == '\'')
            {
                return StringLiteralToken.ParseToken(expString, ref pos);
            }
            else if (expString[pos] == ENDEXPRESSION)
            {
                return new EndToken();
            }
            else
                throw new SyntaxErrorException("Invalid token: " + expString[pos]);
        }

        public static IEnumerable<string> Keywords
        {
            get { return keywords.AsEnumerable(); }
        }

        private static void SkipWhiteSpaces(string expString, ref int pos)
        {
            while (char.IsWhiteSpace(expString[pos]))
                pos++;
        }
    }
}
