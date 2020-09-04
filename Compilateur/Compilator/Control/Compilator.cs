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
            FileInfo fi = new FileInfo(path);
            string code = "";
            try
            {
                code = File.ReadAllText(fi.FullName);
            }
            catch (Exception e)
            {
                throw new Exception($"Erreur chemin incorrect ou fichier inaccessible : {e.Message}");
            }

            // On effectue l'analyse lexicale
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(code);
            var analyzedTokens = lexicalAnalyzer.AnalyzeCode();

            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer(analyzedTokens);
            var tree = syntacticAnalyzer.Analyze();

            CodeGenerator codeGenerator = new CodeGenerator();
            var generatedCode = codeGenerator.GenerateCode(tree);
            generatedCode = ".start\n" + generatedCode;
            generatedCode += "dbg\nhalt\n";

            string resPath = fi.DirectoryName + @"\generatedCode.code";
            if (File.Exists(resPath))
            {
                File.Delete(resPath);
            }
            File.WriteAllText(resPath, generatedCode);
        }
    }
}
