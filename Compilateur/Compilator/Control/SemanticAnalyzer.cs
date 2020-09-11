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

        public SymbolTable SymbolTable { get; set; }

        public void AnalyzeTree(Node N)
        {
            Analyze(N);
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
                case Node.NodeType.Fonction:
                    NbSlot = 0;
                    S = SymbolTable.Declarer(N.Identificator);
                    S.Type = Symbol.SymbolType.Function;
                    SymbolTable.DebutBloc();
                    foreach (var nChild in N.Children)
                    {
                        Analyze(nChild);
                    }
                    SymbolTable.FinBloc();
                    N.SlotCount = NbSlot;
                    break;
                case Node.NodeType.Appel:
                    S = SymbolTable.Acceder(N.Identificator);
                    if (S.Type != Symbol.SymbolType.Function)
                    {
                        throw new Exception();
                    }

                    foreach (var child in N.Children)
                    {
                        Analyze(child);
                    }
                    break;
            }
        }
    }
}
