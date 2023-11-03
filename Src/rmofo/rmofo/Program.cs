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
            ProtectDirs(rootPath);
            Console.WriteLine("done");
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                ProtectFiles(dir);
                ProtectDirs(dir);
                Console.WriteLine("done");
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
        public static void ProtectDirs(string dirName)
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
    }
}