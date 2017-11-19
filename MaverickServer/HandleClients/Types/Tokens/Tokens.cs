using MaverickServer.Database;
using MaverickServer.Logs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients.Tokens
{
    public class Tokens
    {
        public static List<Token> AuthTokens = new List<Token>();

        /// <summary>
        /// Checks for Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool CheckToken(Token token)
        {
            List<Token> TempAuthTokens = new List<Token>();

            lock (AuthTokens)
            {
                TempAuthTokens = AuthTokens;
            }

            int index = 0;
            foreach (Token toke in TempAuthTokens)
            {
                if (toke.IP == token.IP)
                {
                    Console.WriteLine("Token IPs are the Same!");

                    if (toke.Username == token.Username)
                    {
                        Console.WriteLine("Token Usernames are the Same!");

                        if (toke.AuthToken == token.AuthToken)
                        {
                            lock (AuthTokens)
                            {
                                AuthTokens[index].LastRequest = DateTime.Now;
                            }

                            Console.WriteLine("Tokens are the Same!");

                            return true;
                        }
                    }
                }

                index++;
            }

            //Return false if not found
            return false;
        }

        /// <summary>
        /// Reutnr username associated with the token
        /// </summary>
        /// <param name="Roken"></param>
        /// <returns></returns>
        public static Token GetToken(string IP)
        {
            List<Token> tokens = new List<Token>();

            lock (AuthTokens)
            {
                tokens = AuthTokens;
            }

            foreach (Token token in tokens)
            {
                if (token.IP == IP)
                {
                    return token;
                }
            }

            //Return false if not found
            return null;
        }

        /// <summary>
        /// Reutnr username associated with the token
        /// </summary>
        /// <param name="Roken"></param>
        /// <returns></returns>
        public static Token GetTokenByToken(string Token)
        {
            List<Token> tokens = new List<Token>();

            lock (AuthTokens)
            {
                tokens = AuthTokens;
            }

            foreach (Token token in tokens)
            {
                if (token.AuthToken == Token)
                {
                    return token;
                }
            }

            //Return false if not found
            return null;
        }

        /// <summary>
        /// Generates Random Token
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GenerateToken(string Ip, int ID, string Username)
        {
            //Check if the IP is Null or Invalid
            if (Ip != null && Ip != "")
            {
                string random = FilterToken(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));

                Token token = new Token(Ip, ID, Username, random);

                //While the token we generated is already used, try and regen a new one
                while (CheckToken(token))
                {
                    random = FilterToken(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));

                    token = new Token(Ip, ID, Username, random);
                }

                lock (AuthTokens)
                {
                    //Add token to the memory
                    AuthTokens.Add(token);
                }

                return random;
            }

            return null;
        }

        /// <summary>
        /// Filters Guid Input to create a clean GUID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string FilterToken(string input)
        {
            input = input.Replace("+", "");
            input = input.Replace("=", "");

            return input;
        }
    }
}
