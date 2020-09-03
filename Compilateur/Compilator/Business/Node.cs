using System;
using System.Collections.Generic;
using System.Text;

namespace Compilateur.Compilator.Business
{
    public class Node
    {
        public enum NodeType
        {
            Add,
            Increment,
            Sub,
            Decrement,
            Mult,
            Div,
            Mod,
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

        public Node()
        {
            
        }

        public Node(NodeType type, int line)
        {

        }
    }
}
