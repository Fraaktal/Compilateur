using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Compilateur.Compilator.Business
{
    public class Token
    {
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
            UnNot,
            OpenParenthese,
            ClosingParenthese, 
            SemiColon, 
            Const, 
            Identificator,
            Int,
            If, 
            Else, 
            For, 
            While, 
            OpenAccolade,
            ClosingAccolade,
            EOF,
            Debug,
            UnSub,
            UnAdd,
            Break,
            Continue,
            Virgule,
            Return,
            Indirection,
            OpenBracket,
            ClosingBracket,
            Receive,
            Send
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
