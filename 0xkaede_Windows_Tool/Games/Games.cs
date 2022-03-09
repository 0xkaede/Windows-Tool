using _0xkaede_Windows_Tool.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0xkaede_Windows_Tool
{
    internal class Games
    {
        static WebClient wc = new WebClient();

        public static void GetGames()
        {
            Program.Title();
            Console.WriteLine("  1) Minecraft");
            Console.WriteLine("  2) Ds Simulator");
            Console.Write("\n  Please Enter a number: ");
            string text = Console.ReadLine();

            if (text == "1")
            {
                Console.Clear();
                Minecraft();
            }
        }

        private static void Minecraft()
        {
            Program.Title();
            Console.WriteLine("  1) Download Launcher (Only needs to be installed once on each pc)");
            Console.WriteLine("  2) Version 1.8.9");
            Console.WriteLine("  3) Version 1.8.9 Optifine (Soon)");
            Console.WriteLine("  4) Version 1.5_1b (Soon)");
            Console.WriteLine("  5) Version 1.9.2 (Soon)");
            Console.WriteLine("  5) Version 1.9.2 Optifine (Soon)");
            Console.Write("\n  Please Enter a number: ");
            string text = Console.ReadLine();

            if (text == "1")
            {
                Console.Clear();
                DownloadMCLauncher();
                Console.Clear();
                Minecraft();
            }
            if (text == "2")
            {
                Console.Clear();
                DownloadMC_1_8_9();
                Console.Clear();
                Minecraft();
            }
        }

        private static void DsSim()
        {
            Program.Title();
            Console.WriteLine("  1) Download Launcher To Desktop");
            Console.WriteLine("  2) New Super Mario File");
            Console.Write("\n  Please Enter a number: ");
            string text = Console.ReadLine();

            if (text == "1")
            {
                Console.Clear();
                DownloadMCLauncher();
                Console.Clear();
                Minecraft();
            }
            if (text == "2")
            {
                Console.Clear();
                DownloadMC_1_8_9();
                Console.Clear();
                Minecraft();
            }
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

        private static void DownloadDsLauncher()
        {
            var versions = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\versions\\1.8.9";
            var zip = versions + "\\1.8.9.zip";
            Console.WriteLine("");
            Console.WriteLine("");
            PositiveLog("Starting to install 1.8.9 Files...");
            if (Directory.Exists(versions))
            {
                DeleteDirectory(versions);
                Thread.Sleep(100);
                Directory.CreateDirectory(versions);
            }
            else
                Directory.CreateDirectory(versions);

            Thread.Sleep(3000);

            var StatusAPI = wc.DownloadString("https://raw.githubusercontent.com/0xkaede/Windows-Tool/main/0xkaede_Backend/Api.json");
            Info StatusResponse = JsonConvert.DeserializeObject<Info>(StatusAPI);

            wc.Proxy = null;
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            wc.DownloadFileAsync(new Uri(StatusResponse.application.games.minecraft.versions.V1_8_9), zip);
            while (wc.IsBusy)
                Thread.Sleep(1000);
            if (File.Exists(zip))
            {
                Thread.Sleep(3000);
                ZipFile.ExtractToDirectory(zip, versions);
                Thread.Sleep(3000);

                File.Delete(zip);

                PositiveLog("Finished! Minecraft 1.8.9 Was installed, Please run tlauncher to play minecraft now...");
            }
            else
            {
                NegiviteLog("ERROR: File not downloaded!");
                Thread.Sleep(1000);
            }
        }

        private static void DownloadMC_1_8_9()
        {
            var versions = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\versions\\1.8.9";
            var zip = versions + "\\1.8.9.zip";
            Console.WriteLine("");
            Console.WriteLine("");
            PositiveLog("Starting to install 1.8.9 Files...");
            if (Directory.Exists(versions))
            {
                DeleteDirectory(versions);
                Thread.Sleep(100);
                Directory.CreateDirectory(versions);
            }
            else
                Directory.CreateDirectory(versions);

            Thread.Sleep(3000);

            var StatusAPI = wc.DownloadString("https://raw.githubusercontent.com/0xkaede/Windows-Tool/main/0xkaede_Backend/Api.json");
            Info StatusResponse = JsonConvert.DeserializeObject<Info>(StatusAPI);

            wc.Proxy = null;
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            wc.DownloadFileAsync(new Uri(StatusResponse.application.games.minecraft.versions.V1_8_9), zip);
            while (wc.IsBusy)
                Thread.Sleep(1000);
            if (File.Exists(zip))
            {
                Thread.Sleep(3000);
                ZipFile.ExtractToDirectory(zip, versions);
                Thread.Sleep(3000);

                File.Delete(zip);

                PositiveLog("Finished! Minecraft 1.8.9 Was installed, Please run tlauncher to play minecraft now...");
            }
            else
            {
                NegiviteLog("ERROR: File not downloaded!");
                Thread.Sleep(1000);
            }
        }

        private static void DownloadMCLauncher()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            PositiveLog("Starting to install TLauncher...");

            var dir = Directory.GetCurrentDirectory() + $"\\TLauncher-2.72-Installer-0.6.81.exe";

            var StatusAPI = wc.DownloadString("https://raw.githubusercontent.com/0xkaede/Windows-Tool/main/0xkaede_Backend/Api.json");
            Info StatusResponse = JsonConvert.DeserializeObject<Info>(StatusAPI);

            Process injector = new Process
            {
                StartInfo = new ProcessStartInfo(dir)
                {
                    UseShellExecute = true,
                    WorkingDirectory = dir
                },
            };

            wc.Proxy = null;
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            wc.DownloadFileAsync(new Uri(StatusResponse.application.games.minecraft.installer), dir);
            while (wc.IsBusy)
                Thread.Sleep(1000);
            if (File.Exists(dir))
            {
                Thread.Sleep(2000);
                PositiveLog("Finished! Launching Launcher, Please follow steps on screen...");
                Thread.Sleep(3000);
                injector.Start();

                injector.WaitForExit();

                PositiveLog("Done! ");
            }
            else
            {
                NegiviteLog("ERROR: File not downloaded!");
                Thread.Sleep(1000);
            }
        }

        static void Input(string text)
            => PositiveLog("Downloading: " + text);

        private static int counter;
        public static string totalfix;

        static void ProgressChanged(object obj, DownloadProgressChangedEventArgs e)
        {
            var Loger = $" {e.ProgressPercentage}% of 100%";

            counter++;

            if (counter % 65 == 0)
            {
                Input(Loger);
            }
        }

        static void Completed(object obj, AsyncCompletedEventArgs e)
        {
            var Loger = $" 100% of 100%, {totalfix}MB of {totalfix}MB.";
            Input(Loger);
        }

        static void PositiveLog(string content)
            => Console.WriteLine("  {0} {1}", "[+]", content);

        static void NegiviteLog(string content)
            => Console.WriteLine("  {0} {1}", "[-]", content);
    }
}
