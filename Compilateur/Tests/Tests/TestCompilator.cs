using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;

#if DEBUG
namespace Compilateur.Tests.Tests
{
    [TestFixture]
    public class TestCompilator
    {
        private string FileContent
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Compilateur.Tests.Fichiers.TestCompilator.c";
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
        public void Test_Vide()
        {
            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();
            string code = compilator.Compile("");
            TestUtils.SaveCodeToTmp(code);
            string res = TestUtils.CallMachine();
            Assert.AreEqual("", res);
        }

        [Test]
        public void Test()
        {
            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();
            string code = compilator.Compile(FileContent);
            TestUtils.SaveCodeToTmp(code);
            string res = TestUtils.CallMachine();
            string[] results = res.Split("\r\n",StringSplitOptions.RemoveEmptyEntries);

            //Test Fibonacci => fonction recurcive
            Assert.AreEqual("610", results[0]);

            Assert.AreEqual($"10", results[1]);
            Assert.AreEqual($"20", results[2]);
            Assert.AreEqual($"20", results[3]); //TODO problème avec soustraction quand négatif

            //for (int i = 1; i < 11; i++)
            //{
            //    Assert.AreEqual($"{i-1}", results[i]);
            //}
        }
    }
}
#endif
