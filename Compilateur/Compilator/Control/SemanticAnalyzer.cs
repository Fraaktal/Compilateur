using System;
using System.Collections.Generic;
using System.Linq;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class SemanticAnalyzer
    {
        private Queue<Dictionary<string,Symbol>> Pile { get; set; }

        public SemanticAnalyzer()
        {
            void DebutBloc()
            {
                Pile.Enqueue(new Dictionary<string, Symbol>());
            }

            void FinBloc()
            {
                Pile.Dequeue();
            }

            Symbol Declarer(String ident)
            {
                if (Pile.Peek().ContainsKey(ident))
                {
                    //Erreur
                }
                else
                {
                    Symbol s = new Symbol(ident);
                    Pile.Peek().Add(ident, s);
                    return s;
                }
            }

            Symbol Accepter(String ident)
            {
                
            }

        }
    }
}
