using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpCalculatorLib.Expression
{
    public static class ExpressionHelper
    {
        public static bool IsNumeric(Type type)
        {
            return type == typeof(decimal)
                || (type.IsPrimitive && type != typeof(char) && type != typeof(bool))
                || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    && IsNumeric(Nullable.GetUnderlyingType(type));
        }
    }
}
