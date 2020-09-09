using System;
using System.Collections.Generic;

namespace Compilateur.Compilator.Business
{
    public class SymbolTable
    {

        public SymbolTable()
        {
            Pile = new Queue<Dictionary<string, Symbol>>();
        }
        public Queue<Dictionary<string, Symbol>> Pile { get; set; }
        void DebutBloc()
        {
            Pile.Enqueue(new Dictionary<string, Symbol>());
        }

        void FinBloc()
        {
            Pile.Dequeue();
        }

        Symbol Declarer(string ident)
        {
            if (Pile.Peek().ContainsKey(ident))
            {
                throw new Exception();
            }

            Symbol s = new Symbol(ident);
            Pile.Peek().Add(ident, s);
            return s;

        }

        Symbol Accepter(string ident)
        {
            foreach(var h in Pile)
            {
                if (h.ContainsKey(ident))
                {
                    return h[ident];
                }
            }

            throw new Exception();
        }

    }
}

