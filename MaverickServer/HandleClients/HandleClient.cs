using MaverickServer.Logs;
using MaverickServer.Types;
using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients
{
    public class HandleClient
    {
        /// <summary>
        /// Starts the Socket Client Async
        /// </summary>
        /// <param name="inClientSocket">fhfshfshfshfs</param>
        /// <param name="clineNo">fhfshfshfshfs</param>
        public void startClient(Connection connection)
        {
            connection.clientHandle = this;

            connection.thread = new Thread(() => doChat(connection));
            connection.thread.Start();
        }

        private void doChat(Connection connection)
        {
            int requestCount = 0;

            while (true)
            {
                byte[] size;
                byte[] bytesFrom;
                string dataFromClient = null;
                string serverResponse = null;

                Stopwatch timer = new Stopwatch();
                timer.Start();

                try
                {
                    requestCount++;

                    if (connection.clientSocket == null || !connection.clientSocket.Connected || connection.disconnected)
                    {
                        try
                        {
                            connection.clientSocket.Close();
                        }
                        catch
                        {

                        }

                        break;
                    }

                    string IPAddress = ((IPEndPoint)connection.clientSocket.Client.RemoteEndPoint).Address.ToString();

                    //Whitelist
                    if (IPAddress != "127.0.0.1" && IPAddress != "109.199.125.101")
                    {
                        bool found = false;
                        MaverickServer.Types.Request request = new MaverickServer.Types.Request();
                        foreach (MaverickServer.Types.Request cachedrequest in new List<MaverickServer.Types.Request>(Server.Requests))
                        {
                            if (cachedrequest.IP == IPAddress)
                            {
                                found = true;

                                request = cachedrequest;
                            }
                        }

                        if (found)
                        {
                            Console.WriteLine("IP Found -> " + request.Attempts + " IP= " + request.IP);

                            //Requests Per Minute?
                            if (request.Attempts > Server.RequestMax)
                            {
                                if (request.Attempts > (Server.RequestMax * 2))
                                {
                                    Console.WriteLine("Blocking IP");

                                    INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                                    INetFwRule firewallRule = firewallPolicy.Rules.OfType<INetFwRule>().Where(x => x.Name == "Blocked: " + IPAddress).FirstOrDefault();

                                    if (firewallRule == null)
                                    {
                                        firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                                        firewallRule.Name = "Blocked: " + IPAddress;
                                        firewallRule.Description = "Blocking Traffic";
                                        firewallRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                                        firewallRule.LocalPorts = "6969";
                                        firewallRule.RemoteAddresses = IPAddress;
                                        firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                                        firewallRule.Enabled = true;
                                        firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                                        firewallPolicy.Rules.Add(firewallRule);
                                    }

                                    Log.WriteLine("Blocked IP (API Quota) [" + IPAddress + "]", "Blocked");
                                }

                                //Keep updating the attempts and when they last attempted
                                foreach (MaverickServer.Types.Request cachedrequest in new List<MaverickServer.Types.Request>(Server.Requests))
                                {
                                    if (cachedrequest.IP == request.IP)
                                    {
                                        cachedrequest.Attempts = cachedrequest.Attempts + 1;
                                        cachedrequest.Time = DateTime.Now;
                                    }
                                }

                                Console.WriteLine("Blocked Request {0} Times -> " + IPAddress, request.Attempts);

                                Log.WriteLine("Blocked Request [" + IPAddress + "]", "APIQuota");

                                Console.WriteLine("Server Response: API Quota Reached" + " IP -> " + ((IPEndPoint)connection.clientSocket.Client.RemoteEndPoint).Address.ToString());

                                serverResponse = "API Quota Reached";

                                //Byte Streams
                                byte[] APIQuota = Encoding.ASCII.GetBytes(serverResponse);

                                byte[] APIQiotaSize = BitConverter.GetBytes(APIQuota.Length);

                                //Network Streams
                                connection.networkStream.Write(APIQiotaSize, 0, APIQiotaSize.Length);
                                connection.networkStream.Write(APIQuota, 0, APIQuota.Length);
                                connection.networkStream.Flush();

                                Console.Write(">> Size=" + BitConverter.ToInt32(APIQuota, 0) + " Response: " + serverResponse);

                                connection.timeout = DateTime.Now;

                                break;
                            }
                            else
                            {
                                foreach (MaverickServer.Types.Request cachedrequest in new List<MaverickServer.Types.Request>(Server.Requests))
                                {
                                    if (cachedrequest.IP == request.IP)
                                    {
                                        cachedrequest.Attempts = cachedrequest.Attempts + 1;
                                        cachedrequest.Time = DateTime.Now;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("IP Not Found (Adding IP) -> IP= " + IPAddress);

                            Server.Requests.Add(new MaverickServer.Types.Request(IPAddress, 1, DateTime.Now));
                        }
                    }

                    connection.networkStream = connection.clientSocket.GetStream();

                    size = new byte[4];

                    if (connection.networkStream.Read(size, 0, size.Length) == 0)
                    {
                        Console.WriteLine("Request == 0");

                        Thread.Sleep(1000);

                        continue;
                    }
                    else if (BitConverter.ToInt32(size, 0) == 0)
                    {
                        serverResponse = "Too Small of a Request";
                    }
                    else if (BitConverter.ToInt32(size, 0) > 1048576)
                    {
                        serverResponse = "Too Large of a Request";
                    }
                    else
                    {
                        bytesFrom = new byte[BitConverter.ToInt32(size, 0)];

                        Console.WriteLine("ExpectedSize: " + BitConverter.ToInt32(size, 0) + " bytesFrom Length: " + bytesFrom.Length);

                        connection.networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                        dataFromClient = Encoding.ASCII.GetString(bytesFrom);

                        Console.WriteLine("Raw Data: " + dataFromClient);

                        serverResponse = HandleCommand.ParseCommand(connection.clientSocket, dataFromClient);
                    }

                    //Byte Streams
                    byte[] outStream = Encoding.ASCII.GetBytes(serverResponse);

                    byte[] outSize = BitConverter.GetBytes(outStream.Length);

                    //Network Streams
                    connection.networkStream.Write(outSize, 0, outSize.Length);
                    connection.networkStream.Write(outStream, 0, outStream.Length);
                    connection.networkStream.Flush();

                    Console.Write(">> Size=" + BitConverter.ToInt32(outSize, 0) + " Response: " + serverResponse);

                    connection.timeout = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Console.Write(">> " + ex.ToString());

                    connection.disconnected = true;

                    break;
                }

                Console.WriteLine(timer.Elapsed.TotalMilliseconds + "ms");

                timer.Reset();
            }
        }
    }
}
