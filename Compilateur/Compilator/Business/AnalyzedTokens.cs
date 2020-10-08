using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

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

        public List<Token> Tokens { get; set; }

        public Token Current()
        {
            var t = Tokens.ElementAt(CurrentIndex);
            return t;
        }

        public void Forward()
        {
            CurrentIndex++;
        }

        public void Accept(Token.TokensType type)
        {
            if(Current().Type != type)
            {
                throw new Exception($"Erreur : {type} attendu mais {Current().Type} trouvé ligne {Current().LineNumber}");
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
