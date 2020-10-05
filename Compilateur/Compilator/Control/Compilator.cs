using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Compilateur.Compilator.Business;

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

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Compilateur.Runtime.runtime.c";
            string runtime = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    runtime = reader.ReadToEnd();
                }
            }
            

            string generatedCode = GenerateCode(runtime);
            generatedCode += GenerateCode(code);

            generatedCode += ".start\n";
            generatedCode += "prep init\ncall 0\n";
            generatedCode += "prep main\ncall 0\n";
            generatedCode += "halt\n";

            string resPath = fi.DirectoryName + @"\generatedCode.code";
            if (File.Exists(resPath))
            {
                File.Delete(resPath);
            }
            File.WriteAllText(resPath, generatedCode);
        }

        private string GenerateCode(string code)
        {
            // On effectue l'analyse lexicale
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(code);
            var analyzedTokens = lexicalAnalyzer.AnalyzeCode();


            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer(analyzedTokens);
            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            CodeGenerator codeGenerator = new CodeGenerator();
            string generatedCode = "";
            semanticAnalyzer.SymbolTable.DebutBloc();
            while (syntacticAnalyzer.Tokens.Current().Type != Token.TokensType.EOF)
            {
                var tree = syntacticAnalyzer.Analyze();
                semanticAnalyzer.AnalyzeTree(tree);
                generatedCode += codeGenerator.GenerateCode(tree);
            }
            semanticAnalyzer.SymbolTable.FinBloc();

            return generatedCode;
        }
    }
}
