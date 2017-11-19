using NetFwTypeLib;
using Server.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace MaverickServer.HandleClients
{
    public class HandleCommand
    {
        public static string ParseCommand(TcpClient clientSocket, string data)
        {
            string Function;
            string ServerResponse;
            
            //Request Variable
            if (Request.Contains("Request", data))
            {
                Function = Request.Get("Request", data);

                Console.WriteLine(Function);

                #region User API Requests
                if (Function == "Version")
                {
                    //If cache is older than 5 minutes, update it
                    if (Server.CacheTime.AddMinutes(30) > DateTime.Now)
                        ServerResponse = Server.Version;
                    else
                    {
                        Server.Version = new StreamReader("Version.txt").ReadLine();

                        ServerResponse = Server.Version;
                    }
                }
                else if (Function == "Update")
                    ServerResponse = HandleFiles.UploadUpdate(clientSocket);
                else if (Function == "Login")
                    ServerResponse = HandleLogin.Login(clientSocket, data);
                else if (Function == "Register")
                    ServerResponse = "Register on the Forum"; //HandleRegister.Register(clientSocket, data);
                else if (Function == "Activate")
                    ServerResponse = "Activations Disabled"; //HandleActivation.Activate(clientSocket, data);
                else if (Function == "Products")
                    ServerResponse = HandleProducts.UserProducts(clientSocket, data);
                else if (Function == "Newsfeed")
                    ServerResponse = HandleNews.ProductNews(clientSocket, data);
                else if (Function == "Download")
                    ServerResponse = HandleFiles.UploadFile(clientSocket, data);
                else if (Function == "AccountDetails")
                    ServerResponse = HandleAccount.AccountDetails(clientSocket, data);
                else if (Function == "CheckProduct")
                    ServerResponse = HandleCheat.Products(clientSocket, data);
                else if (Function == "APICheck")
                    ServerResponse = HandleCheat.Verified(clientSocket, data);
                else if (Function == "LoginCount")
                    ServerResponse = HandleAccountCount.LoginCount(clientSocket, data);
                else if (Function == "OnlineCount")
                    ServerResponse = HandleAccountCount.OnlineCount(clientSocket, data);
                #endregion

                #region Administrator Commands
                else if (Function == "AdminGetAccountDetails")
                    ServerResponse = "Deprecated"; //HandleAccount.AdminGetAccountDetails(clientSocket, data);
                else if (Function == "GenerateKey")
                    ServerResponse = "Deprecated"; //HandleGenerating.GenerateKey(clientSocket, data);
                else if (Function == "Ban")
                    ServerResponse = "Deprecated"; //HandleBan.Ban(clientSocket, data);
                else if (Function == "FindUser")
                    ServerResponse = "Deprecated"; //HandleAccount.FindUsernameFromDiscordId(clientSocket, data);
                else if (Function == "ResetHWID")
                    ServerResponse = "Dreprecated"; //HandleAccount.ResetHWID(clientSocket, data);
                else if (Function == "ResetPassword")
                    ServerResponse = "Deprecated"; //HandleAccount.ResetPassword(clientSocket, data);
                else
                    ServerResponse = "Request Undefined";
                #endregion
            }
            else
            {
                ServerResponse = "Invalid API Request";
            }

            Console.WriteLine("Server Response: " + ServerResponse + " Data: " + data + " IP -> " + ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString());

            return ServerResponse;
        }
    }
}
