using CrispyCheats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.API
{
    public class Client
    {
        int fails = 0;
        TcpClient clientSocket;
        NetworkStream serverStream;

        /// <summary>
        /// Entry Point - Establishes Socket Connection
        /// </summary>
        public Client()
        {
            //Log Init
            Console.WriteLine("Logging Init");

            //Establish Server Connection
            Console.WriteLine("Client Started");

            Connect();

            Console.WriteLine("Client Socket Program - Server Connected ...");
        }

        private void Connect()
        {
            try
            {
                Console.WriteLine("Socket reinitiating!");

                clientSocket = new TcpClient();

                Console.WriteLine("Socket NoDelay = true");

                clientSocket.NoDelay = true;
                clientSocket.Connect("127.0.0.1", 6969);
                Console.WriteLine("Running in Local Mode");
                
                Console.WriteLine("Socket Connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Socket Error");
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Contacts server for Version Number
        /// </summary>
        /// <returns></returns>
        public bool ServerCheck()
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return false;
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Version");

            if (Response != "")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Version Check Failed: " + Response);
            }

            Console.WriteLine(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return false;
        }

        /// <summary>
        /// Returns a count of all logins
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public string LoginCount(string Token)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return "Failed to Connect";
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=LoginCount&Token=" + Token);

            Console.WriteLine("Request: " + "LoginCount" + " -> " + "Response: " + Response);

            Console.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Returns a count of all logins
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public string OnlineCount(string Token)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return "Failed to Connect";
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=OnlineCount&Token=" + Token);

            Console.WriteLine("Request: " + "OnlineCount" + " -> " + "Response: " + Response);

            Console.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Contacts server for Login Check
        /// </summary>
        /// <returns></returns>
        public bool Login(out string Token)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;
                    Token = "";

                    return false;
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            Token = "";

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Login&Username=MaverickCheats&Password=@UrDrive337859185&HWID=" + FingerPrint.Value());

            if (Response.Split('-')[0] == "Login Found")
            {
                if (Response.Split('-')[1] != "")
                {
                    Token = Response.Split('-')[1];

                    return true;
                }
                else
                {
                    Token = Response;

                    return false;
                }
            }

            Console.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return false;
        }

        /// <summary>
        /// Returns a list of owned products
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public string Products(string Token)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return "Failed to Connect";
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Products&Token=" + Token);

            Console.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Contacts server for Version Number
        /// </summary>
        /// <returns></returns>
        public string GenerateKey(int ProductID, string GeneratedByID, string GeneratedByName, string DiscordID, string DiscordName)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return "Failed to Connect";
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=GenerateKey&ProductID=" + ProductID + "&GeneratedByID=" + GeneratedByID + "&GeneratedByName=" + GeneratedByName + "&DiscordID=" + DiscordID + "&DiscordName=" + DiscordName);

            if (Response != "" && Response != "Invalid API Request" && Response != "Request Undefined")
            {
                return Response;
            }
            else
            {
                Console.WriteLine("Version Check Failed: " + Response);
            }

            Console.WriteLine(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return "";
        }

        public string FindUser(ulong DiscordId)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return "Failed to Connect";
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=FindUser&DiscordId=" + DiscordId);

            if (Response != "DiscordId Not Specified" && Response != "" && Response != "User Not Found")
            {
                return Response;
            }
            else
            {
                Console.WriteLine("Get Username Failed: " + Response);
            }

            Console.WriteLine(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return "User Not Found";
        }

        /// <summary>
        /// Get Anyones Account Details (Admin)
        /// </summary>
        /// <param name="Token">User Auth-Token</param>
        public string AdminGetAccountDetails(string Username, string Token)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return "Failed to Connect";
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=AdminGetAccountDetails&Username=" + Username + "&Token=" + Token);

            Console.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        public string ResetPassword(string Username, string Token)
        {
            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return "Failed to Connect";
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=ResetPassword&Username=" + Username + "&Token=" + Token);

            Console.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Ban a User
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public bool ResetHWID(string Username, string Token)
        {
            bool Success;

            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return false;
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=ResetHWID&Username=" + Username + "&Token=" + Token);

            if (Response == "HWID Reset")
            {
                Success = true;
            }
            else
            {
                Success = false;
            }

            timer.Reset();

            return Success;
        }

        /// <summary>
        /// Ban a User
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public bool Ban(string Username, string Token)
        {
            bool Success;

            Connect:
            if (!clientSocket.Connected)
            {
                if (fails >= 3)
                {
                    fails = 0;

                    return false;
                }

                fails++;

                Connect();

                goto Connect;
            }

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Ban&Username=" + Username + "&Token=" + Token);

            if (Response == "Banned User")
            {
                Success = true;
            }
            else
            {
                Success = false;
            }

            timer.Reset();

            return Success;
        }

        private void CleanStream()
        {
            if (!clientSocket.Connected)
                Connect();

            if (serverStream == null)
                return;

            Console.WriteLine("Cleaning Stream");

            byte[] buffer = new byte[4096];

            while (serverStream.DataAvailable)
            {
                Console.WriteLine("Cleared Data");

                serverStream.Read(buffer, 0, buffer.Length);
            }
        }
    }
}
