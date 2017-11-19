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
    public class HandleNews
    {
        public static string ProductNews(TcpClient clientSocket, string data)
        {
            string[] datas = data.Split('&');

            string token;
            string ServerResponse;

            if (Request.Contains("Token", data))
            {
                token = Request.Get("Token", data);

                Tokens.Token Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                Console.WriteLine(Token.IP + " - " + Token.Username + " - " + Token.AuthToken);

                if (Tokens.Tokens.CheckToken(Token))
                {
                    ServerResponse = "Newsfeed=";

                    int id = 0;
                    foreach (News newsfeed in GetNews(Token.Username))
                    {
                        if (id == 0)
                            ServerResponse += newsfeed.productid + "-" + newsfeed.newsfeed + "-" + newsfeed.postdate.ToString();
                        else
                            ServerResponse += "|" + newsfeed.productid + "-" + newsfeed.newsfeed + "-" + newsfeed.postdate.ToString();

                        id++;
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

        private static List<News> GetNews(string Username)
        {
            if (Caches.Newsfeed.Count <= 0)
                CacheNewsfeed();

            return Caches.Newsfeed;
        }

        private static void CacheNewsfeed()
        {
            Connect connect = new Connect();

            Caches.Newsfeed = connect.QueryNewsfeed();

            connect.Close();
        }
    }
}
