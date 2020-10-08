using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Compilateur.Tests
{
    public static class TestUtils
    {
        public static string CallMachine(string code)
        {
            Process machine = new Process();
            machine.StartInfo.FileName = "csc.exe";
            //machine.StartInfo.Arguments = "/r:System.dll /out:sample.exe stdstr.cs";
            //machine.StartInfo.UseShellExecute = false;
            machine.StartInfo.RedirectStandardOutput = true;
            machine.StartInfo.RedirectStandardInput = true;
            machine.Start();

            machine.StandardInput.WriteLine(code);
            return machine.StandardOutput.ReadLine();
        }
    }
}
