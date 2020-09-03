using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class SyntacticAnalyzer
    {
        public SyntacticAnalyzer(AnalyzedTokens tokens)
        {

        }

        public AnalyzedTokens Tokens { get; set; }

        public void Analyze()
        {

        }

        private Node Atom()
        {
            if (Tokens.check(Token.TokensType.OpenParenthese))
            {
                Node n = Expression(0);
                Tokens.Accept(Token.TokensType.ClosingParenthese);
                return n;
            }
            else if (Courant().type == token_const)
            {
                Node n = new Node(Node.NodeType.Const, Tokens.Current().LineNumber);
                n.ValeurEntiere = Tokens.Current().IntValue;
                Tokens.Forward();
                return n;
            }
            else
            {
                throw new Exception();
            }
        }

        private Node Expression(int minPriority)
        {

        }
    }
}
