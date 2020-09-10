using System;
using System.Collections.Generic;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class SemanticAnalyzer
    {
        public SemanticAnalyzer()
        {
            SymbolTable = new SymbolTable();
            NbSlot = 0;   
        }

        private int NbSlot { get; set; }

        private SymbolTable SymbolTable { get; set; }

        public int AnalyzeTree(Node N)
        {
            Analyze(N);
            return NbSlot;
        }

        private void Analyze(Node N)
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
                    SymbolTable.DebutBloc();
                    foreach(var child in N.Children)
                    {
                        Analyze(child);
                    }
                    SymbolTable.FinBloc();
                    break;
                case Node.NodeType.Declaration:
                    S = SymbolTable.Declarer(N.Identificator);
                    S.Type = Symbol.SymbolType.Variable;
                    S.Slot = NbSlot;
                    NbSlot++;
                    break;
                case Node.NodeType.Ref:
                    S = SymbolTable.Acceder(N.Identificator);
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
