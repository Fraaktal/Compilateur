using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Compilateur.Compilator.Business;

namespace Compilateur.Compilator.Control
{
    class CCompilatorManager
    {
        private CCompilatorManager _instance;

        private CCompilatorManager()
        {
            Tokens = new List<Token>();
        }

        public CCompilatorManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CCompilatorManager();
            }

            return _instance;
        }

        public List<Token> Tokens { get; set; }

        public string File { get; set; }

        public string GetStringFileFromPath(string path)
        {
            string file = "";
            


            return file;
        }
    }
}
