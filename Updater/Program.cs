using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Updater.API;

namespace Updater
{
    public class Program
    {
        public static Client client = new Client();

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Update Protocol");

            //Delete old updates due to local header issues
            if (File.Exists(Environment.CurrentDirectory + "\\" + "Update.zip"))
            {

                try
                {
                    File.Delete(Environment.CurrentDirectory + "\\" + "Update.zip");
                }
                catch
                {

                }
            }

            if (client.Update() == "Download Completed")
            {
                Console.WriteLine("Local Loader Outdated, downloaded new loader in current folder!");

                Process[] processes = Process.GetProcessesByName("MaverickClient");

                foreach (Process process in processes)
                {
                    try
                    {
                        process.Kill();
                        process.Close();
                        process.CloseMainWindow();
                    }
                    catch
                    {

                    }
                }

                //Delete client to update client
                if (File.Exists(Environment.CurrentDirectory + "\\" + "MaverickClient.exe"))
                {
                    try
                    {
                        File.Delete(Environment.CurrentDirectory + "\\" + "MaverickClient.exe");
                    }
                    catch
                    {

                    }
                }

                //Delete AuthLib to replace new version
                if (File.Exists(Environment.CurrentDirectory + "\\" + "AuthLib.dll"))
                {

                    try
                    {
                        File.Delete(Environment.CurrentDirectory + "\\" + "AuthLib.dll");
                    }
                    catch
                    {

                    }
                }

                ZipArchive archive = ZipFile.Open(Environment.CurrentDirectory + "\\Update.zip", ZipArchiveMode.Read);

                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    //Skip Theme.dll and Updater.exe, we need these for the updater
                    if (!file.Name.Contains("Theme.dll") && !file.Name.Contains("Updater.exe"))
                    {
                        file.ExtractToFile(Environment.CurrentDirectory + "\\" + file.Name);
                    }
                }

                archive.Dispose();

                if (File.Exists(Environment.CurrentDirectory + "\\" + "MaverickClient.exe"))
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = Environment.CurrentDirectory + "\\",
                        //UseShellExecute = false,
                        FileName = Environment.CurrentDirectory + "\\" + "MaverickClient.exe",
                        //WindowStyle = ProcessWindowStyle.Hidden,
                        //CreateNoWindow = true,
                        UseShellExecute = true,
                        Verb = "runas"
                    };
                    process.StartInfo = startInfo;
                    process.Start();

                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Failed to auto update!");
            }
        }
    }
}
