using MaverickServer.Database;
using MaverickServer.HandleClients.Tokens;
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
    public class HandleBan
    {
        public static string Ban(TcpClient clientSocket, string data)
        {
            string ServerResponse;
            string Username;
            string token;
            Token Token;

            if (Request.Contains("Username", data))
            {
                Username = Request.Get("Username", data);

                if (Request.Contains("Token", data))
                {

                    token = Request.Get("Token", data);

                    Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                    Connect connect = new Connect();

                    if (connect.BanUser(Username, Token))
                    {
                        ServerResponse = "Banned User";
                    }
                    else
                    {
                        ServerResponse = "Failed to Ban User";
                    }

                    connect.Close();
                }
                else
                {
                    ServerResponse = "Token not Provided";
                }
            }
            else
            {
                ServerResponse = "Username not Provided";
            }
            return ServerResponse;
        }
    }
}
