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
    public class HandleAccount
    {
        public static string FindUsernameFromDiscordId(TcpClient clientSocket, string data)
        {
            string DiscordId;
            string ServerResponse;

            if (Request.Contains("DiscordId", data))
            {
                DiscordId = Request.Get("DiscordId", data);

                Connect connect = new Connect();

                ServerResponse = connect.FindUsername(DiscordId);

                connect.Close();
            }
            else
            {
                ServerResponse = "DiscordID not Specified";
            }

            return ServerResponse;
        }

        public static string AccountDetails(TcpClient clientSocket, string data)
        {
            string Token;
            string ServerResponse;

            //Check for Auth Token
            if (Request.Contains("Token", data))
            {
                Token = Request.Get("Token", data);

                Tokens.Token token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(Token).ID, Tokens.Tokens.GetTokenByToken(Token).Username, Token);

                //Check for Token
                if (Tokens.Tokens.CheckToken(token))
                {
                    //Query Database for Account Details Associated to the Name
                    Connect connect = new Connect();

                    //Username, HWID....etc
                    ServerResponse = connect.QueryUserAccount(token.Username) + "|" + connect.QueryUserLicensing(token.Username); //index[4]

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

        public static string AdminGetAccountDetails(TcpClient clientSocket, string data)
        {
            string Username;
            string token;
            Tokens.Token Token;
            string ServerResponse;

            //Check for Auth Token
            if (Request.Contains("Username", data))
            {
                Username = Request.Get("Username", data);

                if (Request.Contains("Token", data))
                {
                    token = Request.Get("Token", data);

                    Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                    //Query Database for Account Details Associated to the Name
                    Connect connect = new Connect();

                    if (connect.IsAdmin(Token.Username))
                    {
                        if (connect.CheckForAccount(Username))
                        {
                            ServerResponse = connect.QueryUserAccount(Username) + "|" + connect.QueryUserLicensing(Username); //index[4]
                        }
                        else
                        {
                            ServerResponse = "Account not Found";
                        }
                    }
                    else
                    {
                        ServerResponse = "Invalid Permissions";
                    }

                    connect.Close();
                }
                else
                {
                    ServerResponse = "Token Not Provided";
                }
            }
            else
            {
                ServerResponse = "Username Not Provided";
            }

            return ServerResponse;
        }

        public static string ResetHWID(TcpClient clientSocket, string data)
        {
            string Username;
            string token;
            Tokens.Token Token;
            string ServerResponse;

            //Check for Auth Token
            if (Request.Contains("Username", data))
            {
                Username = Request.Get("Username", data);

                if (Request.Contains("Token", data))
                {
                    token = Request.Get("Token", data);

                    Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                    //Query Database for Account Details Associated to the Name
                    Connect connect = new Connect();

                    if (connect.IsAdmin(Token.Username))
                    {
                        //Username, HWID....etc
                        ServerResponse = connect.HWIDReset(Username);
                    }
                    else
                    {
                        ServerResponse = "Invalid Permissions";
                    }

                    connect.Close();
                }
                else
                {
                    ServerResponse = "Token Not Specified";
                }
            }
            else
            {
                ServerResponse = "Username Not Specified";
            }

            return ServerResponse;
        }

        public static string ResetPassword(TcpClient clientSocket, string data)
        {
            string token;
            string Username;
            string Password;
            string HWID;
            string ServerResponse;

            if (Request.Contains("Token", data))
            {
                token = Request.Get("Token", data);

                Tokens.Token Token = new Tokens.Token(((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(), Tokens.Tokens.GetTokenByToken(token).ID, Tokens.Tokens.GetTokenByToken(token).Username, token);

                if (Request.Contains("Username", data))
                {
                    Username = Request.Get("Username", data);

                    //If we arent setting a new password ( assume we're triggering it for a reset )
                    Connect connect = new Connect();

                    if (connect.IsAdmin(Token.Username))
                    {
                        //Login Response
                        ServerResponse = connect.PWReset(Username);
                    }
                    else
                    {
                        ServerResponse = "Invalid Permissions";
                    }

                    connect.Close();
                }
                else
                {
                    ServerResponse = "Username not Specified";
                }
            }
            else
            {
                if (Request.Contains("Username", data))
                {
                    Username = Request.Get("Username", data);

                    //User is asking for a reset, not the bot
                    if (Request.Contains("Password", data))
                    {
                        Password = Request.Get("Password", data);

                        //User is asking for a reset, not the bot
                        if (Request.Contains("HWID", data))
                        {
                            HWID = Request.Get("HWID", data);

                            //If we arent setting a new password ( assume we're triggering it for a reset )
                            Connect connect = new Connect();

                            //Login Response;
                            ServerResponse = connect.ResetPassword(Username, Password, HWID);

                            connect.Close();
                        }
                        else
                        {
                            ServerResponse = "HWID Not Specified";
                        }
                    }
                    else
                    {
                        ServerResponse = "Password Not Specified";
                    }
                }
                else
                {
                    ServerResponse = "Username not Specified";
                }
            }

            return ServerResponse;
        }
    }
}
