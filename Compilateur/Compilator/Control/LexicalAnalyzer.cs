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

            Regex regex = new Regex("");

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
                            index += 2;
                            token.Type = Token.TokensType.Increment;
                        }
                        else
                        {
                            index += 1;
                            token.Type = Token.TokensType.Add;
                        }
                        break;
                    case '-':
                        if ((index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                            || (index < code.Length - 1 && code.ElementAt(index + 1).Equals('+'))
                            )
                        {
                            index += 2;
                            token.Type = Token.TokensType.Decrement;
                        }
                        else
                        {
                            index += 1;
                            token.Type = Token.TokensType.Sub;
                        }
                        break;
                    case '*':
                        index += 1;
                        token.Type = Token.TokensType.Mult;
                        break;
                    case '/':
                        index += 1;
                        token.Type = Token.TokensType.Div;
                        break;
                    case '%':
                        index += 1;
                        token.Type = Token.TokensType.Mod;
                        break;
                    case '<':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            index += 2;
                            token.Type = Token.TokensType.InferiorEq;
                        }
                        else
                        {
                            index += 1;
                            token.Type = Token.TokensType.Inferior;
                        }
                        break;
                    case '>':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            index += 2;
                            token.Type = Token.TokensType.SuperiorEq;
                        }
                        else
                        {
                            index += 1;
                            token.Type = Token.TokensType.Superior;
                        }
                        break;
                    case '=':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            index += 2;
                            token.Type = Token.TokensType.Equals;
                        }
                        else
                        {
                            index += 1;
                            token.Type = Token.TokensType.Affect;
                        }
                        break;
                    case '!':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('='))
                        {
                            index += 2;
                            token.Type = Token.TokensType.Different;
                        }
                        break;
                    case '(':
                        index += 1;
                        token.Type = Token.TokensType.OpenParenthese;
                        break;
                    case ')':
                        index += 1;
                        token.Type = Token.TokensType.ClosingParenthese;
                        break;
                    case ';':
                        index += 1;
                        token.Type = Token.TokensType.SemiColon;
                        break;
                    case 'i':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('f'))
                        {
                            index += 2;
                            token.Type = Token.TokensType.If;
                        }
                        else
                        {
                            //todo
                            index += 1;
                        }
                        break;
                    case 'f':
                        if (index < code.Length - 1 && code.ElementAt(index + 1).Equals('o'))
                        {
                            if (index < code.Length - 2 && code.ElementAt(index + 2).Equals('r'))
                            {
                                index += 3;
                                token.Type = Token.TokensType.For;
                            }
                        }
                        else
                        {
                            //todo
                            index += 1;
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
                                        index += 5;
                                        token.Type = Token.TokensType.While;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //todo
                            index += 1;
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
                                        index += 4;
                                        token.Type = Token.TokensType.Else;
                                    }
                                }
                            }
                            else
                            {
                                //todo
                                index += 1;
                            }
                        }

                        break;
                    default:
                        //todo
                        index += 1;
                        break;
                }

                tokens.Add(token);
            }

            tokens.Add(new Token(){Type = Token.TokensType.EOF});
            return tokens;
        }
    }
}
//Identificateur : nom variable
//const : valeur :)