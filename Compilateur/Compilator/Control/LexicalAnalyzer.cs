using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class LexicalAnalyzer
    {
        public LexicalAnalyzer()
        {

        }

        public List<Token> AnalyzeCode(string code)
        {
            List<Token> tokens = new List<Token>();

            int index = 0;

            Regex regex = new Regex("^");

            while (index < code.Length)
            {
                char c = code.ElementAt(index);
                Token token = new Token();
                switch (c)
                {
                    case '+':
                        token.Type = Token.TokensType.Add;
                        break;
                    case '-':
                        token.Type = Token.TokensType.Sub;
                        break;
                    case '*':
                        token.Type = Token.TokensType.Mult;
                        break;
                    case '/':
                        token.Type = Token.TokensType.Div;
                        break;
                    case '%':
                        token.Type = Token.TokensType.Mod;
                        break;
                    case '<':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            token.Type = Token.TokensType.InferiorEq;
                        }
                        else
                        {
                            token.Type = Token.TokensType.Inferior;
                        }
                        break;
                    case '>':
                        break;
                    case '=':
                        break;
                    case '!':
                        break;
                    case '(':
                        break;
                    case ')':
                        break;
                    case ';':
                        break;
                    case 'i':
                        break;
                    case 'f':
                        break;
                    case 'w':
                        break;
                    case 'e':
                        break;
                    default:
                        break;
                }

                tokens.Add(token);
            }

            tokens.Add(new Token(){Type = Token.TokensType.EOF});
            return tokens;
        }
    }
}
