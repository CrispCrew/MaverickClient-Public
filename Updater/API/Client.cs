using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace Updater.API
{
    public class Client
    {
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
            Console.WriteLine("Socket reinitiating!");

            clientSocket = new TcpClient();

            Console.WriteLine("Socket NoDelay = true");

            clientSocket.NoDelay = true;

            //clientSocket.Connect("162.248.247.2", 6060); //Debug Server
            clientSocket.Connect("158.69.255.77", 6969);

            Console.WriteLine("Socket Connected");
        }

        /// <summary>
        /// Contacts server for Version Number
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            string Response = API.SendAPIRequest(clientSocket, "Request=Version");

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

        public string Update()
        {
            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes("Request=Update");
            byte[] outSize = BitConverter.GetBytes(outStream.Length);

            Console.WriteLine("Raw Data: " + BitConverter.ToInt32(outSize, 0) + " -> " + Encoding.ASCII.GetString(outStream));

            //Write Bytes
            serverStream.Write(outSize, 0, outSize.Length);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            //Wait for Response - TODO: Add Recieve Byte outSize

            byte[] size = new byte[4];

            serverStream.Read(size, 0, size.Length);

            byte[] bytesFrom = new byte[BitConverter.ToInt32(size, 0)];

            Console.WriteLine("ExpectedSize: " + BitConverter.ToInt32(size, 0) + " bytesFrom Length: " + bytesFrom.Length);

            serverStream.Read(bytesFrom, 0, bytesFrom.Length);

            string returndata = Encoding.ASCII.GetString(bytesFrom); //Out of memory????

            Console.WriteLine("Data from Server: " + returndata);

            if (Request.Contains("Size", returndata))
            {
                long ExpectedSize = Convert.ToInt64(Request.Get("Size", returndata));

                Download download = new Download(serverStream, Environment.CurrentDirectory + "\\Update.zip", ExpectedSize);
                download.ShowDialog();

                if (download.Done)
                {
                    Console.WriteLine("Download Completed: Update.zip -> Size: " + ExpectedSize);

                    return "Download Completed";
                }
                else
                {
                    Console.WriteLine("Download Failed: Update.zip -> Size: " + ExpectedSize);

                    return "Download Failed";
                }
            }
            else
            {
                Console.WriteLine("Size Parameter was not provided");
            }

            Console.WriteLine(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return returndata;
        }
    }
}
