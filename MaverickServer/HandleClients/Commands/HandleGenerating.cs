using MaverickServer.Database;
using Server.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients
{
    public class HandleGenerating
    {
        public static string GenerateKey(TcpClient clientSocket, string data)
        {
            int ProductId;
            string DiscordID;
            string DiscordName;
            string ServerResponse;

            if (Request.Contains("ProductID", data))
            {
                ProductId = Convert.ToInt32(Request.Get("ProductID", data));

                if (Request.Contains("DiscordID", data))
                {
                    DiscordID = Request.Get("DiscordID", data);

                    if (Request.Contains("DiscordName", data))
                    {
                        DiscordName = Request.Get("DiscordName", data);

                        string LicenseKey;

                        Connect connect = new Connect();

                        //Check if there's a product with the ID and that its not free ( Free = No Keys )
                        if (connect.QueryProducts().Any(product => product.id == ProductId))
                        {
                            if (connect.QueryProducts().Any(product => product.id == ProductId))
                            {
                                if (!connect.LicenseCheck(ProductId, DiscordID, out LicenseKey))
                                {
                                    if (GenerateUniqueKey(ProductId, DiscordID, DiscordName, out LicenseKey))
                                    {
                                        ServerResponse = "Generation Successful=" + LicenseKey;
                                    }
                                    else
                                    {
                                        ServerResponse = "Generation Failed";
                                    }
                                }
                                else
                                {
                                    ServerResponse = "LicenseKey=" + LicenseKey;
                                }
                            }
                            else
                            {
                                ServerResponse = "That Product ID is Free, Free products do not need LicenseKeys.";
                            }
                        }
                        else
                        {
                            ServerResponse = "Product ID does not exist";
                        }

                        connect.Close();
                    }
                    else
                    {
                        ServerResponse = "DiscordName Parameter was not provided";
                    }
                }
                else
                {
                    ServerResponse = "DiscordID Parameter was not provided";
                }
            }
            else
            {
                ServerResponse = "ProductId Parameter was not provided";
            }

            return ServerResponse;
        }

        private static bool GenerateUniqueKey(int ProductID, string DiscordID, string DiscordName, out string LicenseKey)
        {
            Connect connect = new Connect();

            while (true)
            {
                LicenseKey = GetSerialKeyAlphaNumaric();

                if (connect.GenerateKey(ProductID, DiscordID, DiscordName, LicenseKey))
                    break;
            }

            connect.Close();

            return true;
        }

        #region Generation Functions
        public static string GetSerialKeyAlphaNumaric()
        {
            Guid newguid = Guid.NewGuid();
            string randomStr =
            newguid.ToString("N");
            string tracStr = randomStr.Substring(0, 16);
            tracStr = tracStr.ToUpper();
            char[] newKey =
            tracStr.ToCharArray();
            string newSerialNumber = "";
            
            newSerialNumber = AppendSpecifiedStr(16, "-", newKey);

            return newSerialNumber;
        }

        private static string AppendSpecifiedStr(int length, string str, char[] newKey)
        {
            string newKeyStr = "";
            int k = 0;
            for (int i = 0; i < length; i++)
            {
                for (k = i; k < 4 + i; k++)
                {
                    newKeyStr += newKey[k];
                }
                if (k == length)
                {
                    break;
                }
                else
                {
                    i = (k) - 1;
                    newKeyStr += str;
                }
            }

            return newKeyStr;
        }
        #endregion
    }
}