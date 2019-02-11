﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;

namespace FlubuCore.WebApi.Updater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread.Sleep(2000);
            
            string frameworkName = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()
                ?.FrameworkName;
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            bool isNetCore = frameworkName.StartsWith(".NETCoreApp");
            string flubuPath, deployScript;
            if (isWindows)
            {
              
               flubuPath = Path.GetFullPath("Updates/WebApi/flubu.exe");
               if (isNetCore)
               {
                   deployScript = Path.GetFullPath("Updates/WebApi/DeployScript.cs");
               }
               else
               {
                   deployScript = Path.GetFullPath("Updates/WebApi/DeploymentScript.cs");
               }

             
            }
            else
            {
                flubuPath = Path.GetFullPath("Updates/WebApi/Deploy.bat");
                deployScript = Path.GetFullPath("Updates/WebApi/DeploymentScript.cs");
                var processRestore = Process.Start(new ProcessStartInfo
                {
                    WorkingDirectory = Path.GetDirectoryName(flubuPath),
                    FileName = "dotnet restore",
                    Arguments = "Deploy.csproj",
                });

                processRestore.WaitForExit();
            }

            Console.WriteLine($"path: {deployScript}");
            var process = Process.Start(new ProcessStartInfo
            {
                WorkingDirectory = Path.GetDirectoryName(flubuPath),
                FileName = "dotnet flubu",
                Arguments = $"-s={deployScript}"
            });
         
            process.WaitForExit();
            int code = process.ExitCode;
            Console.WriteLine($"flubu exit code: {code}");
        }
    }
}
