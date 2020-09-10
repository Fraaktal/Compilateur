using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class SyntacticAnalyzer
    {
        public SyntacticAnalyzer(AnalyzedTokens tokens)
        {
            Tokens = tokens;
        }

        public AnalyzedTokens Tokens { get; set; }

        public Node Analyze()
        {
            Node res = Instruction();
            Tokens.Accept(Token.TokensType.EOF);
            return res;
        }

        private Node Instruction()
        {
            Node N, E1;
            int line = 0;

            if (Tokens.Check(Token.TokensType.Debug))
            {
                line = Tokens.Current().LineNumber;
                E1 = Expression(0);
                Tokens.Accept(Token.TokensType.SemiColon);
                N = new Node(Node.NodeType.Debug, line);
                N.AddChildren(new List<Node>() {E1});
                return N;
            }
            else if (Tokens.Check(Token.TokensType.OpenAccolade))
            {
                line = Tokens.Current().LineNumber;
                N = new Node(Node.NodeType.Block, line);
                while (!Tokens.Check(Token.TokensType.ClosingAccolade))
                {
                    N.AddChildren(new List<Node>() {Instruction()});
                }

                return N;
            }
            else if (Tokens.Check(Token.TokensType.If))
            {
                line = Tokens.Current().LineNumber;
                Tokens.Accept(Token.TokensType.OpenParenthese);
                E1 = Expression(0);
                Tokens.Accept(Token.TokensType.ClosingParenthese);
                Node I1 = Instruction();
                N = new Node(Node.NodeType.Test, line);
                N.AddChildren(new List<Node>() {E1, I1});
                if (Tokens.Check(Token.TokensType.Else))
                {
                    Node I2 = Instruction();
                    N.AddChildren(new List<Node>() {I2});
                }

                return N;
            }
            else if (Tokens.Check(Token.TokensType.Int))
            {
                if (Tokens.Current().Type == Token.TokensType.Identificator)
                {
                    line = Tokens.Current().LineNumber;
                    N = new Node(Node.NodeType.Declaration, line);
                    N.Identificator = Tokens.Current().StringValue;
                    Tokens.Forward();
                    Tokens.Accept(Token.TokensType.SemiColon);
                    return N;
                }
                else
                {
                    throw new Exception();
                }

            }
            else if (Tokens.Check(Token.TokensType.For))
            {
                line = Tokens.Current().LineNumber;
                Tokens.Accept(Token.TokensType.OpenParenthese);
                Node forLoop = new Node(Node.NodeType.For, line);
                Node declaration = Expression(0);
                Tokens.Accept(Token.TokensType.SemiColon);
                Node test = Expression(0);
                Tokens.Accept(Token.TokensType.SemiColon);
                Node increment = Expression(0);
                Tokens.Accept(Token.TokensType.ClosingParenthese);
                Node forContent = Instruction();

                Node B1 = new Node(Node.NodeType.Block, line);
                Node B2 = new Node(Node.NodeType.Block, line);
                Node condition = new Node(Node.NodeType.Condition, line);
                Node breakFor = new Node(Node.NodeType.Break, line);
                Node drop1 = new Node(Node.NodeType.Drop, line);
                Node drop2 = new Node(Node.NodeType.Drop, line);

                drop1.AddChildren(new List<Node>() {declaration});
                drop2.AddChildren(new List<Node>() {increment});
                B2.AddChildren(new List<Node>() {forContent, drop2});
                condition.AddChildren(new List<Node>() {test, B2, breakFor});
                forLoop.AddChildren(new List<Node>() {condition});
                B1.AddChildren(new List<Node>() {drop1, forLoop});

                return B1;
            }
            else if (Tokens.Check(Token.TokensType.While))
            {
                line = Tokens.Current().LineNumber;
                Tokens.Accept(Token.TokensType.OpenParenthese);
                Node whileLoop = new Node(Node.NodeType.While, line);
                Node test = Expression(0);
                Tokens.Accept(Token.TokensType.ClosingParenthese);
                Node whileContent = Instruction();

                Node condition = new Node(Node.NodeType.Condition, line);
                Node breakWhile = new Node(Node.NodeType.Break, line);

                condition.AddChildren(new List<Node>() {test, whileContent, breakWhile});
                whileLoop.AddChildren(new List<Node>() {condition});

                return whileLoop;
            }

            line = Tokens.Current().LineNumber;
            N = new Node(Node.NodeType.Drop, line);
            E1 = Expression(0);
            Tokens.Accept(Token.TokensType.SemiColon);
            N.AddChildren(new List<Node>() {E1});
            return N;
        }

        private Node Atom()
        {
            if (Tokens.Check(Token.TokensType.OpenParenthese))
            {
                Node n = Expression(0);
                Tokens.Accept(Token.TokensType.ClosingParenthese);
                return n;
            }
            else if (Tokens.Current().Type == Token.TokensType.Const)
            {
                Node n = new Node(Node.NodeType.Const, Tokens.Current().LineNumber);
                n.IntValue = Tokens.Current().IntValue;
                Tokens.Forward();
                return n;
            }
            else if (Tokens.Check(Token.TokensType.Sub))
            {
                Node n = new Node(Node.NodeType.UnSub, Tokens.Current().LineNumber);
                Node arg = Expression(OperatorsPriorities.GetPriority(Token.TokensType.UnSub).RightPriority);
                n.AddChildren(new List<Node>() {arg});
                return n;
            }
            else if (Tokens.Check(Token.TokensType.Add))
            {
                Node n = new Node(Node.NodeType.UnAdd, Tokens.Current().LineNumber);
                Node arg = Expression(OperatorsPriorities.GetPriority(Token.TokensType.UnAdd).RightPriority);
                n.AddChildren(new List<Node>() {arg});
                return n;
            }
            else if (Tokens.Check(Token.TokensType.Not))
            {
                Node n = new Node(Node.NodeType.UnNot, Tokens.Current().LineNumber);
                Node arg = Expression(OperatorsPriorities.GetPriority(Token.TokensType.UnNot).RightPriority);
                n.AddChildren(new List<Node>() {arg});
                return n;
            }
            else if (Tokens.Current().Type == Token.TokensType.Identificator)
            {
                Node n = new Node(Node.NodeType.Ref, Tokens.Current().LineNumber);
                n.Identificator = Tokens.Current().StringValue;
                Tokens.Forward();
                return n;
            }
            else
            {
                throw new Exception($"Erreur - Token innatendu ligne . Trouvé : {Tokens.Current().Type}");
            }
        }

        private Node Expression(int minPriority)
        {
            Node n = Atom();
            while (OperatorsPriorities.IsLeftPrioritySuperirOrEqualsTo(Tokens.Current().Type, minPriority))
            {
                var op = OperatorsPriorities.GetPriority(Tokens.Current().Type);
                Tokens.Forward();
                int line = Tokens.Current().LineNumber;
                Node a = Expression(op.RightPriority);
                Node tmp = new Node(op.NodeType, line);
                tmp.AddChildren(new List<Node> {n, a});
                n = tmp;
            }

            return n;
        }
    }
}