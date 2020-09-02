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
                        if ((index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                            || (index < code.Length -1 && code.ElementAt(index + 1).Equals('+'))
                            )
                        {
                            token.Type = Token.TokensType.Increment;
                        }
                        else
                        {
                            token.Type = Token.TokensType.Add;
                        }
                        break;
                    case '-':
                        if ((index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                            || (index < code.Length - 1 && code.ElementAt(index + 1).Equals('+'))
                            )
                        {
                            token.Type = Token.TokensType.Decrement;
                        }
                        else
                        {
                            token.Type = Token.TokensType.Sub;
                        }
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
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            token.Type = Token.TokensType.SuperiorEq;
                        }
                        else
                        {
                            token.Type = Token.TokensType.Superior;
                        }
                        break;
                    case '=':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            token.Type = Token.TokensType.Equals;
                        }
                        else
                        {
                            token.Type = Token.TokensType.Affect;
                        }
                        break;
                    case '!':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            token.Type = Token.TokensType.Different;
                        }
                        break;
                    case '(':
                        token.Type = Token.TokensType.OpenParenthese;
                        break;
                    case ')':
                        token.Type = Token.TokensType.ClosingParenthese;
                        break;
                    case ';':
                        token.Type = Token.TokensType.SemiColon;
                        break;
                    case 'i':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('f'))
                        {
                            token.Type = Token.TokensType.If;
                        }
                        break;
                    case 'f':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('o'))
                        {
                            if (index < code.Length - 2 && code.ElementAt(index + 2).Equals('r'))
                            {
                                token.Type = Token.TokensType.For;
                            }
                        }
                        break;
                    case 'w':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('h'))
                        {
                            if (index < code.Length - 2 && code.ElementAt(index + 2).Equals('i'))
                            {
                                if (index < code.Length - 3 && code.ElementAt(index + 3).Equals('l'))
                                {
                                    if (index < code.Length - 4 && code.ElementAt(index + 4).Equals('e'))
                                    {
                                        token.Type = Token.TokensType.While;
                                    }
                                }
                            }
                        }
                        break;
                    case 'e':
                        {
                            if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('l'))
                            {
                                if (index < code.Length - 2 && code.ElementAt(index + 2).Equals('s'))
                                {
                                    if (index < code.Length - 3 && code.ElementAt(index + 3).Equals('e'))
                                    {
                                        token.Type = Token.TokensType.Else;
                                    }
                                }
                            }
                        }

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
