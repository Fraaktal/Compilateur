namespace Compilateur.Compilator.Business
{
    public class Symbol
    {
        public enum SymbolType{Variable, Function}

        public Symbol(string identificator)
        {
            Identificator = identificator;
        }

        public string Identificator { get; set; }

        public SymbolType Type { get; set; }

        public int Slot { get; set; }
    }
}
