using Server;
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
    public class HandleFiles
    {
        public static string UploadFile(TcpClient clientSocket, string data)
        {
            int productid;
            string Token;
            string productfile = null;
            string ServerResponse = null;

            if (Request.Contains("Token", data))
            {
                Token = Request.Get("Token", data);

                Tokens.Token token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(Token).ID, Tokens.Tokens.GetTokenByToken(Token).Username, Token);

                if (Tokens.Tokens.CheckToken(token))
                {
                    if (Request.Contains("ProductID", data))
                    {
                        productid = Convert.ToInt32(Request.Get("ProductID", data));

                        if (HandleProducts.HasProduct(productid, token))
                        {
                            foreach (Product product in Caches.Products)
                            {
                                if (product.id == productid)
                                {
                                    productfile = product.file;

                                    break;
                                }
                            }

                            if (productfile != null)
                            {
                                if (FileTransfer.UploadFile(clientSocket.GetStream(), productfile))
                                {
                                    ServerResponse = "File Upload Complete";
                                }
                                else
                                {
                                    ServerResponse = "File Upload failed";
                                }
                            }
                            else
                            {
                                ServerResponse = "ProductID returned an invalid file";
                            }

                        }
                        else
                        {
                            ServerResponse = "You do not own the Product that was specified";
                        }
                    }
                    else
                    {
                        ServerResponse = "ProductID was missing from the request";
                    }
                }
                else
                {
                    ServerResponse = "Authentication Token was not found";
                }
            }

            return ServerResponse;
        }

        public static string UploadUpdate(TcpClient clientSocket)
        {
            string ServerResponse;

            if (FileTransfer.UploadFile(clientSocket.GetStream(), "Update.zip"))
            {
                ServerResponse = "File Upload Complete";
            }
            else
            {
                ServerResponse = "File Upload failed";
            }        

            return ServerResponse;
        }
    }
}
