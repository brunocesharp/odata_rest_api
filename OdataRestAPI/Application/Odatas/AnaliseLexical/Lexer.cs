using System.Reflection;
using System.Text.RegularExpressions;

namespace OdataRestAPI.Application.Odatas.AnaliseLexical
{
    public class Lexer
    {
        private static readonly Regex Identifier = new Regex(@"^[a-zA-Z_]\w*");
        private static readonly Regex Number = new Regex(@"^\d+");
        private static readonly Regex String = new Regex(@"^'[^']*'");
        private static readonly Regex Operator = new Regex(@"^(ctn|eq|neq|gt|lt|gte|lte)");
        private static readonly Regex LogicalOperator = new Regex(@"^(and|or|not)");
        private static readonly Regex Whitespace = new Regex(@"^\s+");
        private static readonly Regex EndOfFile = new Regex(@"^$");
        private static readonly Regex ScapeBar = new Regex(@"^\\");
        private static readonly Regex LeftParenthesis = new Regex(@"^\(");
        private static readonly Regex RightParenthesis = new Regex(@"^\)");

        public List<Token> Tokenize<TEntity>(string input)
        {
            var tokens = new List<Token>();
            int position = 0;

            while (position < input.Length)
            {
                string remaingText = input.Substring(position);

                if (MatchToken(remaingText, Operator, out string op))
                {
                    tokens.Add(new Token(TokenType.Operator, op));
                    position += op.Length;
                }
                else if (MatchToken(remaingText, LogicalOperator, out string lo))
                {
                    tokens.Add(new Token(TokenType.LogicalOperator, lo));
                    position += lo.Length;
                }
                else if (MatchToken(remaingText, EndOfFile, out string eof))
                {
                    tokens.Add(new Token(TokenType.EndOfFile, eof));
                    position += eof.Length;
                }
                else if (MatchToken(remaingText, Whitespace, out string ws))
                {
                    position += ws.Length;
                }
                else if (MatchToken(remaingText, ScapeBar, out string sb))
                {
                    tokens.Add(new Token(TokenType.ScapeBar, sb));
                    position += sb.Length;
                }
                else if (MatchToken(remaingText, Number, out string number))
                {
                    tokens.Add(new Token(TokenType.Number, number));
                    position += number.Length;
                }
                else if (MatchToken(remaingText, LeftParenthesis, out string leftParenthesis))
                {
                    tokens.Add(new Token(TokenType.LeftParenthesis, leftParenthesis));
                    position += leftParenthesis.Length;
                }
                else if (MatchToken(remaingText, RightParenthesis, out string rightParenthesis))
                {
                    tokens.Add(new Token(TokenType.RightParenthesis, rightParenthesis));
                    position += rightParenthesis.Length;
                }
                else if (MatchToken(remaingText, String, out string str))
                {
                    tokens.Add(new Token(TokenType.String, str.Replace("'", "")));
                    position += str.Length;
                }
                else if (MatchTokenIdentifier<TEntity>(remaingText, Identifier, out string identifier))
                {
                    tokens.Add(new Token(TokenType.Identifier, identifier));
                    position += identifier.Length;
                }
                else
                {
                    throw new Exception($"Invalid token at position {position}");
                }
            }

            //add end of file token
            tokens.Add(new Token(TokenType.EndOfFile, string.Empty));

            return tokens;
        }

        private bool MatchToken(string input, Regex regex, out string value)
        {
            var match = regex.Match(input);

            if (match.Success)
            {
                value = match.Value;
                return true;
            }

            value = null;
            return false;
        }

        private bool MatchTokenIdentifier<TEntity>(string input, Regex regex, out string value)
        {
            var match = regex.Match(input);

            if (match.Success)
            {
                var property = PropertyExists(match.Value, typeof(TEntity));
                value = match.Value;
                return property;
            }

            value = null;
            return false;
        }

        private bool MatchTokenNumber<TEntity>(string input, Regex regex, out string value)
        {
            var match = regex.Match(input);

            if (match.Success && match.Value is int)
            {

                value = match.Value;
                return true;
            }

            value = null;
            return false;
        }

        public bool PropertyExists(string propertyName, Type type)
        {
            var prop = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            return prop != null;
        }
    }
}
