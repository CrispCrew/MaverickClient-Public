using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace Updater.API
{
    public class API
    {
        public static string SendAPIRequest(TcpClient clientSocket, string Request)
        {
            //Sockets Connection
            //Debug - Log Times
            Stopwatch timer = new Stopwatch();
            timer.Start();

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(Request);
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

            string returndata = Encoding.ASCII.GetString(bytesFrom);

            Console.WriteLine("Data from Server: " + returndata);

            Console.WriteLine(timer.Elapsed.TotalMilliseconds + "ms");

            timer.Reset();

            return returndata;
        }
    }
}
