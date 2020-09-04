using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Compilateur.Compilator.Business
{
    public class Token
    {
        public const string ADD = "+";
        public const string INCREMENT = "++";
        public const string SUB = "-";
        public const string DECREMENT = "--";
        public const string MULT = "*";
        public const string MULTIPLICATE = "*=";
        public const string DIV = "*=";
        public const string DIVIDE = "*/";
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
            EOF
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
