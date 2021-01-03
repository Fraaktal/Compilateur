using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

#if DEBUG
namespace Compilateur.Tests.Tests
{
    [TestFixture]
    public class TestCompilator
    {
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
        public void Test_Print_Read()
        {
            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();
            string code = compilator.Compile(GetCodeFromFileName("TestCompilator_Print"));
            TestUtils.SaveCodeToTmp(code);
            string res = TestUtils.CallMachine();
            string[] results = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual("0", results[0]);
            Assert.AreEqual($"1", results[1]);
            Assert.AreEqual($"-1", results[2]);
            Assert.AreEqual($"3", results[3]);
            Assert.AreEqual("-1", results[4]);
            Assert.AreEqual($"2", results[5]);
            Assert.AreEqual($"0", results[6]);
            Assert.AreEqual($"1", results[7]);
            Assert.AreEqual("-1", results[8]);
            Assert.AreEqual($"3", results[9]);
            Assert.AreEqual($"-1", results[10]);
            Assert.AreEqual($"2", results[11]);
            Assert.AreEqual($"0", results[12]);
        }

        [Test]
        public void Test_BasicOperator()
        {
            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();
            string code = compilator.Compile(GetCodeFromFileName("TestCompilator_BasicOperator"));
            TestUtils.SaveCodeToTmp(code);
            string res = TestUtils.CallMachine();
            string[] results = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            //Addition
            Assert.AreEqual("4", results[0]);
            Assert.AreEqual($"-2", results[1]);
            Assert.AreEqual($"-4", results[2]);
            Assert.AreEqual($"0", results[3]);

            //Soustraction
            Assert.AreEqual($"1", results[4]);
            Assert.AreEqual($"-1", results[5]);
            Assert.AreEqual($"1", results[6]);
            Assert.AreEqual($"-1", results[7]);
            Assert.AreEqual($"0", results[8]);

            //Multiplication
            Assert.AreEqual($"42", results[9]);
            Assert.AreEqual($"-42", results[10]);
            Assert.AreEqual($"42", results[11]);
            Assert.AreEqual($"42", results[12]);
            Assert.AreEqual($"42", results[13]);

            //Division
            Assert.AreEqual($"6", results[14]);
            Assert.AreEqual($"-6", results[15]);
            Assert.AreEqual($"6", results[16]);
            Assert.AreEqual($"6", results[17]);
            Assert.AreEqual($"6", results[18]);

            //Modulo
            Assert.AreEqual($"1", results[19]);
            Assert.AreEqual($"-1", results[20]);
            Assert.AreEqual($"-1", results[21]);
            Assert.AreEqual($"1", results[22]);
            Assert.AreEqual($"1", results[23]);
            
            //Pow
            Assert.AreEqual($"8", results[24]);
            Assert.AreEqual($"-8", results[25]);
            Assert.AreEqual($"0", results[26]);
            Assert.AreEqual($"8", results[27]);
            Assert.AreEqual($"8", results[28]);
        }

        [Test]
        public void TestCompilator_ConditionalLoopRecurcive()
        {
            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();
            string code = compilator.Compile(GetCodeFromFileName("TestCompilator_ConditionalLoopRecurcive"));
            TestUtils.SaveCodeToTmp(code);
            string res = TestUtils.CallMachine();
            string[] results = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            //Test If
            Assert.AreEqual("-1", results[0]);
            Assert.AreEqual($"2", results[1]);
            Assert.AreEqual($"2", results[2]);

            //Test While
            Assert.AreEqual($"10", results[3]);

            //Test For
            Assert.AreEqual($"10", results[4]);

            //Test Recurcif => Fibonacci
            Assert.AreEqual($"610", results[5]);

            //Test break
            Assert.AreEqual($"1", results[6]);

            //Test continue
            Assert.AreEqual($"1", results[7]);

        }

        [Test]
        public void TestCompilator_PointerArray()
        {
            Compilator.Control.Compilator compilator = new Compilator.Control.Compilator();
            string code = compilator.Compile(GetCodeFromFileName("TestCompilator_PointerArray"));
            TestUtils.SaveCodeToTmp(code);
            string res = TestUtils.CallMachine();
            string[] results = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            //Test Pointer
            Assert.AreEqual($"6", results[0]);

            //Test array
            Assert.AreEqual("1", results[1]);
            Assert.AreEqual($"2", results[2]);
            Assert.AreEqual($"3", results[3]);

        }

        public string GetCodeFromFileName(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Compilateur.Tests.Fichiers.{fileName}.c";
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
}
#endif
