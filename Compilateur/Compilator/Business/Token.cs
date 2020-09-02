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
            Sub,
            Mult,
            Div, 
            Mod, 
            Inferior, 
            Superior, 
            InferiorEq, 
            SuperiorEq, 
            Equals, 
            Different, 
            And, 
            Or, 
            Not, 
            OpenParenthese,
            ClosingParenthese, 
            SemiColon, 
            Const, 
            Indent, 
            If, 
            Else, 
            For, 
            While, 
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
