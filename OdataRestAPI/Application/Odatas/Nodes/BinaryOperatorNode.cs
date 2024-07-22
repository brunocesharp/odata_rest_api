using System.Linq.Expressions;

namespace OdataRestAPI.Application.Odatas.Nodes
{
    public class BinaryOperatorNode : Node
    {
        public string Operator { get; }
        public BinaryOperatorNode(string operatorValue)
        {
            Operator = operatorValue;
        }

        public override void Print(int indent = 0)
        {
            Console.WriteLine($@"{indent} {Operator}");
            foreach (var child in Children)
            {
                child.Print(indent + 2);
            }
        }

        public override Expression ToExpression(ParameterExpression parameter)
        {
            var left = Children[0].ToExpression(parameter);
            var right = Children[1].ToExpression(parameter, left);

            return Operator switch
            {
                "eq" => Expression.Equal(left, right),
                "neq" => Expression.NotEqual(left, right),
                "gt" => Expression.GreaterThan(left, right),
                "lt" => Expression.LessThan(left, right),
                "gte" => Expression.GreaterThanOrEqual(left, right),
                "lte" => Expression.LessThanOrEqual(left, right),
                "ctn" => Methods.GenerateContainsExpression(left, right),
                _ => throw new Exception($"Unknown operator: {Operator}")
            };
        }

        public override Expression ToExpression(ParameterExpression parameter, Expression left)
        {
            return ToExpression(parameter);
        }
    }
}
