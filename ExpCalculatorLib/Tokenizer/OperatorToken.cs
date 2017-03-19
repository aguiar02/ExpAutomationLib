using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Tokenizer
{
    public abstract class OperatorToken : IToken
    {
        public static IToken ParseToken(string exp, ref int pos)
        {
            char c = exp[pos++];
            switch (c)
            {
                case '+':
                    return new PlusToken();
                case '-':
                    return new MinusToken();
                case '*':
                    return new MultiplyToken();
                case '/':
                    return new DivideToken();
                case '%':
                    return new ModToken();
                case '^':
                    return new ExponentationToken();
                case '=':
                    return new EqualsToken();
                case '<':
                    c = exp[pos++];
                    switch (c)
                    {
                        case '>':
                            return new DifferentToken();
                        case '=':
                            return new LessThanOrEqualToken();
                        default:
                            pos--;
                            return new LessThanToken();
                    }
                case '>':
                    c = exp[pos++];
                    switch (c)
                    {
                        case '=':
                            return new GreaterThanOrEqualToken();
                        default:
                            pos--;
                            return new GreaterThanToken();
                    }

                default:
                    throw new SyntaxErrorException("Operador inválido: " + c);
            }
        }
    }

    public class PlusToken : OperatorToken { }
    public class MinusToken : OperatorToken { }
    public class MultiplyToken : OperatorToken { }
    public class DivideToken : OperatorToken { }
    public class ModToken : OperatorToken { }
    public class ExponentationToken : OperatorToken { }
    public class EqualsToken : OperatorToken { }
    public class DifferentToken : OperatorToken { }
    public class LessThanToken : OperatorToken { }
    public class GreaterThanToken : OperatorToken { }
    public class LessThanOrEqualToken : OperatorToken { }
    public class GreaterThanOrEqualToken : OperatorToken { }
}
