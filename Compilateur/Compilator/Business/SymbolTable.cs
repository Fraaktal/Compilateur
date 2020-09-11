using System;
using System.Collections.Generic;
using System.Linq;

namespace Compilateur.Compilator.Business
{
    public class SymbolTable
    {

        public SymbolTable()
        {
            Pile = new List<Dictionary<string, Symbol>>();
        }

        public List<Dictionary<string, Symbol>> Pile { get; set; }

        public void DebutBloc()
        {
            Pile.Add(new Dictionary<string, Symbol>());
        }

        public void FinBloc()
        {
            Pile.RemoveAt(Pile.Count-1);
        }

        public Symbol Declarer(string ident)
        {
            if (Pile.First().ContainsKey(ident))
            {
                throw new Exception("Erreur : une variable du même nom est déja déclaré dans le bloc.");
            }

            Symbol s = new Symbol(ident);
            Pile.First().Add(ident, s);
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

            throw new Exception($"Erreur : {ident} n'existe pas dans le contexte actuel.");
        }

    }
}

