using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0xkaede_Windows_Tool
{
    internal class Program
    {
        public static void Log(string log)
            => Console.WriteLine("  [+] " + log);

        private static void Main(string[] args)
            => startme();

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

        private static void startme()
        {

            Console.Clear();
            Title();
            Console.Title = "Windows Tools by 0xkaede";
            Log("Welcome " + Environment.UserName + ", App version 1.0.0.1");
            Console.WriteLine("");
            Console.WriteLine("  1) Change Background");
            Console.WriteLine("  2) Install TaskBarX");
            Console.WriteLine("  3) System Info");
            Console.WriteLine("  4) Restart File Explorer");
            Console.WriteLine("  5) Games Download");
            Console.WriteLine("  6) Open Appdata folder");
            Console.WriteLine("  7) Crash PC");
            Console.Write("\n  Please Enter a number: ");
            string text = Console.ReadLine();

            if (text == "1")
            {
                Console.Clear();
                SetImage(null);
                startme();
            }
            else if (text == "2")
            {
                Console.Clear();
                Process.Start("https://drive.google.com/file/d/1XRyVC_leWkKNagfV51xtmSzIcinHS4ZP/view");
                startme();
            }
            else if (text == "3")
            {
                Console.Clear();
                GetHwid();
                startme();
            }
            else if (text == "4")
            {
                Console.Clear();
                RestartExplorer();
                startme();
            }
            else if (text == "5")
            {
                Console.Clear();
                Games.GetGames();
                startme();
            }
            else if (text == "6")
            {
                Console.Clear();
                Openappdata();
                startme();
            }
            if (text == "7")
            {
                Console.Clear();
                CrashComputer();
                startme();
            }
            Console.Read();
        }

        private static void Openappdata()
            => Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        private static void GetHwid()
        {
            Console.WriteLine("");
            Log("Processor Id: " + SystemInfo.GetProcessorId());
            Log("HDD Serial No: " + SystemInfo.GetHDDSerialNo());
            Log("MAC Address: " + SystemInfo.GetMACAddress());
            Log("Board ProductId Id: " + SystemInfo.GetBoardProductId());
            Thread.Sleep(5000);
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            return new string((from s in Enumerable.Repeat<string>("0123456789", length)
                               select s[random.Next(s.Length)]).ToArray<char>());
        }

        public static bool SetImage(string ImagePath = null)
        {
            Title();
            Console.Write("\n  [+] Please Drag and drop a file and click the enter key: ");
            ImagePath = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("");
            for (int i = 0; i < 12; i++)
            {
                string str = RandomString(6);
                Log("Bypassing - 0x00" + str);
                Log("Bypassed - 0x00" + str);
                Thread.Sleep(50);
            }
            string AppdataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Themes";
            Image image = Image.FromFile(ImagePath);
            string filename = AppdataDir + "/TranscodedWallpaper";
            string text = AppdataDir + "/CacheFiles";
            string a = Path.GetExtension(ImagePath).ToLower();
            bool flag = a == ".png";
            if (flag)
            {
                try
                {
                    foreach (string path in Directory.GetFiles(text))
                    {
                        string a2 = Path.GetExtension(path).ToLower();
                        bool flag2 = a2 == ".jpg";
                        if (flag2)
                        {
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                            string filename2 = text + "\\" + fileNameWithoutExtension + ".jpg";
                            image.Save(filename2, ImageFormat.Jpeg);
                        }
                    }
                }
                catch
                {
                }
                image.Save(filename, ImageFormat.Jpeg);
            }
            else
            {
                bool flag3 = a == ".jpg";
                if (!flag3)
                {
                    Log("Atm we only support .png and .jpg formats");
                    return false;
                }
                try
                {
                    foreach (string path2 in Directory.GetFiles(text))
                    {
                        string a3 = Path.GetExtension(path2).ToLower();
                        bool flag4 = a3 == ".jpg";
                        if (flag4)
                        {
                            string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(path2);
                            string filename3 = text + "\\" + fileNameWithoutExtension2 + ".jpg";
                            image.Save(filename3, ImageFormat.Jpeg);
                        }
                    }
                }
                catch
                {
                }
                image.Save(filename, ImageFormat.Jpeg);
            }
            image.Dispose();
            RestartExplorer();
            return true;
        }

        private static void RestartExplorer()
        {
            try
            {
                Log("Restarting Explore.exe");
                Process[] processesByName = Process.GetProcessesByName("explorer");
                if (processesByName.Length != 0)
                {
                    foreach (Process process in processesByName)
                    {
                        process.Kill();
                        Log($"Killed {process}");
                    }
                }
                Log("Starting Explore.exe");
                Thread.Sleep(1000);
                Process.Start(Environment.SystemDirectory + "\\..\\explorer.exe");
                Thread.Sleep(2000);
            }
            catch (Exception arg)
            {
                Log($"ERROR: {arg}");
                Console.ReadLine();
            }
        }

        private static void CrashComputer()
        {
            for (int i = 0; i < 10000000; i++)
                Process.Start("notepad.exe");
        }
    }
}
