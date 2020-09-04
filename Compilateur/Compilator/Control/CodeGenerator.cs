using System;
using System.Collections.Generic;
using System.Text;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    public class CodeGenerator
    {
        public CodeGenerator()
        {
        }

        public string GenerateCode(Node node)
        {
            string generatedCode = "";
            foreach (var nodeChild in node.Children)
            {
                generatedCode += GenerateCode(nodeChild);
            }

            switch (node.Type)
            {
                case Node.NodeType.Add:
                    generatedCode += "add\n";
                    break;
                case Node.NodeType.Mult:
                    generatedCode += "mul\n";
                    break;
                case Node.NodeType.Sub:
                    generatedCode += "sub\n";
                    break;
                case Node.NodeType.Div:
                    generatedCode += "div\n";
                    break;
                case Node.NodeType.Mod:
                    generatedCode += "mod\n";
                    break;
                case Node.NodeType.Const:
                    generatedCode += $"push {node.IntValue}\n";
                    break;
            }

            return generatedCode;
        }
    }
}
