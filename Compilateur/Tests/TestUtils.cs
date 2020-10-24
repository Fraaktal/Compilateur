using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Compilateur.Tests
{
    public static class TestUtils
    {
        public static string TmpPath
        {
            get { return Directory.GetCurrentDirectory() + @"\tmpCodetest.txt"; }
        }

        public static void SaveCodeToTmp(string code)
        {
            File.WriteAllText(TmpPath, code);
        }

        public static string CallMachine()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "E:\\Polytech\\APP4\\Compilation\\msm.exe";
            startInfo.Arguments = TmpPath;
            startInfo.RedirectStandardOutput = true;
            process.StartInfo = startInfo;
            process.Start();

            return process.StandardOutput.ReadToEnd();
        }
    }
}
