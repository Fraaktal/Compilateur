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
                res.Add(new OperatorPriority(Token.TokensType.Add,40,41,Node.NodeType.Add));

                return res;
            }
        }

        public static bool IsLeftPrioritySuperirOrEqualsTo(Token.TokensType type, int priority)
        {
            var op = GetPriority(type);
            return op.LeftPriority >= priority;
        }

        public static OperatorPriority GetPriority(Token.TokensType type)
        {
            var res = Tab.FirstOrDefault(op => op.TokenType == type);
            if (res != null)
            {
                return res;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
