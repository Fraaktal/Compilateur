using System.Collections.Generic;
using System.Linq;

namespace Compilateur.Compilator.Business
{
    static class OperatorsPriorities
    {
        private static List<OperatorPriority> _tab;

        private static List<OperatorPriority> Tab
        {
            get
            {
                if (_tab == null)
                {
                    _tab = new List<OperatorPriority>();

                    _tab.Add(new OperatorPriority(Token.TokensType.Affect, 5, 5, Node.NodeType.Affect));

                    _tab.Add(new OperatorPriority(Token.TokensType.Or, 10, 11, Node.NodeType.Or));

                    _tab.Add(new OperatorPriority(Token.TokensType.And, 20, 21, Node.NodeType.And));
                    
                    _tab.Add(new OperatorPriority(Token.TokensType.Superior, 30, 31, Node.NodeType.Superior));
                    _tab.Add(new OperatorPriority(Token.TokensType.SuperiorEq, 30, 31, Node.NodeType.SuperiorEq));
                    _tab.Add(new OperatorPriority(Token.TokensType.Inferior, 30, 31, Node.NodeType.Inferior));
                    _tab.Add(new OperatorPriority(Token.TokensType.InferiorEq, 30, 31, Node.NodeType.InferiorEq));
                    _tab.Add(new OperatorPriority(Token.TokensType.Equals, 30, 31, Node.NodeType.Equals));
                    _tab.Add(new OperatorPriority(Token.TokensType.Different, 30, 31, Node.NodeType.Different));
                    
                    _tab.Add(new OperatorPriority(Token.TokensType.Add, 40, 41, Node.NodeType.Add));
                    _tab.Add(new OperatorPriority(Token.TokensType.Sub, 40, 41, Node.NodeType.Sub));
                    
                    _tab.Add(new OperatorPriority(Token.TokensType.Mult, 50, 51, Node.NodeType.Mult));
                    _tab.Add(new OperatorPriority(Token.TokensType.Div, 50, 51, Node.NodeType.Div));
                    _tab.Add(new OperatorPriority(Token.TokensType.Mod, 50, 51, Node.NodeType.Mod));
                    
                    _tab.Add(new OperatorPriority(Token.TokensType.UnSub, 0, 55, Node.NodeType.UnSub));
                    _tab.Add(new OperatorPriority(Token.TokensType.UnNot, 0, 55, Node.NodeType.UnNot));
                    _tab.Add(new OperatorPriority(Token.TokensType.UnAdd, 0, 55, Node.NodeType.UnAdd));
                    _tab.Add(new OperatorPriority(Token.TokensType.Indirection, 0, 55, Node.NodeType.Indirection));
                    
                    _tab.Add(new OperatorPriority(Token.TokensType.Pow, 60, 61, Node.NodeType.Pow));
                }

                return _tab;
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
