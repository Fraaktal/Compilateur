﻿using System;
using System.IO;
using System.Reflection;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class Compilator
    {
        public SemanticAnalyzer SemanticAnalyzer { get; set; }
        public CodeGenerator CodeGenerator { get; set; }

        public string DoCompile(string path)
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

            return Compile(code);
        }

        public string Compile(string content)
        {
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

            SemanticAnalyzer = new SemanticAnalyzer();
            CodeGenerator = new CodeGenerator();

            SemanticAnalyzer.SymbolTable.DebutBloc();

            string generatedCode = GenerateCode(runtime);
            generatedCode += GenerateCode(content);

            SemanticAnalyzer.SymbolTable.FinBloc();

            generatedCode += ".start\n";
            generatedCode += "prep init\ncall 0\n";
            generatedCode += "prep main\ncall 0\n";
            generatedCode += "halt\n";

            return generatedCode;
        }

        private string GenerateCode(string code)
        {
            // On effectue l'analyse lexicale
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(code);
            var analyzedTokens = lexicalAnalyzer.AnalyzeCode();
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer(analyzedTokens);
            
            string generatedCode = "";
            while (syntacticAnalyzer.Tokens.Current().Type != Token.TokensType.EOF)
            {
                var tree = syntacticAnalyzer.Analyze();
                SemanticAnalyzer.AnalyzeTree(tree);
                generatedCode += CodeGenerator.GenerateCode(tree);
            }

            return generatedCode;
        }
    }
}
