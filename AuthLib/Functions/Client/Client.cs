using AuthLib.Functions.Callbacks;
using AuthLib.Functions.Logs;
using MaverickClient;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace AuthLib.Functions.Client
{
    /// <summary>
    /// The AuthLib Client
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The Server ClientSocket
        /// </summary>
        TcpClient clientSocket = new TcpClient();

        /// <summary>
        /// The Network Stream
        /// </summary>
        NetworkStream serverStream = null;

        /// <summary>
        /// Entry Point - Establishes Socket Connection
        /// </summary>
        public Client()
        {
            //Console Title
            Console.Title = "Maverick Logs";

            //Log Init
            Console.WriteLine("Logging Init");

            //Establish Server Connection
            Console.WriteLine("Client Started");

            Connect();

            Console.WriteLine("Client Socket Program - Server Connected ...");
        }

        /// <summary>
        /// Connect to the server
        /// </summary>
        private void Connect()
        {
            clientSocket.NoDelay = true;

            //clientSocket.Connect("162.248.247.2", 6060); //Debug Server
            //clientSocket.Connect("127.0.0.1", 6060); //Debug Server
            //Console.WriteLine("Running in Debug Mode, Using Local Server.");

            clientSocket.Connect("158.69.255.77", 6969);
        }

        /// <summary>
        /// Contacts server for Version Number
        /// </summary>
        /// <returns>Version</returns>
        public string Version()
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Version");

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Server Update Call
        /// </summary>
        public string Update()
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes("Request=Update");
            byte[] outSize = BitConverter.GetBytes(outStream.Length);

            Log.Write("Raw Data: " + BitConverter.ToInt32(outSize, 0) + " -> " + Encoding.ASCII.GetString(outStream));

            //Write Bytes
            serverStream.Write(outSize, 0, outSize.Length);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            //Wait for Response - TODO: Add Recieve Byte outSize

            byte[] size = new byte[4];

            serverStream.Read(size, 0, size.Length);

            byte[] bytesFrom = new byte[BitConverter.ToInt32(size, 0)];

            Log.Write("ExpectedSize: " + BitConverter.ToInt32(size, 0) + " bytesFrom Length: " + bytesFrom.Length);

            serverStream.Read(bytesFrom, 0, bytesFrom.Length);

            string returndata = Encoding.ASCII.GetString(bytesFrom); //Out of memory????

            Log.Write("Data from Server: " + returndata);

            if (Request.Contains("Size", returndata))
            {
                long ExpectedSize = Convert.ToInt64(Request.Get("Size", returndata));

                Download download = new Download(serverStream, Environment.CurrentDirectory + "\\Update.zip", ExpectedSize);
                download.ShowDialog();

                if (download.Done)
                {
                    Log.Write("Download Completed: Update.zip -> Size: " + ExpectedSize);

                    return "Download Completed";
                }
                else
                {
                    Log.Write("Download Failed: Update.zip -> Size: " + ExpectedSize);

                    return "Download Failed";
                }
            }
            else
            {
                Console.WriteLine("Size Parameter was not provided");
            }

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return returndata;
        }

        /// <summary>
        /// Contacts server for Login Check
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="Password">Password</param>
        /// <param name="HWID">Hardware ID</param>
        /// <param name="Token">Users Token</param>
        /// <returns></returns>
        public string Login(string Username, string Password, out string Token)
        {
            string Success;

            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            Token = "";

            if (!Prepare.PrepareString(Username) || !Prepare.PrepareString(Password))
            {
                if (!Prepare.PrepareString(Username))
                    Log.Write("Prepare Failed: Username=" + Username);
                else if (!Prepare.PrepareString(Password))
                    Log.Write("Prepare Failed: Password=" + Password);

                return "Empty Credentials";
            }

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Login&Username=" + Username + "&Password=" + Password + "&HWID=" + FingerPrint.Value());

            if (Response.Split('-')[0] == "Login Found")
            {
                Token = Response.Split('-')[1];

                Log.Write("Login Found: " + Username + " -> " + Password + " -> " + Token);

                Success = "Login Found";
            }
            else
            {
                Log.Write("Error: Login not Found -> " + Response);

                Success = Response;
            }

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Success;
        }

        /// <summary>
        /// Contacts server for Login Check
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="Password">Password</param>
        /// <param name="HWID">Hardware ID</param>
        /// <param name="Token">Users Token</param>
        /// <returns></returns>
        public string ResetPassword(string Username, string Password)
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Password))
            {
                if (!Prepare.PrepareString(Password))
                    Log.Write("Prepare Failed: Password=" + Password);

                return "Empty Credentials";
            }

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=ResetPassword&Username=" + Username + "&Password=" + Password + "&HWID=" + FingerPrint.Value());

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Contacts server for Registeration Check
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="HWID"></param>
        /// <returns></returns>
        public string Register(string Username, string Password)
        {
            string Success;

            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Username) || !Prepare.PrepareString(Password))
            {
                if (!Prepare.PrepareString(Username))
                    Log.Write("Prepare Failed: Username=" + Username);
                else if (!Prepare.PrepareString(Password))
                    Log.Write("Prepare Failed: Password=" + Password);

                return "Empty Credentials";
            }

            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Register&Username=" + Username + "&Password=" + Password + "&HWID=" + FingerPrint.Value());

            if (Response == "Registration Successful")
            {
                Log.Write("Registration Successful: " + Username + " -> " + Password);

                Success = "Registration Successful";
            }
            else
            {
                Log.Write("Registration Failed: " + Username + " -> " + Password);

                Success = Response;
            }

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Success;
        }

        /// <summary>
        /// Contacts Server for Activation Check
        /// </summary>
        /// <param name="Token">User Token</param>
        /// <param name="LicenseKey">License Key</param>
        /// <returns></returns>
        public string Activate(string Token, string LicenseKey)
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Token) || !Prepare.PrepareString(LicenseKey))
            {
                if (!Prepare.PrepareString(Token))
                    Log.Write("Prepare Failed: Token=" + Token);
                else if (!Prepare.PrepareString(LicenseKey))
                    Log.Write("Prepare Failed: LicenseKey=" + LicenseKey);

                return "Error: " + "Prepare Failed: LicenseKey=" + LicenseKey + "&" + "Prepare Failed: Token=" + Token;
            }

            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Activate&Token=" + Token + "&LicenseKey=" + LicenseKey);

            if (Response == "Activation Successful")
            {
                Log.Write("Activation Successful: " + Token + " -> " + LicenseKey);
            }
            else
            {
                Log.Write("Activation Failed: " + Token + " -> " + LicenseKey);
            }

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Contacts server for product list
        /// </summary>
        /// <param name="Token">User Token</param>
        /// <returns></returns>
        public string Products(string Token)
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Token))
            {
                if (!Prepare.PrepareString(Token))
                    Log.Write("Prepare Failed: Token=" + Token);

                return "Empty Token";
            }

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Products&Token=" + Token);

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Contacts server for a product wide newsfeed
        /// </summary>
        /// <param name="Token">User Token</param>
        /// <returns></returns>
        public string NewsFeed(string Token)
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Token))
            {
                if (!Prepare.PrepareString(Token))
                    Log.Write("Prepare Failed: Token=" + Token);

                return "Empty Token";
            }

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Newsfeed&Token=" + Token);

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Contacts server for a download - Cheat / Update
        /// </summary>
        /// <param name="Token">User Token</param>
        /// <param name="File">File to Download</param>
        /// <param name="productid">Product ID</param>
        /// <returns></returns>
        public string Download(string Token, string File, int productid)
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Token))
            {
                if (!Prepare.PrepareString(Token))
                    Log.Write("Prepare Failed: Username=" + Token);

                return "Empty Token";
            }

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes("Request=Download&Token=" + Token + "&ProductID=" + productid);
            byte[] outSize = BitConverter.GetBytes(outStream.Length);

            Log.Write("Raw Data: " + BitConverter.ToInt32(outSize, 0) + " -> " + Encoding.ASCII.GetString(outStream));

            //Write Bytes
            serverStream.Write(outSize, 0, outSize.Length);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            //Wait for Response - TODO: Add Recieve Byte outSize

            byte[] size = new byte[4];

            serverStream.Read(size, 0, size.Length);

            byte[] bytesFrom = new byte[BitConverter.ToInt32(size, 0)];

            Log.Write("ExpectedSize: " + BitConverter.ToInt32(size, 0) + " bytesFrom Length: " + bytesFrom.Length);

            serverStream.Read(bytesFrom, 0, bytesFrom.Length);

            string returndata = Encoding.ASCII.GetString(bytesFrom); //Out of memory????

            Log.Write("Data from Server: " + returndata);

            if (Request.Contains("Size", returndata))
            {
                long ExpectedSize = Convert.ToInt64(Request.Get("Size", returndata));

                Download download = new Download(serverStream, Environment.CurrentDirectory + "\\" + File, ExpectedSize);
                download.ShowDialog();

                if (download.Done)
                {
                    Log.Write("Download Completed: " + File + " -> Size: " + ExpectedSize);

                    return "Download Completed";
                }
                else
                {
                    Log.Write("Download Failed: " + File + " -> Size: " + ExpectedSize);

                    return "Download Failed";
                }
            }
            else
            {
                Console.WriteLine("Size Parameter was not provided");
            }

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return returndata;
        }

        /// <summary>
        /// Get Account Details
        /// </summary>
        /// <param name="Token">User Auth-Token</param>
        public string AccountDetails(string Token)
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Token))
            {
                if (!Prepare.PrepareString(Token))
                    Log.Write("Prepare Failed: Token=" + Token);

                return "Empty Token";
            }

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=AccountDetails&Token=" + Token);

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
        }

        /// <summary>
        /// Get Anyones Account Details (Admin)
        /// </summary>
        /// <param name="Token">User Auth-Token</param>
        public string AdminGetAccountDetails(string Username, string Token)
        {
            if (!clientSocket.Connected)
                Connect();

            CleanStream();

            if (!Prepare.PrepareString(Token))
            {
                if (!Prepare.PrepareString(Token))
                    Log.Write("Prepare Failed: Token=" + Token);

                return "Empty Token";
            }

            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=AdminGetAccountDetails&Username=" + Username + "&Token=" + Token);

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return Response;
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
