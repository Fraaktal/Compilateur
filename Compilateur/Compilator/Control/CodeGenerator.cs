using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            LabelsLoop = new List<Tuple<string, string>>();
        }

        private int IfCount { get; set; }

        private int LoopCount { get; set; }

        private List<Tuple<string, string>> LabelsLoop { get; set; }

        public string GenerateCode(Node node)
        {
            string generatedCode = "";
            
            switch (node.Type)
            {
                case Node.NodeType.Add:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "add\n";
                    break;
                case Node.NodeType.Mult:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "mul\n";
                    break;
                case Node.NodeType.Sub:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "sub\n";
                    break;
                case Node.NodeType.Div:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "div\n";
                    break;
                case Node.NodeType.Mod:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
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
                    generatedCode += GenerateCode(node.Children.First());
                    generatedCode += "drop\n";
                    break;
                case Node.NodeType.Ref:
                    generatedCode += $"get {node.Slot}\n";
                    break;
                case Node.NodeType.Affect:
                    generatedCode += GenerateCode(node.Children[1]);
                    generatedCode += $"dup\nset {node.Children[0].Slot}\n";
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
                    string endIf = $"if{IfCount}";
                    generatedCode += $"jumpf {endIf}\n";
                    IfCount++;
                    generatedCode += GenerateCode(node.Children[1]);
                    if (node.Children.Count == 3)
                    {
                        string endElse = $"if{IfCount}";
                        IfCount++;
                        generatedCode += $"jump {endElse}\n.{endIf}\n";
                        generatedCode += GenerateCode(node.Children[2]);
                        generatedCode += $".{endElse}\n";
                    }
                    else
                    {
                        generatedCode += $".{endIf}\n";
                    }
                    break;
                case Node.NodeType.Loop:
                    string loop1 = $"Loop{LoopCount}";
                    string loop2 = $"Loop{LoopCount + 1}";
                    LabelsLoop.Add(new Tuple<string, string>(loop1,loop2));
                    LoopCount += 2;
                    generatedCode += $".{loop1}\n";
                    generatedCode += GenerateCode(node.Children.First());
                    generatedCode += $"jump {loop1}\n.{loop2}\n";
                    LabelsLoop.RemoveAt(LabelsLoop.Count-1);
                    break;
                case Node.NodeType.Break:
                    generatedCode += $"jump {LabelsLoop.First().Item2}\n";
                    break;
                case Node.NodeType.Continue:
                    generatedCode += $"jump {LabelsLoop.First().Item1}\n";
                    break;
                case Node.NodeType.Inferior:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "cmplt\n";
                    break;
                case Node.NodeType.Equals:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "cmpeq\n";
                    break;
                case Node.NodeType.Different:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "cmpne\n";
                    break;
                case Node.NodeType.InferiorEq:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "cmple\n";
                    break;
                case Node.NodeType.Superior:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "cmpgt\n";
                    break;
                case Node.NodeType.SuperiorEq:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += "cmpge\n";
                    break;
                case Node.NodeType.Appel:
                    generatedCode += $"prep {node.Identificator}\n";
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    generatedCode += $"call {node.Children.Count}\n";
                    break;
                case Node.NodeType.Fonction:
                    generatedCode += $".{node.Identificator}\n";
                    generatedCode += $"resn {node.SlotCount - (node.Children.Count-1)}\n";
                    generatedCode += GenerateCode(node.Children.Last());
                    generatedCode += "push 0;\n ret\n";

                    break;
                default:
                    foreach (var nodeChild in node.Children)
                    {
                        generatedCode += GenerateCode(nodeChild);
                    }
                    break;

            }

            return generatedCode;
        }
    }
}
