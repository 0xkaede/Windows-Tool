using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows_Tool_Launcher.Model;

namespace Windows_Tool_Launcher
{
    internal class Program
    {
        static Process Tool;

        static WebClient wc = new WebClient();

        static string InstallFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WindowsTool";

        static string exe = InstallFolder + "\\SAHJWE+37486923.exe";

        public static void Title()
        {
            Colorful.Console.WriteLine();
            Colorful.Console.Write("   ██╗    ██╗██╗███╗   ██╗██████╗  ██████╗ ██╗    ██╗███████╗    ████████╗ ██████╗  ██████╗ ██╗     \n", Color.LightPink);
            Colorful.Console.Write("   ██║    ██║██║████╗  ██║██╔══██╗██╔═══██╗██║    ██║██╔════╝    ╚══██╔══╝██╔═══██╗██╔═══██╗██║     \n", Color.LightPink);
            Colorful.Console.Write("   ██║ █╗ ██║██║██╔██╗ ██║██║  ██║██║   ██║██║ █╗ ██║███████╗       ██║   ██║   ██║██║   ██║██║     \n", Color.LightPink);
            Colorful.Console.Write("   ██║███╗██║██║██║╚██╗██║██║  ██║██║   ██║██║███╗██║╚════██║       ██║   ██║   ██║██║   ██║██║     \n", Color.LightPink);
            Colorful.Console.Write("   ╚███╔███╔╝██║██║ ╚████║██████╔╝╚██████╔╝╚███╔███╔╝███████║       ██║   ╚██████╔╝╚██████╔╝███████╗\n", Color.LightPink);
            Colorful.Console.Write("    ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═════╝  ╚═════╝  ╚══╝╚══╝ ╚══════╝       ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝\n", Color.LightPink);
            Colorful.Console.Write("                                                                                                    \n", Color.LightPink);
            Console.Write("\n\n\n");
        }

        static void log(string message)
            => Console.WriteLine($" [+] {message}");

        static void Main(string[] args)
        {
            Title();
            Console.CursorVisible = false;

            Console.Title = "0xkaede Windows Tool Install (Release)";

            log($"Welcome to 0xkaede windows tool (Release) Launcher!");

            if (File.Exists(exe))
            {
                log("Checking For Updates!");

                if (GetFileVer(exe))
                    DownloadTool();

                Tool = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = exe,
                        WorkingDirectory = InstallFolder
                    }
                };
            }
            else
                DownloadTool();
        }

        static void DownloadTool()
        {
            var StatusAPI = wc.DownloadString("https://raw.githubusercontent.com/0xkaede/Windows-Tool/main/0xkaede_Backend/Api.json");
            Info StatusResponse = JsonConvert.DeserializeObject<Info>(StatusAPI);

            Thread.Sleep(1000);
            Tool = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    WorkingDirectory = InstallFolder
                }
            };

            try
            {
                if (Directory.Exists(InstallFolder))
                {
                    DeleteDirectory(InstallFolder);
                    Thread.Sleep(100);
                    Directory.CreateDirectory(InstallFolder);
                }
                else
                    Directory.CreateDirectory(InstallFolder);

                wc.Proxy = null;
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                wc.DownloadFileAsync(new Uri(StatusResponse.application.mainexe), exe);
                while (wc.IsBusy)
                    Thread.Sleep(1000);
                if (File.Exists(exe))
                {
                    log("Finished! Launching Windows Tool...");
                    Thread.Sleep(3000);
                    Tool.Start();
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
                else
                {
                    log("ERROR: File not downloaded!");
                    Console.ReadLine();
                }
            }
            catch (Exception arg)
            {
                log("ERROR: " + arg);
                Console.Read();
            }
        }

        private static int counter;
        public static string totalfix;

        private static void ProgressChanged(object obj, DownloadProgressChangedEventArgs e)
        {
            var Loger = $"{e.ProgressPercentage}% of 100%, {((e.BytesReceived / 1024f) / 1024f).ToString("#0.##")}MB of {((e.TotalBytesToReceive / 1024f) / 1024f).ToString("#0.##")}MB.";

            totalfix = $"{((e.TotalBytesToReceive / 1024f) / 1024f).ToString("#0.##")}MB.";

            counter++;

            if (counter % 50 == 0)
                log(Loger);
        }

        private static void Completed(object obj, AsyncCompletedEventArgs e)
        {
            var Loger = $"100% of 100%, {totalfix}MB of {totalfix}MB.";
            log(Loger);
        }

        public static void DeleteDirectory(string DirectoryToDelete)
        {
            string[] files = Directory.GetFiles(DirectoryToDelete);
            string[] dirs = Directory.GetDirectories(DirectoryToDelete);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(DirectoryToDelete, false);
        }

        static bool GetFileVer(string exe)
        {
            if (File.Exists(exe))
            {
                FileVersionInfo fileVersionInfo = null;
                try
                {
                    fileVersionInfo = FileVersionInfo.GetVersionInfo(exe);
                }
                catch
                {
                    log("ERROR: Unknown!");
                }
                string text = string.Format("{0}.{1}.{2}.{3}", new object[]
                {
                    fileVersionInfo.FileMajorPart,
                    fileVersionInfo.FileMinorPart,
                    fileVersionInfo.FileBuildPart,
                    fileVersionInfo.FilePrivatePart,
                });
                return CheckUpdate(text);
            }
            else
                return false;
        }

        static bool CheckUpdate(string fv)
        {
            var wc = new WebClient();

            try
            {
                var StatusAPI = wc.DownloadString("https://raw.githubusercontent.com/0xkaede/Windows-Tool/main/0xkaede_Backend/Api.json");
                Info StatusResponse = JsonConvert.DeserializeObject<Info>(StatusAPI);

                var text = StatusResponse.application.version;

                log($"Newest version: {text}");

                if (!text.Contains(fv))
                {
                    log("Update available!");
                    return true;
                }
                else
                {
                    log("You are up to date!");
                    Thread.Sleep(1000);
                    return false;
                }
            }
            catch
            {
                log("ERROR: Unknown!");
            }
            return false;
        }
    }
}

