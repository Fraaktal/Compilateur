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

        public void DebutBloc()
        {
            Pile.Enqueue(new Dictionary<string, Symbol>());
        }

        public void FinBloc()
        {
            Pile.Dequeue();
        }

        public Symbol Declarer(string ident)
        {
            if (Pile.Peek().ContainsKey(ident))
            {
                throw new Exception();
            }

            Symbol s = new Symbol(ident);
            Pile.Peek().Add(ident, s);
            return s;

        }

        public Symbol Acceder(string ident)
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

