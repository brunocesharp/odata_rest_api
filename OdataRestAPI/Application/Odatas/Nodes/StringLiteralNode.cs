using System.Linq.Expressions;
using System.Reflection;

namespace OdataRestAPI.Application.Odatas.Nodes
{
    public class StringLiteralNode : Node
    {
        public string Value { get; }
        public StringLiteralNode(string value)
        {
            Value = value;
        }

        public override void Print(int indent = 0)
        {
            Console.WriteLine($@"{indent} {Value}");
        }

        public override Expression ToExpression(ParameterExpression parameter)
        {
            return Expression.Constant(Value);
        }

        public override Expression ToExpression(ParameterExpression parameter, Expression left)
        {
            if (left is MemberExpression && ((MemberExpression)left).Member is PropertyInfo)
            {
                var property = ((PropertyInfo)((MemberExpression)left).Member).PropertyType;

                if (property == typeof(int))
                    return Expression.Constant(int.Parse(Value), typeof(int));
                else if (property == typeof(decimal))
                    return Expression.Constant(decimal.Parse(Value), typeof(decimal));
                else if (property == typeof(DateTime))
                    return Expression.Constant(DateTime.Parse(Value), typeof(DateTime));
            }

            return ToExpression(parameter);
        }
    }
}
