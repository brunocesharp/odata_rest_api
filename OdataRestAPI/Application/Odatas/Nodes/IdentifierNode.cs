using System.Linq.Expressions;

namespace OdataRestAPI.Application.Odatas.Nodes
{
    public class IdentifierNode : Node
    {
        public string Identifier { get; }
        public IdentifierNode(string identifier)
        {
            Identifier = identifier;
        }

        public override void Print(int indent = 0)
        {
            Console.WriteLine($@"{indent} {Identifier}");
        }

        public override Expression ToExpression(ParameterExpression parameter)
        {
            return Expression.Property(parameter, Identifier);
        }

        public override Expression ToExpression(ParameterExpression parameter, Expression left)
        {
            return ToExpression(parameter);
        }
    }
}
