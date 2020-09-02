using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compilateur.Compilator.Control
{
    public class Compilator
    {
        public void Compile(string path)
        {
            // On lis le fichier
            string code = File.ReadAllText(path);

            // On effectue l'analyse lexicale
            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            var tokens = analyzer.AnalyzeCode(code);
        }
    }
}
