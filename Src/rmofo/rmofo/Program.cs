using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rmofo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string rootPath = System.Windows.Forms.Application.StartupPath; 
            string[] fileNames = Directory.GetFiles(rootPath);
            foreach (string fileName in fileNames)
            {
                Console.WriteLine(fileName);
                UnblockFiles(fileName);
                Console.WriteLine("done");
            }
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);
            foreach (string dir in dirs)
            {
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                    UnblockFiles(file);
                    Console.WriteLine("done");
                }
            }
        }
        public static void UnblockFiles(string fileName)
        {
            if (!Directory.Exists(fileName))
            {
                return;
            }
            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = $"attrib +S '{fileName}' /d /s",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            var process = Process.Start(start);
            process.WaitForExit();
        }
    }
}