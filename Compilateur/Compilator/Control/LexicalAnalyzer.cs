﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class LexicalAnalyzer
    {
        public LexicalAnalyzer(string code)
        {
            Regex regex = new Regex(@"\t|(\/\/(\w*\ *)*(?=\r\n))");
            code = regex.Replace(code, ""); //supprime les commentaires (pas supprimer les espaces !)
            Code = code;
        }

        public string Code { get; set; }

        //TODO mot commencant par if else,...
        
        public List<Token> AnalyzeCode()
        {
            List<Token> tokens = new List<Token>();

            int index = 0;
            int line = 1;

            while (index < Code.Length)
            {
                char c = Code.ElementAt(index);
                Token token = new Token();
                switch (c)
                {
                    case '+':
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('=')
                            || index < Code.Length -1 && Code.ElementAt(index + 1).Equals('+'))
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
                        if ((index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('='))
                            || (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('+'))
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
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('='))
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
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('='))
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
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('='))
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
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('='))
                        {
                            index += 2;
                            token.Type = Token.TokensType.Different;
                        }
                        else
                        {
                            index += 1;
                            token.Type = Token.TokensType.Not;
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
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('f'))
                        {
                            index += 2;
                            token.Type = Token.TokensType.If;
                        }
                        else
                        {
                            token = HandleIdentificatorOrConst(index, out index);
                        }
                        break;
                    case 'f':
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('o'))
                        {
                            if (index < Code.Length - 2 && Code.ElementAt(index + 2).Equals('r'))
                            {
                                index += 3;
                                token.Type = Token.TokensType.For;
                            }
                        }
                        else
                        {
                            token = HandleIdentificatorOrConst(index, out index);
                        }
                        break;
                    case 'w':
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('h'))
                        {
                            if (index < Code.Length - 2 && Code.ElementAt(index + 2).Equals('i'))
                            {
                                if (index < Code.Length - 3 && Code.ElementAt(index + 3).Equals('l'))
                                {
                                    if (index < Code.Length - 4 && Code.ElementAt(index + 4).Equals('e'))
                                    {
                                        index += 5;
                                        token.Type = Token.TokensType.While;
                                    }
                                }
                            }
                        }
                        else
                        {
                            token = HandleIdentificatorOrConst(index, out index);
                        }
                        break;
                    case 'e':
                        {
                            if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('l'))
                            {
                                if (index < Code.Length - 2 && Code.ElementAt(index + 2).Equals('s'))
                                {
                                    if (index < Code.Length - 3 && Code.ElementAt(index + 3).Equals('e'))
                                    {
                                        index += 4;
                                        token.Type = Token.TokensType.Else;
                                    }
                                }
                            }
                            else
                            {
                                token = HandleIdentificatorOrConst(index, out index);
                            }
                        }

                        break;
                    case ' ':
                    {
                        index += 1;
                        continue;
                    }
                    case '{':
                    {
                        token.Type = Token.TokensType.OpenAccolade;
                        index += 1;
                        break;
                    }
                    case '}':
                    {
                        token.Type = Token.TokensType.ClosingAccolade;
                        index += 1;
                        break;
                    }
                    case '\r':
                    {
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('\n'))
                        {
                            line++;
                            index += 2;
                            continue;
                        }
                        //n'est pas sensé arriver.
                        break;
                    }
                    default:
                        token = HandleIdentificatorOrConst(index, out index);
                        break;
                }

                token.LineNumber = line;
                tokens.Add(token);
            }

            tokens.Add(new Token(){Type = Token.TokensType.EOF});
            return tokens;
        }

        private Token HandleIdentificatorOrConst(int index, out int newIndex)
        {
            char c = Code.ElementAt(index);
            string val = "";
            Token token = new Token();
            if (IsLetter(c))
            {
                while (IsLetter(c) && index < Code.Length - 1)
                {
                    val += c;
                    index++;
                    c = Code.ElementAt(index);
                }

                token.Type = Token.TokensType.Identificator;
                token.StringValue = val;
            }
            else if(IsNumber(c))
            {
                while (IsNumber(c) && index < Code.Length - 1)
                {
                    val += c;
                    index++;
                    c = Code.ElementAt(index);
                }

                token.Type = Token.TokensType.Const;
                int.TryParse(val, out int valI);
                token.IntValue = valI;
            }

            newIndex = index;
            return token;
        }

        private bool IsNumber(in char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsLetter(char c)
        {
            return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z';
        }
    }
}