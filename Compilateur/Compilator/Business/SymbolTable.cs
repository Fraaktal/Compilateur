﻿using System;
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
            if (Pile.Last().ContainsKey(ident))
            {
                var symbol = Pile.Last()[ident];
                string type = symbol.Type == Symbol.SymbolType.Function ? "fonction" : "variable";
                throw new Exception($"Erreur : une {type} de même nom est déja déclarée dans le bloc.");
            }

            Symbol s = new Symbol(ident);
            Pile.Last().Add(ident, s);
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

