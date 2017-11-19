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
    public class HandleCheat
    {
        public static string Products(TcpClient clientSocket, string data)
        {
            string IP = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString();
            string Product = ""; //Check if they own a Paid Product
            string ServerResponse;

            if (Request.Contains("Product", data))
                Product = Request.Get("Product", data);

            if (Tokens.Tokens.GetToken(IP) != null)
            {
                Tokens.Token token = Tokens.Tokens.GetToken(IP);

                if (Tokens.Tokens.CheckToken(token))
                {
                    if (Product != "")
                    {
                        int ProductID = 0;

                        try
                        {
                            ProductID = Convert.ToInt32(Product);
                        }
                        catch
                        {
                            ProductID = 0;
                        }

                        Connect connect = new Connect();

                        if (ProductID != 0)
                        {
                            ServerResponse = (connect.QueryUserProducts(token.ID, token.Username).Any(productid => productid == ProductID) ? "Authenticated" : "Not Authenticated");
                        }
                        else
                        {
                            ServerResponse = "Not Authenticated";
                        }

                        connect.Close();
                    }
                    else
                    {
                        ServerResponse = "Not Authenticated";
                    }
                }
                else
                {
                    ServerResponse = "Not Authenticated";
                }
            }
            else
            {
                ServerResponse = "Not Authenticated";
            }

            return ServerResponse;
        }

        public static string Verified(TcpClient clientSocket, string data)
        {
            string IP = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString();
            string Device = "";
            string DisplayName = "";
            string ServerResponse;

            if (Request.Contains("Device", data))
                Device = Request.Get("Device", data);

            if (Request.Contains("Name", data))
                DisplayName = Request.Get("Name", data);

            if (Tokens.Tokens.GetToken(IP) != null)
            {
                Tokens.Token token = Tokens.Tokens.GetToken(IP);

                if (Tokens.Tokens.CheckToken(token))
                {
                    if (Device != "" || DisplayName != "")
                    {
                        if (Device != "")
                            token.LastDevice = Device;

                        /*
                        Connect connect = new Connect();

                        connect.UpdateAPICheck(token.Username, token.LastDevice, DisplayName);

                        connect.Close();
                        */
                    }

                    ServerResponse = "Authenticated";
                }
                else
                {
                    ServerResponse = "Not Authenticated";
                    //ServerResponse = "Not Authenticated";
                }
            }
            else
            {
                ServerResponse = "Not Authenticated";
                //ServerResponse = "Not Authenticated";
            }           

            return ServerResponse;
        }
    }
}
