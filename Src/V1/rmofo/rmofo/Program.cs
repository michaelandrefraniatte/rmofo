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
            Console.WriteLine(rootPath);
            ProtectFiles(rootPath);
            ProtectDir(rootPath); 
            Console.WriteLine("done");
            string[] fileNames = Directory.GetFiles(rootPath);
            foreach (string fileName in fileNames)
            {
                Console.WriteLine(fileName);
                ProtectFile(fileName);
                Console.WriteLine("done");
            }
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                ProtectFiles(dir);
                ProtectDir(dir);
                Console.WriteLine("done");
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                    ProtectFile(file);
                    Console.WriteLine("done");
                }
            }
        }
        public static void ProtectFiles(string dirName)
        {
            if (!Directory.Exists(dirName))
            {
                return;
            }
            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = $"attrib +S '{dirName}' /s",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            var process = Process.Start(start);
            process.WaitForExit();
        }
        public static void ProtectDir(string dirName)
        {
            if (!Directory.Exists(dirName))
            {
                return;
            }
            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = $"attrib +S '{dirName}' /d",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            var process = Process.Start(start);
            process.WaitForExit();
        }
        public static void ProtectFile(string name)
        {
            if (!File.Exists(name))
            {
                return;
            }
            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = $"attrib +S '{name}'",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            var process = Process.Start(start);
            process.WaitForExit();
        }
    }
}