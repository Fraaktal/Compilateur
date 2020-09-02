using System;

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
                compilator.Compile(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la compilation : " + e.Message);
            }
        }
    }
}
