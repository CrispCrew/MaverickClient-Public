using AuthLib.Functions.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuthLib.Functions.Client
{
    /// <summary>
    /// The AuthLib API Client
    /// </summary>
    public class API
    {
        /// <summary>
        /// Send API Request to specified clientSocket
        /// <param name="clientSocket">Server Socket</param>
        /// <param name="Request">API Request</param>
        /// </summary>
        public static string SendAPIRequest(TcpClient clientSocket, string Request)
        {
            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(Request);
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

            string returndata = Encoding.ASCII.GetString(bytesFrom);

            Log.Write("Data from Server: " + returndata);

            Log.Write(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return returndata;
        }
    }
}
