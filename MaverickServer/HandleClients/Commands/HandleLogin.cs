using MaverickServer.Database;
using MaverickServer.HandleClients.Parse;
using Server.Functions;
using System;
using System.Net;
using System.Net.Sockets;

namespace MaverickServer.HandleClients
{
    public class HandleLogin
    {
        public static string Login(TcpClient clientSocket, string data)
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

                        int UserID = 0;
                        string Response = new WebClient().DownloadString("http://api.maverickcheats.eu/forum/api/index.php?Request=Login&Username=" + Username + "&Password=" + Password);

                        if (Response.Contains("-"))
                        {
                            if (Response.Split('-')[0] == "Login Found")
                            {
                                UserID = Convert.ToInt32(Response.Split('-')[1]);

                                /*
                                Connect connect = new Connect();

                                if (connect.MemberID(UserID))
                                {
                                    string LoginResponse = connect.Login(Username, Password, HWID);

                                    if (LoginResponse == "Login Found")
                                        ServerResponse = "Login Found" + "-" + Tokens.Tokens.GenerateToken(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Username);
                                    else
                                        return LoginResponse;
                                }

                                string resp = connect.Login(Username, Password, HWID);

                                connect.Close();
                                */

                                ServerResponse = "Login Found" + "-" + Tokens.Tokens.GenerateToken(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), UserID, Username);
                            }
                            else
                            {
                                ServerResponse = Response;
                            }
                        }
                        else if (Response == "Invalid Username")
                        {
                            ServerResponse = "Invalid Username";
                        }
                        else if (Response == "Invalid Password")
                        {
                            ServerResponse = "Invalid Password";
                        }
                        else if (Response == "Account Banned")
                        {
                            ServerResponse = "Account Banned";
                        }
                        else
                        {
                            ServerResponse = Response;
                        }

                        /*
                        Connect connect = new Connect();

                        //User Check
                        if (connect.QueryUsers(Username, true) == "No Enteries Found")
                            return "Invalid Username";

                        //Ban Check
                        if (connect.IsBanned(Username))
                            return "Account Banned";

                        //Ban Check
                        if (connect.PasswordReset(Username))
                            return "Password Reset";

                        //Login Response;
                        string resp = connect.Login(Username, Crypto.CreateHash(Password, Convert.FromBase64String(connect.GetSalt(Username))), HWID);

                        if (resp == "Login Found")
                            ServerResponse = "Login Found" + "-" + Tokens.Tokens.GenerateToken(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Username);
                        else
                            return resp;

                        connect.Close();
                        */
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
