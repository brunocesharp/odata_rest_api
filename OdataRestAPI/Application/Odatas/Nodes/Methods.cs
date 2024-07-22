using System.Linq.Expressions;

namespace OdataRestAPI.Application.Odatas.Nodes
{
    public static class Methods
    {
        public static Expression GenerateContainsExpression(Expression property, Expression value)
        {
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return Expression.Call(property, method, value);
        }
    }
}
