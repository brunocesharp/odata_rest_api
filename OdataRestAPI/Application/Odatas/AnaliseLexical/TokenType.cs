namespace OdataRestAPI.Application.Odatas.AnaliseLexical
{
    public enum TokenType
    {
        Identifier,
        Number,
        String,
        Operator,
        LogicalOperator,
        Whitespace,
        EndOfFile,
        ScapeBar,
        LeftParenthesis,
        RightParenthesis
    }
}
