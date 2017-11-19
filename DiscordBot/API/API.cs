using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DiscordBot.API
{
    public class API
    {
        public static string SendAPIRequest(TcpClient clientSocket, string Request)
        {
            try
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

                //Check if the stream isnt returning the header
                int fails = 0;
                long read = 0;
                while (read < 4)
                {
                    if (serverStream.DataAvailable)
                    {
                        //Get First 4 bytes and break
                        read = serverStream.Read(size, 0, size.Length);
                    }
                    else
                    {
                        //Fail if it takes too long
                        if (fails >= 1000)
                        {
                            return "Error: Server didnt send size header!";
                        }

                        fails++;

                        Thread.Sleep(5);
                    }
                }

                long total = BitConverter.ToInt32(size, 0);

                //byte[] bytesFrom = new byte[BitConverter.ToInt32(size, 0)];
                byte[] bytesFrom = new byte[2048];
                MemoryStream memory = new MemoryStream();

                Console.WriteLine("ExpectedSize: " + BitConverter.ToInt32(size, 0) + " bytesFrom Length: " + bytesFrom.Length);

                fails = 0;
                read = 0;
                while (read < total)
                {
                    if (serverStream.DataAvailable)
                    {
                        if ((read + bytesFrom.Length) > total)
                        {
                            bytesFrom = new byte[total - read];
                        }

                        read += serverStream.Read(bytesFrom, 0, bytesFrom.Length);
                        memory.Write(bytesFrom, 0, bytesFrom.Length);

                        Console.Write("\rRead: {0} Total: {1} Memory: {2}", read, total, memory.Length);
                    }
                    else
                    {
                        if (fails >= 1000)
                        {
                            return "Error: Server didnt request response!";
                        }

                        fails++;

                        Thread.Sleep(5);
                    }
                }

                string returndata = Encoding.ASCII.GetString(memory.ToArray());

                Console.WriteLine("Data from Server: " + returndata);

                Console.WriteLine(timer.Elapsed.TotalMilliseconds + "ms");

                timer.Reset();

                return returndata;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
