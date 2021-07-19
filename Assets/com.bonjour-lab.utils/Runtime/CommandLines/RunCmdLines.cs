using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Bonjour.CommandPrompt
{
    public class RunCmdLines
    {

        public static void ExecuteCommand(string path, string filename, string args)
        {
            string cmd = " /c start /d " + path + " " + filename + ".exe " + args;
            UnityEngine.Debug.Log("Execute commande: " + cmd);
            Process process = Process.Start("cmd.exe", cmd);
        }

        public static void ListProcesses()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                UnityEngine.Debug.Log(p.ProcessName);
            }
        }

        public static void killProcess(string appName)
        {
            string cmd = " /c taskkill /f /fi \"WindowTitle eq " + appName + "\"";
            UnityEngine.Debug.Log("Kill process: " + cmd);
            Process process = Process.Start("cmd.exe", cmd);
        }
    }
}
