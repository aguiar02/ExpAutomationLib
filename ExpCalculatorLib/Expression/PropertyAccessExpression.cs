using System;
using System.Globalization;
using System.Reflection;
using ExpCalculatorLib.Exceptions;

namespace ExpCalculatorLib.Expression
{
    public class PropertyAccessExpression : IExpression
    {
        public IExpression ObjectExpression { get; private set; }
        public string PropertyName { get; private set; }

        public PropertyInfo Property { get; private set; }

        public PropertyAccessExpression(IExpression objectExpression, string propertyIdentifier)
        {
            ObjectExpression = objectExpression;
            PropertyName = propertyIdentifier;
        }

        public Type ExpressionType
        {
            get
            {
                if (ObjectExpression == null || ObjectExpression.ExpressionType == null)
                    return null;

                Property = ObjectExpression.ExpressionType.GetProperty(PropertyName);
                if (Property == null)
                    return null;

                var propType = Property.PropertyType;

                return propType;
            }
        }

        public object Eval()
        {
            object obj = ObjectExpression.Eval();
            if (obj == null)
                throw new EvaluationErrorException(string.Format(CultureInfo.CurrentCulture, "Objeto de acesso a propriedade '{0}' retornou nulo", PropertyName), this);

            var propInfo = ObjectExpression.ExpressionType.GetProperty(PropertyName);

            return propInfo.GetValue(obj, new object[] { });
        }

        public void CheckSemantics(ParsingContext context)
        {
            ObjectExpression.CheckSemantics(context);
            PropertyInfo prop = ObjectExpression.ExpressionType.GetProperty(PropertyName);
            if (prop == null || !Attribute.IsDefined(prop, typeof(EnablePropertyAttribute)))
                throw new SemanticErrorException("Propriedade não encontrada: " + PropertyName, this);
        }

        public void Accept(Visitor.VisitorBase visitor)
        {
            if (visitor.VisitEnter(this))
            {
                ObjectExpression.Accept(visitor);
                visitor.Visit(this);
            }
            visitor.VisitLeave(this);
        }
    }
}
