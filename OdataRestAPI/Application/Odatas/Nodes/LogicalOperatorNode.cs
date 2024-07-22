using System.Linq.Expressions;

namespace OdataRestAPI.Application.Odatas.Nodes
{
    public class LogicalOperatorNode : Node
    {
        public string Value { get; }
        public LogicalOperatorNode(string logicalOperator)
        {
            Value = logicalOperator;
        }

        public override void Print(int indent = 0)
        {
            Console.WriteLine($@"{indent} {Value}");
            foreach (var child in Children)
            {
                child.Print(indent + 2);
            }
        }

        public override Expression ToExpression(ParameterExpression parameter)
        {
            var left = Children[0].ToExpression(parameter);
            var right = Children[1].ToExpression(parameter, left);

            return Value switch
            {
                "and" => Expression.AndAlso(left, right),
                "or" => Expression.OrElse(left, right),
                "not" => Expression.Not(left),
                _ => throw new Exception($"Unknown logical operator: {Value}")
            };
        }

        public override Expression ToExpression(ParameterExpression parameter, Expression left)
        {
            return ToExpression(parameter);
        }
    }
}
