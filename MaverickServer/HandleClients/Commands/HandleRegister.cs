using MaverickServer.Database;
using MaverickServer.HandleClients.Parse;
using Server.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients
{
    public class HandleRegister
    {
        public static string Register(TcpClient clientSocket, string data)
        {
            string[] datas = data.Split('&');

            string Username;
            string Password;
            string HWID;
            string ServerResponse;

            if (Request.Contains("Username", data))
            {
                Username = Request.Get("Username", data);

                if (Request.Contains("Password", data))
                {
                    Password = Request.Get("Password", data);

                    if (Request.Contains("HWID", data))
                    {
                        HWID = Request.Get("HWID", data);

                        //Validation
                        if (HWID.Contains("-") && HWID.Length > 32)
                        {
                            Connect connect = new Connect();

                            byte[] Salt = Crypto.CreateSalt(Password);

                            string Hash = Crypto.CreateHash(Password, Salt);

                            ServerResponse = connect.Register(Username, Hash, Convert.ToBase64String(Salt), HWID, ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString());

                            connect.Close();
                        }
                        else
                        {
                            ServerResponse = "HWID Parameter is corrupted";
                        }
                    }
                    else
                    {
                        ServerResponse = "HWID Parameter was not provided";
                    }
                }
                else
                {
                    ServerResponse = "Password Parameter was not provided";
                }
            }
            else
            {
                ServerResponse = "Username Parameter was not provided";
            }

            return ServerResponse;
        }
    }
}
