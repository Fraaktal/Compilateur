using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class LexicalAnalyzer
    {
        public LexicalAnalyzer(string code)
        {
            Regex regex = new Regex(@"\t|(\/\/(.*)(?=\r\n))"); //lookahead pas forcément utile. todo
            code = regex.Replace(code, ""); //supprime les commentaires (pas supprimer les espaces !)
            Code = code;
        }

        public string Code { get; set; }

        //TODO mot commencant par if else,...

        public AnalyzedTokens AnalyzeCode()
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
                        index += 1;
                        token.Type = Token.TokensType.Add;
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
                    case '^':
                        index += 1;
                        token.Type = Token.TokensType.Pow;
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
                    case ',':
                        index += 1;
                        token.Type = Token.TokensType.Virgule;
                        break;
                    case ' ':
                        index += 1;
                        continue;
                    case '{':
                        token.Type = Token.TokensType.OpenAccolade;
                        index += 1;
                        break;
                    case '}':
                        token.Type = Token.TokensType.ClosingAccolade;
                        index += 1;
                        break;
                    case '[':
                        token.Type = Token.TokensType.OpenBracket;
                        index += 1;
                        break;
                    case ']':
                        token.Type = Token.TokensType.ClosingBracket;
                        index += 1;
                        break;
                    case '\r':
                        if (index < Code.Length - 1 && Code.ElementAt(index + 1).Equals('\n'))
                        {
                            line++;
                            index += 2;
                            continue;
                        }
                        break;
                    default:
                        token = HandleIdentificatorOrConst(ref index);
                        break;
                }

                token.LineNumber = line;
                tokens.Add(token);
            }

            tokens.Add(new Token() {Type = Token.TokensType.EOF});
            return new AnalyzedTokens(tokens);
        }


        //public AnalyzedTokens AnalyzeCodeN()
        //{
        //    List<Token> tokens = new List<Token>();

        //    int index = 0;
        //    int line = 1;

        //    while (index < Code.Length)
        //    {
        //        char c = Code.ElementAt(index);
        //        Token token = new Token();

        //        token.Type = HandleChar(c, ref index);

        //        tokens.Add(token);
        //    }

        //    return new AnalyzedTokens(tokens);
        //}

        private Token.TokensType HandleChar(in char c, ref int index)
        {
            return Token.TokensType.Add;
        }

        private Token HandleConst(ref int index)
        {
            char c = Code.ElementAt(index);
            string val = "";
            Token token = new Token();

            while (IsNumber(c) && index < Code.Length - 1)
            {
                val += c;
                index++;
                c = Code.ElementAt(index);
            }

            token.Type = Token.TokensType.Const;
            int.TryParse(val, out int valI);
            token.IntValue = valI;

            return token;
        }

        private Token HandleIdentificator(ref int index)
        {
            char c = Code.ElementAt(index);
            string val = "";
            Token token = new Token();

            while (IsLetter(c) && index < Code.Length - 1)
            {
                val += c;
                index++;
                c = Code.ElementAt(index);
            }

            switch (val)
            {
                case "if":
                    token.Type = Token.TokensType.If;
                    break;
                case "for":
                    token.Type = Token.TokensType.For;
                    break;
                case "while":
                    token.Type = Token.TokensType.While;
                    break;
                case "else":
                    token.Type = Token.TokensType.Else;
                    break; 
                case "continue":
                    token.Type = Token.TokensType.Continue;
                    break; 
                case "break":
                    token.Type = Token.TokensType.Break;
                    break;
                case "int":
                    token.Type = Token.TokensType.Int;
                    break;
                case "debug":
                    token.Type = Token.TokensType.Debug;
                    break;
                case "return":
                    token.Type = Token.TokensType.Return;
                    break;
                case "receive":
                    token.Type = Token.TokensType.Receive;
                    break;
                case "send":
                    token.Type = Token.TokensType.Send;
                    break;
                default:
                    token.Type = Token.TokensType.Identificator;
                    token.StringValue = val;
                    break;
            }


            return token;
        }

        private Token HandleIdentificatorOrConst(ref int index)
        {
            if (IsNumber(Code.ElementAt(index)))
            {
                return HandleConst(ref index);
            }

            if (IsLetter(Code.ElementAt(index)))
            {
                return HandleIdentificator(ref index);
            }

            return null;
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