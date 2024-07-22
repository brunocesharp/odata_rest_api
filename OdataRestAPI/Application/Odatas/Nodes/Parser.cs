using OdataRestAPI.Application.Odatas.AnaliseLexical;

namespace OdataRestAPI.Application.Odatas.Nodes
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _position = 0;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public Node Parse()
        {
            return ParseExpression();
        }

        private Node ParseExpression()
        {
            var counterErro = 0;
            var left = ParsePrimary();

            while (_position < _tokens.Count && counterErro < 100 && (_tokens[_position].Type == TokenType.LogicalOperator || _tokens[_position].Type == TokenType.Operator))
            {
                if (_tokens[_position].Type == TokenType.LogicalOperator)
                {
                    var logicalOperatorToken = _tokens[_position++];
                    var right = ParseExpression();

                    var logicalOperatorNode = new LogicalOperatorNode(logicalOperatorToken.Value);
                    logicalOperatorNode.Children.Add(left);
                    logicalOperatorNode.Children.Add(right);

                    left = logicalOperatorNode;
                }
                if (_tokens[_position].Type == TokenType.Operator)
                {
                    var operatorToken = _tokens[_position++];
                    var right = ParsePrimary();
                    var operatorNode = new BinaryOperatorNode(operatorToken.Value);
                    operatorNode.Children.Add(left);
                    operatorNode.Children.Add(right);
                    left = operatorNode;
                }

                counterErro++;

            }

            return left;
        }

        private Node ParsePrimary()
        {
            var token = _tokens[_position++];

            return token.Type switch
            {
                TokenType.LogicalOperator => new LogicalOperatorNode(token.Value),
                TokenType.String => new StringLiteralNode(token.Value),
                TokenType.Number => new StringLiteralNode(token.Value),
                TokenType.Identifier => new IdentifierNode(token.Value),
                TokenType.LeftParenthesis => ParseExpressionInsideParentheses(token.Value),
                _ => throw new Exception($"Unexpected token: {token.Type}")
            };
        }

        private Node ParseExpressionInsideParentheses(string value)
        {
            var expression = ParseExpression();
            if (_tokens[_position].Type != TokenType.RightParenthesis)
                throw new Exception("Expected closing parenthesis");

            _position++; // Skip the closing parenthesis
            return expression;
        }
    }
}
