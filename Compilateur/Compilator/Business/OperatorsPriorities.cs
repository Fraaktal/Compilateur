using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilateur.Compilator.Business
{
    static class OperatorsPriorities
    {
        private static List<OperatorPriority> Tab
        {
            get
            {
                List<OperatorPriority> res = new List<OperatorPriority>();
                res.Add(new OperatorPriority(Token.TokensType.Or, 10, 11, Node.NodeType.Or));

                res.Add(new OperatorPriority(Token.TokensType.And, 20, 21, Node.NodeType.And));

                res.Add(new OperatorPriority(Token.TokensType.Superior, 30, 31, Node.NodeType.Superior));
                res.Add(new OperatorPriority(Token.TokensType.SuperiorEq, 30, 31, Node.NodeType.SuperiorEq));
                res.Add(new OperatorPriority(Token.TokensType.Inferior, 30, 31, Node.NodeType.Inferior));
                res.Add(new OperatorPriority(Token.TokensType.InferiorEq, 30, 31, Node.NodeType.InferiorEq));
                res.Add(new OperatorPriority(Token.TokensType.Equals, 30, 31, Node.NodeType.Equals));
                res.Add(new OperatorPriority(Token.TokensType.Different, 30, 31, Node.NodeType.Different));

                res.Add(new OperatorPriority(Token.TokensType.Add,40,41,Node.NodeType.Add));
                res.Add(new OperatorPriority(Token.TokensType.Sub,40,41,Node.NodeType.Sub));

                res.Add(new OperatorPriority(Token.TokensType.Mult,50,51,Node.NodeType.Mult));
                res.Add(new OperatorPriority(Token.TokensType.Div,50,51,Node.NodeType.Div));
                res.Add(new OperatorPriority(Token.TokensType.Mod,50,51,Node.NodeType.Mod));

                res.Add(new OperatorPriority(Token.TokensType.Pow,60,61,Node.NodeType.Pow));


                return res;
            }
        }

        public static bool IsLeftPrioritySuperirOrEqualsTo(Token.TokensType type, int priority)
        {
            var op = GetPriority(type);
            if (op == null)
            {
                return false;
            }
            return op.LeftPriority >= priority;
        }

        public static OperatorPriority GetPriority(Token.TokensType type)
        {
            var res = Tab.FirstOrDefault(op => op.TokenType == type);
            return res;

        }
    }
}
