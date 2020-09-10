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
            IfCount = 0;
            LoopCount = 0;
            LabelsLoop = new Queue<Tuple<string, string>>();
        }

        private int IfCount { get; set; }

        private int LoopCount { get; set; }

        private Queue<Tuple<string, string>> LabelsLoop { get; set; }

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
                case Node.NodeType.Ref:
                    generatedCode += $"get {node.Slot}\n";
                    break;
                case Node.NodeType.Affect:
                    generatedCode = GenerateCode(node.Children[1]);
                    generatedCode += $"dup\nset {node.Slot}\n";
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
                case Node.NodeType.Test:
                    generatedCode += GenerateCode(node.Children.First());
                    string endIf = $".if{IfCount}";
                    generatedCode += $"jumpf {endIf}\n";
                    IfCount++;
                    generatedCode += GenerateCode(node.Children[1]);
                    if (node.Children.Count == 3)
                    {
                        string endElse = $".if{IfCount}";
                        generatedCode += $"jump {endElse}\n{endIf}\n";
                        generatedCode += GenerateCode(node.Children[2]);
                        generatedCode += $"{endElse}\n";
                    }
                    else
                    {
                        generatedCode += $"{endIf}\n";
                    }
                    break;
                case Node.NodeType.Loop:
                    string loop1 = $".Loop{LoopCount}";
                    string loop2 = $".{LoopCount + 1}";
                    LabelsLoop.Enqueue(new Tuple<string, string>(loop1,loop2));
                    LoopCount += 2;
                    generatedCode += $"{loop1}\n";
                    GenerateCode(node.Children.First());
                    generatedCode += $"jump {loop1}\n{loop2}\n";
                    LabelsLoop.Dequeue();
                    break;
                case Node.NodeType.Inferior:
                    generatedCode += "cmplt\n";
                    break;

            }
            //TODO CREER UN UTILS POUR AFFICHER L'ARBRE virer ++ et --

            return generatedCode;
        }
    }
}
