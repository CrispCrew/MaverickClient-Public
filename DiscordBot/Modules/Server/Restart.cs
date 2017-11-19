using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Server
{
    public class Restart : ModuleBase<SocketCommandContext>
    {
        /*
        [Command("restart")]
        public async Task RebootServer()
        {
            if (Context.Message.Author.Id != 193255051807031296 &&
                (!Iterations.HasRole((SocketGuildUser)Context.User, "Supervisor")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Owner")))
            {
                await Context.Message.Channel.SendMessageAsync("Permission Denied.");

                return;
            }

            string FilePath = @"C:\Users\root\Desktop\Maverick Server\MaverickServer.exe";
            string WorkingDirectory = @"C:\Users\root\Desktop\Maverick Server\";

            Process server = Process.GetProcessesByName("maverickserver").FirstOrDefault();

            if (server != null)
            {
                FilePath = server.MainModule.FileName;
                WorkingDirectory = Path.GetDirectoryName(FilePath);

                try
                {
                    server.Kill();
                }
                catch
                {
                    //Ignore
                }

                try
                {
                    server.Close();
                }
                catch
                {
                    //Ignore
                }

                try
                {
                    server.CloseMainWindow();
                }
                catch
                {
                    //Ignore
                }

                Thread.Sleep(1000);

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = WorkingDirectory,
                    FileName = FilePath,
                    Verb = "runas"
                };
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = WorkingDirectory,
                    FileName = FilePath,
                    Verb = "runas"
                };
                process.StartInfo = startInfo;
                process.Start();
            }

            Thread.Sleep(1000);

            await Context.Channel.SendMessageAsync("Restart: " + (Process.GetProcessesByName("maverickserver").Length > 0 ? "Success" : "Failed"));
        }
        */
    }
}
