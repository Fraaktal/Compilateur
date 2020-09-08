using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Compilateur.Compilator.Business
{
    public class Token
    {
        //TODO objet gros avec symbole node token type priorite et tout et tout pour faire de la detection et génération facile
        //todo utiliser des flags
        public static Dictionary<string,TokensType> TokenTypeByValue
        {
            get 
            {
                Dictionary<string, TokensType> _res = new Dictionary<string, TokensType>();
                _res.Add("+",TokensType.Add);
                _res.Add("++",TokensType.Increment);
                _res.Add("-",TokensType.Sub);
                _res.Add("--",TokensType.Decrement);
                _res.Add("*",TokensType.Mult);
                _res.Add("/",TokensType.Div);
                _res.Add("%",TokensType.Mod);
                _res.Add("^",TokensType.Pow);
                _res.Add("<",TokensType.Inferior);
                _res.Add("<=",TokensType.InferiorEq);
                _res.Add(">",TokensType.Superior);
                _res.Add(">=",TokensType.SuperiorEq);
                _res.Add(">=",TokensType.SuperiorEq);
                _res.Add("=",TokensType.Affect);
                _res.Add("==",TokensType.Equals);
                _res.Add("!=",TokensType.Different);
                _res.Add("&&",TokensType.And);
                _res.Add("||",TokensType.Or);
                _res.Add("!",TokensType.Not);
                _res.Add("(",TokensType.OpenParenthese);
                _res.Add(")",TokensType.ClosingParenthese);
                _res.Add(";",TokensType.SemiColon);
                _res.Add("",TokensType.Const);
                _res.Add("",TokensType.Identificator);
                _res.Add("if",TokensType.If);
                _res.Add("else",TokensType.Else);
                _res.Add("for",TokensType.For);
                _res.Add("while",TokensType.While);
                _res.Add("{",TokensType.OpenAccolade);
                _res.Add("}",TokensType.ClosingAccolade);
                _res.Add("",TokensType.EOF);
                return _res;
            }
        }
        public enum TokensType
        {
            Add, 
            Increment,
            Sub,
            Decrement,
            Mult,
            Div, 
            Mod, 
            Pow,
            Inferior, 
            Superior, 
            InferiorEq, 
            SuperiorEq,
            Affect,
            Equals, 
            Different, 
            And, 
            Or, 
            Not, 
            OpenParenthese,
            ClosingParenthese, 
            SemiColon, 
            Const, 
            Identificator, 
            If, 
            Else, 
            For, 
            While, 
            OpenAccolade,
            ClosingAccolade,
            EOF,
            Debug
        }

        public Token()
        {

        }

        public TokensType Type { get; set; }

        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public int LineNumber { get; set; }
    }
}
