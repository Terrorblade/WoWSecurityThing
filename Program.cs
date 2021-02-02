using System;
using System.Diagnostics;
using System.IO;

namespace WoWSecurityThing
{
    class Program
    {
        static void Main(string[] args)
        {
            bool dontRun = false;
            bool dontRunFuk = false;
            string errorMessages = "";

            if (!Directory.Exists("DBFilesClient"))
            {
                dontRun = true;
                errorMessages += $"DBFilesClient folder not found. {Environment.NewLine}";
            }
            if (!File.Exists("Wow.exe"))
            {
                dontRun = true;
                errorMessages += $"Wow.exe not found. {Environment.NewLine}";
            }
            if (!File.Exists("utility.exe"))
            {
                dontRun = true;
                errorMessages += $"utility.exe not found. {Environment.NewLine}";
            }
            if (!File.Exists("FuckItUp.exe"))
            {
                dontRunFuk = true;
                errorMessages += $"Not running FukItUp, FuckItUp.exe not found.{Environment.NewLine}";
            }
            if (!dontRunFuk && (args.Length == 0 || !File.Exists(args.ToString())))
            {
                dontRunFuk = true;
                Console.WriteLine($"Not running FukItUp, can't find '{args}'.{Environment.NewLine}");
            }
            if(dontRun)
            {
                Console.WriteLine(errorMessages);
                Console.WriteLine("Press any key to close the application.");
                Console.ReadKey();
            }
            else
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = "Wow.exe \"2E 64 62 63\" \"2E 78 78 78\"",
                    FileName = "utility.exe",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };

                Console.WriteLine("Rewriting Wow.exe");

                for (int index = 0; index < 300; ++index)
                {
                    using (Process process = Process.Start(startInfo))
                        process.WaitForExit();
                }

                Console.WriteLine("Rewriting DBC's...\n");
                foreach (string file in Directory.GetFiles("DBFilesClient"))
                {
                    if (file.EndsWith(".dbc"))
                        File.Move(file, file.Replace(".dbc", ".xxx"));
                }
                if (!dontRunFuk)
                {
                    ProcessStartInfo fuk = new ProcessStartInfo
                    {
                       Arguments = args.ToString(),
                       FileName = "FuckItUp.exe",
                       WindowStyle = ProcessWindowStyle.Hidden,
                       CreateNoWindow = true
                    };
                    using (Process proc = Process.Start(fuk))
                        proc.WaitForExit();
                }

                Console.WriteLine("Done. Press any key to continue...");
            }
        }
    }
}
