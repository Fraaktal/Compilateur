namespace Compilateur.Compilator.Business
{
    public class OperatorPriority
    {
        public OperatorPriority(Token.TokensType tokenType, int leftPriority, int rightPriority, Node.NodeType nodeType)
        {
            TokenType = tokenType;
            LeftPriority = leftPriority;
            RightPriority = rightPriority;
            NodeType = nodeType;
        }

        public Token.TokensType TokenType { get; set; }

        public int LeftPriority { get; set; }

        public int RightPriority { get; set; }

        public Node.NodeType NodeType { get; set; }
    }
}
