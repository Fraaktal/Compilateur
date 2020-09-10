using System;
using System.Collections.Generic;
using System.Linq;
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
                case Node.NodeType.Block:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }

                    break;
                case Node.NodeType.Debug:
                    generatedCode += GenerateCode(node.Children.First());
                    generatedCode += "dbg\n";
                    break;
                case Node.NodeType.Drop:
                    generatedCode = GenerateCode(node.Children.First());
                    generatedCode += "drop\n";
                    break;
                case Node.NodeType.UnAdd:
                    generatedCode += GenerateCode(node.Children.First());
                    break;
                case Node.NodeType.UnNot:
                    generatedCode += GenerateCode(node.Children.First());
                    generatedCode += "neg\n";
                    break;
                case Node.NodeType.UnSub:
                    generatedCode += "push 0\n";
                    generatedCode += GenerateCode(node.Children.First());
                    generatedCode += "sub\n";
                    break;
            }

            return generatedCode;
        }
    }
}
