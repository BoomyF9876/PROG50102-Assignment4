using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.Diagnostics;
using UnityEngine.DedicatedServer;
using System;

public class ConsoleApp: EditorWindow
{
    private string appLocation;
    private string innoScriptLocation;
    private string issLocation;
    private string[] arguments;

    [MenuItem("ConsoleApp/ConsoleApp")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ConsoleApp));
    }

    private void RunConsoleApp()
    {
        arguments = new string[2] { "-installer", "-itch-standalone" };
        for (int i = 0; i < arguments.Length; i++)
        {
            if (arguments[i] == "-installer")
            {
                string command = "& \'" + issLocation + "\' \'" + innoScriptLocation + "\'";

                Console.WriteLine(command);
                Process process = Process.Start("powershell.exe", command);
                process.WaitForExit();
                process.Close();
            }
        }

        for (int i = 0; i < arguments.Length; i++)
        {
            if (arguments[i] == "-itch-standalone")
            {
                string command = "& butler push \'" + appLocation + "\' boomyf9876/prog56693-final-project:windows-standalone";
                command = "-NoExit -Command " + command;

                Console.WriteLine(command);
                Process process = Process.Start("powershell.exe", command);
                process.WaitForExit();
                process.Close();
            }
        }
    }

    private void FillEmpty()
    {
        if (appLocation == "")
        {
            appLocation = "..\\build\\Output\\setup.exe";
        }
        if (innoScriptLocation == "")
        {
            innoScriptLocation = "..\\build\\EscapeZombieCompiler.iss";
        }
        if (issLocation == "")
        {
            issLocation = "C:\\Program Files (x86)\\Inno Setup 6\\ISCC.exe";
        }
    }

    private bool CheckError()
    {
        if (!appLocation.EndsWith(".exe"))
        {
            UnityEngine.Debug.LogWarning("Console App must be an .exe file!");
            return true;
        }

        if (!innoScriptLocation.EndsWith(".iss"))
        {
            UnityEngine.Debug.LogWarning("Inno script must be an .iss file!");
            return true;
        }

        if (!issLocation.EndsWith(".exe"))
        {
            UnityEngine.Debug.LogWarning("ISCC compiler must be an .exe file!");
            return true;
        }

        return false;
    }

    private void OnGUI()
    {
        appLocation = EditorGUILayout.TextField("Console App Location: ", appLocation);
        innoScriptLocation = EditorGUILayout.TextField("Inno Script Location: ", innoScriptLocation);
        issLocation = EditorGUILayout.TextField("Iss File Location: ", issLocation);

        FillEmpty();

        if (GUILayout.Button("Launch Console Tool"))
        {
            if (!CheckError()) RunConsoleApp();
        }
    }
}
