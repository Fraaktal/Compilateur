using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Compilateur.Compilator.Control;
using NUnit.Framework;

#if DEBUG
namespace Compilateur.Tests.Tests
{
    [TestFixture] 
    public class TestLexicalAnalyser
    {
        private string FileContent
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Compilateur.Tests.Fichiers.TestLexicalAnalyzer.c";
                string file = "";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        file = reader.ReadToEnd();
                    }
                }
                return file;
            }
        }

        [Test]
        public void TestVide()
        {
            string content = "";

            LexicalAnalyzer la = new LexicalAnalyzer(content);

            var res = la.AnalyzeCode();

            Assert.AreEqual(res.Tokens.Count,1); //seulement EOF
        }

        [Test]
        public void Test()
        {
            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();
            string code = compilator.Compile(FileContent);

            string res = TestUtils.CallMachine(code);
        }
    }
}
#endif