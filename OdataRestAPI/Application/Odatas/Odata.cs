using OdataRestAPI.Application.Binders;
using OdataRestAPI.Application.Odatas.AnaliseLexical;
using OdataRestAPI.Application.Odatas.Nodes;
using System.Linq.Expressions;

namespace OdataRestAPI.Application.Odatas
{
    public class Odata<TEntity>
    {
        private readonly OdataRequest _request;
        public string _filters { get; set; }
        public Odata(string filters)
        {
            _filters = filters;
        }
        public Odata(OdataRequest request)
        {
            _request = request;
        }

        public Expression<Func<TEntity, bool>> BuildExpression()
        {
            var lexer = new Lexer();

            Parser parser = new Parser(lexer.Tokenize<TEntity>(_filters));

            var root = parser.Parse();

            var param = Expression.Parameter(typeof(TEntity), "entity");
            var expression = root.ToExpression(param);

            return Expression.Lambda<Func<TEntity, bool>>(expression, param);
        }
    }
}
