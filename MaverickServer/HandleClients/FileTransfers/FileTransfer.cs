using MaverickServer.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class FileTransfer
    {
        #region Upload
        public static bool UploadFile(NetworkStream serverStream, string file)
        {
            using (FileStream fileStream = File.OpenRead(Environment.CurrentDirectory + "\\Products\\" + file))
            {
                long expectedsize = File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\" + file).LongLength;

                Console.WriteLine("File: " + file + " Size: " + expectedsize);

                byte[] outStream = Encoding.ASCII.GetBytes("Size=" + expectedsize);
                byte[] outSize = BitConverter.GetBytes(outStream.Length);

                Console.Write("Raw Data: " + BitConverter.ToInt32(outSize, 0) + " -> " + Encoding.ASCII.GetString(outStream));

                //Write Bytes
                serverStream.Write(outSize, 0, outSize.Length);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                int totalBytes = 0;
                int bytesRead = 0;
                while (true)
                {
                    byte[] buffer = new byte[13106]; //65536 Bytes * 2 = 1310720 Bytes/ps || 10 Mbps

                    if ((totalBytes + buffer.Length) <= expectedsize) //sent bytes + sending bytes smaller than or equal to Size of File
                    {
                        if ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            serverStream.Write(buffer, 0, bytesRead);

                            totalBytes += bytesRead;

                            Console.Write("\rNetwork Size: {0} Bytes Read: {1}", buffer.Length, totalBytes);
                        }
                        else
                        {
                            Console.WriteLine("FileStream == 0");

                            break;
                        }
                    }
                    else
                    {
                        if ((bytesRead = fileStream.Read(buffer, 0, (int)expectedsize - totalBytes)) > 0)
                        {
                            serverStream.Write(buffer, 0, bytesRead);

                            totalBytes += bytesRead;

                            Console.Write("\rNetwork Size: {0} Bytes Read: {1}", buffer.Length, totalBytes);
                        }
                        else
                        {
                            Console.WriteLine("FileStream == 0");

                            break;
                        }
                    }

                    Thread.Sleep(10); //1 mbps
                }

                serverStream.Flush();
            }

            return true;
        }
        #endregion
    }
}
