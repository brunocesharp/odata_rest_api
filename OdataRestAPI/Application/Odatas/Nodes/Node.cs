using System.Linq.Expressions;

namespace OdataRestAPI.Application.Odatas.Nodes
{
    public abstract class Node
    {
        public List<Node> Children { get; } = new List<Node>();
        public abstract void Print(int indent = 0);
        public abstract Expression ToExpression(ParameterExpression parameter);
        public abstract Expression ToExpression(ParameterExpression parameter, Expression left);
    }
}
