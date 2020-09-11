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
            Node res = Fonction();
            return res;
        }

        private Node Fonction()
        {
            Tokens.Accept(Token.TokensType.Int);
            Token T = Tokens.Current();
            Tokens.Accept((Token.TokensType.Identificator));
            Node N = new Node(Node.NodeType.Fonction, Tokens.Current().LineNumber);
            N.Identificator = T.StringValue;
            Tokens.Accept(Token.TokensType.OpenParenthese);

            while (Tokens.Current().Type != Token.TokensType.ClosingParenthese)
            {
                
                if (Tokens.Check(Token.TokensType.Int))
                {
                    if (Tokens.Current().Type == Token.TokensType.Identificator)
                    {
                        Node a = new Node(Node.NodeType.Declaration, Tokens.Current().LineNumber);
                        a.Identificator = Tokens.Current().StringValue;
                        N.AddChild(a);
                    }
                }

                Tokens.Forward();

                if (Tokens.Current().Type != Token.TokensType.Virgule)
                {
                    break;
                }
            }

            Tokens.Accept(Token.TokensType.ClosingParenthese);

            N.AddChild(Instruction());

            return N;

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
                N.AddChild(E1);
                return N;
            }
            else if (Tokens.Check(Token.TokensType.OpenAccolade))
            {
                line = Tokens.Current().LineNumber;
                N = new Node(Node.NodeType.Block, line);
                while (!Tokens.Check(Token.TokensType.ClosingAccolade))
                {
                    N.AddChild(Instruction());
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
                    N.AddChild(I2);
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
                Node forLoop = new Node(Node.NodeType.Loop, line);
                Node declaration = Expression(0);
                Tokens.Accept(Token.TokensType.SemiColon);
                Node test = Expression(0);
                Tokens.Accept(Token.TokensType.SemiColon);
                Node increment = Expression(0);
                Tokens.Accept(Token.TokensType.ClosingParenthese);
                Node forContent = Instruction();

                Node B1 = new Node(Node.NodeType.Block, line);
                Node B2 = new Node(Node.NodeType.Block, line);
                Node condition = new Node(Node.NodeType.Test, line);
                Node breakFor = new Node(Node.NodeType.Break, line);
                Node drop1 = new Node(Node.NodeType.Drop, line);
                Node drop2 = new Node(Node.NodeType.Drop, line);

                drop1.AddChild(declaration);
                drop2.AddChild(increment);
                B2.AddChildren(new List<Node>() {forContent, drop2});
                condition.AddChildren(new List<Node>() {test, B2, breakFor});
                forLoop.AddChild(condition);
                B1.AddChildren(new List<Node>() {drop1, forLoop});

                return B1;
            }
            else if (Tokens.Check(Token.TokensType.While))
            {
                line = Tokens.Current().LineNumber;
                Tokens.Accept(Token.TokensType.OpenParenthese);
                Node whileLoop = new Node(Node.NodeType.Loop, line);
                Node test = Expression(0);
                Tokens.Accept(Token.TokensType.ClosingParenthese);
                Node whileContent = Instruction();

                Node condition = new Node(Node.NodeType.Test, line);
                Node breakWhile = new Node(Node.NodeType.Break, line);

                condition.AddChildren(new List<Node>() {test, whileContent, breakWhile});
                whileLoop.AddChild(condition);

                return whileLoop;
            }

            line = Tokens.Current().LineNumber;
            N = new Node(Node.NodeType.Drop, line);
            E1 = Expression(0);
            Tokens.Accept(Token.TokensType.SemiColon);
            N.AddChild(E1);
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
                n.AddChild(arg);
                return n;
            }
            else if (Tokens.Check(Token.TokensType.Add))
            {
                Node n = new Node(Node.NodeType.UnAdd, Tokens.Current().LineNumber);
                Node arg = Expression(OperatorsPriorities.GetPriority(Token.TokensType.UnAdd).RightPriority);
                n.AddChild(arg);
                return n;
            }
            else if (Tokens.Check(Token.TokensType.Not))
            {
                Node n = new Node(Node.NodeType.UnNot, Tokens.Current().LineNumber);
                Node arg = Expression(OperatorsPriorities.GetPriority(Token.TokensType.UnNot).RightPriority);
                n.AddChild(arg);
                return n;
            }
            else if (Tokens.Current().Type == Token.TokensType.Identificator)
            {
                Token T = Tokens.Current();
                Tokens.Forward();
                if (Tokens.Check(Token.TokensType.OpenParenthese))
                {
                    Node n = new Node(Node.NodeType.Appel,T.LineNumber);
                    n.Identificator = T.StringValue;
                    while (Tokens.Current().Type != Token.TokensType.ClosingParenthese)
                    {
                        n.AddChild(Expression(0));
                        if (Tokens.Check(Token.TokensType.Virgule))
                        {
                            break;
                        }
                    }

                    Tokens.Accept(Token.TokensType.ClosingParenthese);
                    return n;
                }
                else
                {
                    Node n = new Node(Node.NodeType.Ref, T.LineNumber);
                    n.Identificator = T.StringValue;
                    return n;
                }
            }
            else
            {
                throw new Exception($"Erreur - Token inattendu Trouvé : {Tokens.Current().Type}");
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