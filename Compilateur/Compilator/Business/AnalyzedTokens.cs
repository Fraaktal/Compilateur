using System;
using System.Collections.Generic;
using System.Linq;
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
            return Tokens.ElementAt(CurrentIndex);

        }

        public void Forward()
        {
            CurrentIndex++;
        }

        public void Accept(Token.TokensType type)
        {
            if(Current().Type != type)
            {
                //TODO Erreur Fatale
            }
            Forward();
        }

        public bool Check(Token.TokensType type)
        {
            if(Current().Type == type)
            {
                Forward();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
