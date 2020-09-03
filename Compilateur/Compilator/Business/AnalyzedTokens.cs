using System;
using System.Collections.Generic;
using System.Text;

namespace Compilateur.Compilator.Business
{
    public class AnalyzedTokens
    {
        public AnalyzedTokens(List<Token> tokens)
        {
            CurrentIndex = 0;
            Tokens = tokens;
        }

        private int CurrentIndex { get; set; }

        private List<Token> Tokens { get; set; }

        public Token Current()
        {

        }

        public void Forward()
        {

        }

        public void Accept(Token.TokensType type)
        {

        }

        public bool check(Token.TokensType type)
        {

        }
    }
}
