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
    public class HandleAccountCount
    {
        public static string LoginCount(TcpClient clientSocket, string data)
        {
            //Disabled Command
            return "";

            string token;
            string ServerResponse;

            //Check for Auth Token
            if (Request.Contains("Token", data))
            {
                token = Request.Get("Token", data);

                Tokens.Token Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                //Check for Token
                if (Tokens.Tokens.CheckToken(Token))
                {
                    Connect connect = new Connect();

                    ServerResponse = connect.LoginCount().ToString();

                    connect.Close();
                }
                else
                {
                    ServerResponse = "Authenticated Token was not found";
                }
            }
            else
            {
                ServerResponse = "Authentication Token Parameter was not provided";
            }

            return ServerResponse;
        }

        public static string OnlineCount(TcpClient clientSocket, string data)
        {
            string token;
            string ServerResponse;

            //Check for Auth Token
            if (Request.Contains("Token", data))
            {
                token = Request.Get("Token", data);

                Tokens.Token Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                //Check for Token
                if (Tokens.Tokens.CheckToken(Token))
                {
                    string response = "";

                    List<string> products = new List<string>();
                    List<Tokens.Token> tokens = new List<Tokens.Token>();

                    lock (Tokens.Tokens.AuthTokens)
                    {
                        tokens = new List<Tokens.Token>(Tokens.Tokens.AuthTokens);
                    }

                    foreach (Product product in Caches.Products)
                        products.Add(product.name);

                    foreach (string product in products)
                    {
                        List<string> users = new List<string>();

                        foreach (Tokens.Token temptoken in tokens.Where(toke => toke.LastDevice == product))
                        {
                            if (!users.Any(user => user == temptoken.username))
                                users.Add(temptoken.username);
                        }

                        response += product + "-" + users.Count + "|";
                    }

                    ServerResponse = response;

                    /* Old Method (New Method = Querying Tokens [Much Quicker)
                    Connect connect = new Connect();

                    ServerResponse = connect.OnlineCount().ToString();

                    connect.Close();
                    */
                }
                else
                {
                    ServerResponse = "Authenticated Token was not found";
                }
            }
            else
            {
                ServerResponse = "Authentication Token Parameter was not provided";
            }

            return ServerResponse;
        }
    }
}
