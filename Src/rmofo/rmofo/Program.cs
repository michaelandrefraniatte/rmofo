﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using KeyboardInputsAPI;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace rmofo
{
    internal class Program
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        public static uint getForegroundProcessPid()
        {
            uint processID = 0;
            IntPtr hWnd = GetForegroundWindow();
            GetWindowThreadProcessId(hWnd, out processID);
            return processID;
        }
        public static void OnKeyDown()
        {
            KeyboardInput ki = new KeyboardInput();
            ki.Scan();
            ki.BeginPolling();
            while (true)
            {
                if (ki.KeyboardKeyF1 & getForegroundProcessPid() == Process.GetCurrentProcess().Id)
                {
                    const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Contact: michael.franiatte@gmail.com.\n\r\n\r• Publisher: https://github.com/michaelandrefraniatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• License: Not open source, not free of charge to use.";
                    const string caption = "About";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                System.Threading.Thread.Sleep(60);
            }
        }
        static void Main(string[] args)
        {
            Task.Run(() => { OnKeyDown(); });
            string rootPath = System.Windows.Forms.Application.StartupPath; 
            Console.WriteLine(rootPath);
            ProtectDir(rootPath);
            ProtectFiles(rootPath);
            Console.WriteLine("done");
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                ProtectDir(dir);
                ProtectFiles(dir);
                Console.WriteLine("done");
            }
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
    }
}