using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable All

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
            OpenAccolade,
            ClosingAccolade,
            EOF,
            Pow,
            Drop,
            Break,
            Continue,
            Loop,
            Test,
            Block,
            Debug,
            Declaration,
            Ref,
            Appel,
            Fonction,
            Indirection,
            Return,
            Send,
            Receive
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

        public int SlotCount { get; set; }

        public void AddChildren(params Node[] nodes)
        {
            Children.AddRange(nodes);
        }
    }
}
