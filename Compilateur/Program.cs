using System;
using System.IO;

namespace Compilateur
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entrer le chemin du fichier : \n");
            string path = Console.ReadLine();

            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();

            try
            {
                string code = compilator.DoCompile(path);

                Console.WriteLine("Entrer le chemin de sortie du fichier compilé : \n");
                string outF = Console.ReadLine();

                if (File.Exists(outF))
                {
                    File.Delete(outF);
                }

                File.WriteAllText(outF, code);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la compilation : " + e.Message);
            }
        }
    }
}
