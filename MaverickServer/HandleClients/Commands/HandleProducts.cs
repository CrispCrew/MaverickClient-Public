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
    public class HandleProducts
    {
        public static string UserProducts(TcpClient clientSocket, string data)
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
                    ServerResponse = "Packages=";

                    if (Token.Username == "MaverickCheats" || Token.Username == "CrispyCheats")
                    {
                        int id = 0;
                        foreach (Product product in GetProducts(Tokens.Tokens.GetTokenByToken(Token.AuthToken).ID, Tokens.Tokens.GetTokenByToken(Token.AuthToken).Username))
                        {
                            Console.WriteLine(product.ToString());

                            if (id == 0)
                                ServerResponse += product.id + ":" + product.name + ":" + product.file + ":" + product.processname + ":" + product.status + ":" + product.version + ":" + product.free + ":" + product.autolaunchmem;
                            else
                                ServerResponse += "|" + product.id + ":" + product.name + ":" + product.file + ":" + product.processname + ":" + product.status + ":" + product.version + ":" + product.free + ":" + product.autolaunchmem;

                            id++;
                        }
                    }
                    else
                    {
                        int id = 0;
                        foreach (Product product in GetProducts(Tokens.Tokens.GetTokenByToken(Token.AuthToken).ID, Tokens.Tokens.GetTokenByToken(Token.AuthToken).Username))
                        {
                            if (id == 0)
                                ServerResponse += product.id + ":" + product.name + ":" + product.file + ":" + product.processname + ":" + product.status + ":" + product.version + ":" + product.autolaunchmem;
                            else
                                ServerResponse += "|" + product.id + ":" + product.name + ":" + product.file + ":" + product.processname + ":" + product.status + ":" + product.version + ":" + product.autolaunchmem;

                            id++;
                        }
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

        public static bool HasProduct(int id, Tokens.Token token)
        {
            //grab username products
            List<int> owned_uids = OwnedProducts(token.ID, token.Username);

            //Compare username products to list of caches
            foreach (int product_uid in owned_uids)
            {
                if (product_uid == id)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets all Products that the Username Owns - Includes all free ones
        /// </summary>
        /// <param name="Username"></param>
        private static List<Product> GetProducts(int ID, string Username)
        {
            List<Product> owned = new List<Product>();

            if (Caches.Products.Count <= 0)
                CacheProducts();

            //grab username products
            List<int> owned_uids = OwnedProducts(ID, Username);

            //Compare username products to list of caches
            foreach (int product_uid in owned_uids)
            {
                foreach (Product prod in Caches.Products)
                {
                    if (prod.id == product_uid)
                        owned.Add(prod);
                    else
                        Console.WriteLine(prod.id + " != " + product_uid);
                }
            }

            return owned;
        }

        /// <summary>
        /// Caches all Products from SQL Database
        /// </summary>
        private static void CacheProducts()
        {
            Connect connect = new Connect();

            Caches.Products = connect.QueryProducts();

            connect.Close();
        }

        /// <summary>
        /// Caches all Products from SQL Database
        /// </summary>
        private static List<int> OwnedProducts(int ID, string Username)
        {
            List<int> temp = new List<int>();

            Connect connect = new Connect();

            temp = connect.QueryUserProducts(ID, Username);

            connect.Close();

            return temp;
        }
    }
}
