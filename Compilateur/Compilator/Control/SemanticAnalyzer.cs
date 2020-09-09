using System;
using System.Collections.Generic;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class SemanticAnalyzer
    {
        private Queue<Dictionary<string,Symbol>> Pile { get; set; }

        public SemanticAnalyzer()
        {
            NbSlot = 0;   
        }

        private int NbSlot { get; set; }

        public void Analyze(Node N)
        {
            Symbol S;
            switch (N.Type)
            {
                default:
                    foreach(var child in N.Children)
                    {
                        Analyze(child);
                    }
                    break;
                case Node.NodeType.Block:
                    DebutBloc();
                    foreach(var child in N.Children)
                    {
                        Analyze(child);
                    }
                    FinBlock();
                    break;
                case Node.NodeType.Declaration:
                    S = Declarer(N.Identificator);
                    S.Type = Symbol.SymbolType.Variable;
                    S.Slot = NbSlot;
                    NbSlot++;
                    break;
                case Node.NodeType.Ref:
                    S = Acceder(N.Identificator);
                    if(S.Type != Symbol.SymbolType.Variable)
                    {
                        throw new Exception();
                    }
                    N.Slot = S.Slot;
                    break;
            }
        }
    }
}
