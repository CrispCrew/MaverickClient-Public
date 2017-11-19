using MaverickServer.Database;
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
    public class HandleActivation
    {
        public static string Activate(TcpClient clientSocket, string data)
        {
            string[] datas = data.Split('&');

            string token;
            string LicenseKey;
            string ServerResponse;

            if (Request.Contains("Token", data))
            {
                token = Request.Get("Token", data);

                Tokens.Token Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                Console.WriteLine(Token.IP + " - " + Token.Username + " - " + Token.AuthToken);

                if (Tokens.Tokens.CheckToken(Token))
                {
                    if (Request.Contains("LicenseKey", data))
                    {
                        LicenseKey = Request.Get("LicenseKey", data);

                        if (ActivateKey(Token.Username, LicenseKey))
                        {
                            ServerResponse = "Activation Successful";
                        }
                        else
                        {
                            ServerResponse = "Activation Failed";
                        }
                    }
                    else
                    {
                        ServerResponse = "LicenseKey Parameter was not provided";
                    }
                }
                else
                {
                    ServerResponse = "Authentication Token was not found";
                }
            }
            else
            {
                ServerResponse = "Username Parameter was not provided";
            }

            return ServerResponse;
        }

        private static bool ActivateKey(string Username, string LicenseKey)
        {
            bool activated = false;

            Connect connect = new Connect();

            activated = connect.Activate(Username, LicenseKey);

            connect.Close();

            return activated;
        }
    }
}
