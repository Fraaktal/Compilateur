using System;
using System.Collections.Generic;
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
    }
}
