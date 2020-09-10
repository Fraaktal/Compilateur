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
            UnAdd,
            Increment,
            Sub,
            UnSub,
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
            UnNot,
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
            Pow,
            Drop,
            Test,
            Block,
            Debug,
            Declaration,
            Ref
        }

        public Node()
        {
            Children = new List<Node>();
        }

        public Node(NodeType type, int line) :this()
        {
            Type = type;
            LineNumber = line;
        }

        public NodeType Type { get; set; }

        public int LineNumber { get; set; }

        public List<Node> Children { get; set; }

        public int IntValue { get; set; }

        public string Identificator { get; set; }

        public int  Slot { get; internal set; }

        public void AddChildren(List<Node> nodes)
        {
            Children.AddRange(nodes);
        }
    }
}
