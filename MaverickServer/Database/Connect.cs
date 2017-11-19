using MaverickServer.HandleClients.Parse;
using MaverickServer.HandleClients.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MaverickServer.Database
{
    /// <summary>
    /// Deprecated - Forum Integration
    /// </summary>
    public class Connect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;

        public Connect()
        {
            server = "127.0.0.1";
            database = "auth";
            username = "auth";
            password = "password";

            connection = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";");

            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public void Shutdown()
        {
            //TODO: Add / Update LastRequest Time
            using (MySqlCommand com = new MySqlCommand("SHUTDOWN", connection))
            {
                com.ExecuteNonQuery();
            }
        }

        #region Normal API Requests
        /// <summary>
        /// Checks if the UserID exists in the Database
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool MemberID(int UserID)
        {
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE UserID=@UserID", connection);
            com.Parameters.AddWithValue("@UserID", UserID);

            int count = (int)com.ExecuteScalar();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks Database for Login and returns true if it was found
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="HWID"></param>
        /// <returns></returns>
        public string Login(string Username, string Password, string HWID)
        {
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username AND Password=@Password AND HWID=@HWID", connection);
            com.Parameters.AddWithValue("@Username", Username);
            com.Parameters.AddWithValue("@Password", Password);
            com.Parameters.AddWithValue("@HWID", HWID);

            int count = (int)com.ExecuteScalar();

            if (count > 0)
            {
                return "Login Found";
            }
            else
            {
                com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username AND Password=@Password", connection);
                com.Parameters.AddWithValue("@Username", Username);
                com.Parameters.AddWithValue("@Password", Password);

                count = (int)com.ExecuteScalar();

                Console.WriteLine("There are " + count.ToString() + " Accounts linked to these credentials.");

                if (count != 0)
                {
                    //If Invalid HWID - Check if HWID Reset is triggered
                    com = new MySqlCommand("SELECT HWReset FROM Logins WHERE Username=@Username", connection);
                    com.Parameters.AddWithValue("@Username", Username);

                    if ((int)com.ExecuteScalar() > 0)
                    {
                        //Reset HWID
                        com = new MySqlCommand("UPDATE Logins SET HWReset=0, HWID=@HWID WHERE Username=@Username", connection);
                        com.Parameters.AddWithValue("@Username", Username);
                        com.Parameters.AddWithValue("@HWID", HWID);

                        if ((int)com.ExecuteNonQuery() > 0)
                        {
                            return "Login Found";
                        }
                        else
                        {
                            return "Failed to Reset HWID";
                        }
                    }
                    else
                    {
                        return "Invalid HWID";
                    }
                }
                else
                {
                    return "Invalid Password";
                }
            }
        }

        /// <summary>
        /// Registers Username, Password and HWID
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="HWID"></param>
        /// <returns></returns>
        public string Register(string Username, string Password, string Salt, string HWID, string IP)
        {
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            int count = (int)com.ExecuteScalar();

            if (count > 0)
                return "Username Taken";

            com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE HWID=@HWID", connection);
            com.Parameters.AddWithValue("@HWID", HWID);

            count = (int)com.ExecuteScalar();

            if (count > 0)
            {
                com = new MySqlCommand("SELECT * FROM Logins WHERE HWID=@HWID", connection);
                com.Parameters.AddWithValue("@HWID", HWID);

                using (MySqlDataReader reader = com.ExecuteReader())
                    if (reader.Read())
                        return "HWID Taken by (" + reader.GetString(reader.GetOrdinal("Username")) + ")";
                    else
                        return "HWID Taken";
            }

            com = new MySqlCommand("INSERT INTO Logins (Username, Password, Salt, HWID, IP, Banned, Admin) VALUES (@Username, @Password, @Salt, @HWID, @IP, 0, 0)", connection);
            com.Parameters.AddWithValue("@Username", Username);
            com.Parameters.AddWithValue("@Password", Password);
            com.Parameters.AddWithValue("@Salt", Salt);
            com.Parameters.AddWithValue("@HWID", HWID);
            com.Parameters.AddWithValue("@IP", IP);
            com.ExecuteNonQuery();

            return "Registeration Successful";
        }

        public string ResetPassword(string Username, string Password, string HWID)
        {
            string Response = "";

            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            //If we can find the username + account
            if ((int)com.ExecuteScalar() > 0)
            {
                string Salt = GetSalt(Username);

                string Pass = Crypto.CreateHash(Password, Convert.FromBase64String(Salt));

                com = new MySqlCommand("UPDATE Logins SET Password=@Password, Salt=@Salt, PWReset=0 WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);
                com.Parameters.AddWithValue("@Password", Pass);
                com.Parameters.AddWithValue("@Salt", Salt);

                com.ExecuteNonQuery();

                Response = "Password Reset";
            }
            else
            {
                Response = "Account not Found";
            }

            return Response;
        }

        public string GetSalt(string Username)
        {
            MySqlCommand com = new MySqlCommand("SELECT Salt FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            return (string)com.ExecuteScalar();
        }

        /// <summary>
        /// Checks if a user is banned
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public bool IsBanned(string Username)
        {
            MySqlCommand com = new MySqlCommand("SELECT Banned FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            return ((int)com.ExecuteScalar() == 1) ? true : false;
        }

        public string FindUsername(string DiscordId)
        {
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE DiscordID=@DiscordID", connection);
            com.Parameters.AddWithValue("@DiscordID", DiscordId);

            int count = (int)com.ExecuteScalar();

            if (count == 0)
            {
                return "User Not Found";
            }

            com = new MySqlCommand("SELECT Username FROM Logins WHERE DiscordID=@DiscordId", connection);
            com.Parameters.AddWithValue("@DiscordId", DiscordId);

            return (string)com.ExecuteScalar();
        }

        /// <summary>
        /// Ban a user
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public bool BanUser(string Username, Token Token)
        {
            Console.WriteLine("Asked to ban: " + Username);

            MySqlCommand com = new MySqlCommand("SELECT Admin FROM Logins WHERE Username=@Username", connection);

            com.Parameters.AddWithValue("@Username", Token.Username);

            int isAdmin = (int)com.ExecuteScalar();

            if (isAdmin == 0)
                return false;

            com = new MySqlCommand("UPDATE Logins SET Banned=1 WHERE Username=@Username", connection);
            
            com.Parameters.AddWithValue("@Username", Username);

            com.ExecuteNonQuery();

            return true;
        }

        /// <summary>
        /// Asks if a user is admin
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public bool IsAdmin(string Username)
        {
            MySqlCommand com = new MySqlCommand("SELECT Admin FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            int isAdmin = (int)com.ExecuteScalar();

            if (isAdmin == 0)
                return false;
            return true;
        }

        public bool Activate(string Username, string LicenseKey)
        {
            int ID = 0;
            int ProductID = 0;
            string DiscordID = "";
            string DiscordName = "";

            //TODO: Implement LicenseKeys Fully
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM LicenseKeys WHERE LicenseKey=@LicenseKey", connection);
            com.Parameters.AddWithValue("@LicenseKey", LicenseKey);

            Int32 count = (Int32)com.ExecuteScalar();

            if (count <= 0)
            {
                Console.WriteLine("Invalid LicenseKey");

                return false;
            }
            else
            {
                //TODO: Implement LicenseKeys Fully
                com = new MySqlCommand("SELECT ProductID, DiscordID, DiscordName FROM LicenseKeys WHERE LicenseKey=@LicenseKey AND Used=0", connection);
                com.Parameters.AddWithValue("@LicenseKey", LicenseKey);

                using (MySqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Reader has result");

                        ProductID = reader.GetInt32(0);
                        DiscordID = reader.GetString(1);
                        DiscordName = reader.GetString(2);

                        Console.WriteLine("Result: " + ProductID + " -> " + DiscordID + " -> " + DiscordName);
                    }
                }

                Console.WriteLine("Result: " + ProductID);
            }

            if (ProductID <= 0)
            {
                Console.WriteLine("ProductID is 0");

                return false;
            }

            com = new MySqlCommand("SELECT ID FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            ID = (int)com.ExecuteScalar();

            if (ID <= 0)
            {
                Console.WriteLine("MemberID is 0");

                return false;
            }

            com = new MySqlCommand("UPDATE LicenseKeys SET Used=1 WHERE LicenseKey=@LicenseKey", connection);
            com.Parameters.AddWithValue("@LicenseKey", LicenseKey);
            com.ExecuteNonQuery();

            //TODO: Implement LicenseKeys Fully
            com = new MySqlCommand("SELECT Count(*) FROM Licensing WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            count = (Int32)com.ExecuteScalar();

            if (count > 0)
            {
                string Products = "";
                string Licenses = "";

                //TODO: Implement LicenseKeys Fully
                com = new MySqlCommand("SELECT Products, Licenses FROM Licensing WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);

                using (MySqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Reader has result");

                        Products = reader.GetString(0);
                        Licenses = reader.GetString(1);

                        Console.WriteLine("Result: " + Products + " -> " + Licenses);
                    }
                }

                //Licensing has an entry
                com = new MySqlCommand("UPDATE Licensing SET Products=@Products, Licenses=@Licenses WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Products", Products + "," + ProductID);
                com.Parameters.AddWithValue("@Licenses", Licenses + "," + LicenseKey);
                com.Parameters.AddWithValue("@Username", Username);
                com.ExecuteNonQuery();

                //Licensing has an entry
                com = new MySqlCommand("UPDATE Logins SET DiscordID=@DiscordID, DiscordName=@DiscordName WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@DiscordID", DiscordID);
                com.Parameters.AddWithValue("@DiscordName", DiscordName);
                com.Parameters.AddWithValue("@Username", Username);
                com.ExecuteNonQuery();
            }
            else
            {
                //Create an entry
                com = new MySqlCommand("INSERT INTO Licensing (Username, Products, Licenses) VALUES (@Username, @Products, @Licenses)", connection);
                com.Parameters.AddWithValue("@Username", Username);
                com.Parameters.AddWithValue("@Products", ProductID);
                com.Parameters.AddWithValue("@Licenses", LicenseKey);
                com.ExecuteNonQuery();

                //Licensing has an entry
                com = new MySqlCommand("UPDATE Logins SET DiscordID=@DiscordID, DiscordName=@DiscordName WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@DiscordID", DiscordID);
                com.Parameters.AddWithValue("@DiscordName", DiscordName);
                com.Parameters.AddWithValue("@Username", Username);
                com.ExecuteNonQuery();
            }

            return true;
        }

        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public List<Product> QueryProducts()
        {
            List<Product> temp = new List<Product>();

            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Reading Products");

                        Product product = new Product();
                        product.SetFromSQL(reader);

                        temp.Add(product);
                    }
                }
            }

            return temp;
        }

        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public List<int> QueryUserProducts(int ID, string Username)
        {
            List<int> temp = new List<int>();

            //Give all products to MaverickCheats ( Bot )
            if (Username == "MaverickCheats" || Username == "CrispyCheats")
            {
                //Handle Manual Products
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Reading Owned Products");

                            int owned = reader.GetInt32(0);

                            Console.WriteLine("Owned Products: " + owned);

                            temp.Add(owned);
                        }
                    }
                }

                return temp;
            }

            //Handle Free Products
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products WHERE Free=1", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Reading Free Products");

                        //Get Product ID
                        int owned = reader.GetInt32(0);

                        if (owned != 0)
                            temp.Add(owned);       
                    }
                }
            }

            //API Request for the Products the Forum Username Owns
            string Products = new WebClient().DownloadString("http://api.maverickcheats.eu/forum/api/index.php?Request=UserGroups&UserID=" + ID);

            if (Products != "")
            {
                if (Products.Contains(","))
                {
                    //Multiple Product IDs
                    foreach (string ProductIDs in Products.Split(','))
                    {
                        //Single Product ID
                        try
                        {
                            int ProductID = Convert.ToInt32(ProductIDs);

                            if (Caches.Products.Any(Product => Product.group == ProductID))
                            {
                                //Convert to Int
                                temp.Add(Caches.Products.First(Product => Product.group == ProductID).id);
                            }
                        }
                        catch
                        {
                            //Not an INT - Ignore
                        }
                    }
                }
                else
                {
                    //Single Product ID
                    try
                    {
                        int ProductID = Convert.ToInt32(Products);

                        if (Caches.Products.Any(Product => Product.group == ProductID))
                        {
                            //Convert to Int
                            temp.Add(Caches.Products.First(Product => Product.group == ProductID).id);
                        }
                    }
                    catch
                    {
                        //Not an INT - Ignore
                    }
                }
            }

            return temp.OrderBy(id => id).ToList();
        }

        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public List<News> QueryNewsfeed()
        {
            List<News> temp = new List<News>();

            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Newsfeed", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Reading Products");

                        News news = new News(reader);

                        temp.Add(news);
                    }
                }
            }

            return temp;
        }

        /// <summary>
        /// Returns a clean query of the account
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public string QueryUserAccount(string Username)
        {
            string User = "";

            MySqlCommand com = new MySqlCommand("SELECT * FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            using (MySqlDataReader reader = com.ExecuteReader())
            {
                if (reader.Read())
                {
                    User += reader.GetInt32(0)
                        + "|" + (reader.IsDBNull(1) ? "No Username" : reader.GetString(1))
                        + "|" + (reader.IsDBNull(4) ? "No HWID" : reader.GetString(4))
                        + "|" + (reader.IsDBNull(6) ? "No Discord ID" : reader.GetString(6))
                        + "|" + (reader.IsDBNull(7) ? "No Discord Name" : reader.GetString(7))
                        + "|" + reader.GetInt32(11)
                        + "|" + reader.GetInt32(12);
                }
            }

            return User;
        }

        /// <summary>
        /// Returns a Clean Query of the Licenses
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public string QueryUserLicensing(string Username)
        {
            string Licenses = "";

            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Licensing WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            int count = (int)com.ExecuteScalar();

            if (count == 0)
            {
                Licenses += "No Products" + "|" + "No LicenseKeys";
            }
            else
            {
                com = new MySqlCommand("SELECT * FROM Licensing WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);

                using (MySqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //Products = index[0]
                        //LicenseKeys = index[1]
                        Licenses += (reader.IsDBNull(2) ? "No Products" : reader.GetString(2)) + "|" + (reader.IsDBNull(3) ? "No LicenseKeys" : reader.GetString(3));
                    }
                }
            }

            return Licenses;
        }

        /// <summary>
        /// Updates the Skype Column on the Data for a user
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Skypes"></param>
        /// <returns></returns>
        public bool UpdateLastRequest(string Username)
        {
            MySqlCommand com = new MySqlCommand("UPDATE Logins SET LastRequest=@DateTime WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);
            com.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            com.ExecuteNonQuery();

            return true;
        }

        /// <summary>
        /// Updates the Skype Column on the Data for a user
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Skypes"></param>
        /// <returns></returns>
        public bool UpdateAPICheck(string Username, string LastDevice = "", string Name = "")
        {
            bool foundname = false;
            string OldNames = "";

            MySqlCommand com = new MySqlCommand("SELECT * FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            using (MySqlDataReader reader = com.ExecuteReader())
            {
                if (reader.Read())
                {
                    OldNames = (reader.IsDBNull(reader.GetOrdinal("Names")) ? "" : reader.GetString(reader.GetOrdinal("Names")));

                    if (OldNames.Contains(","))
                    {
                        string[] names = OldNames.Split(',');

                        foreach (string name in names)
                        {
                            if (name.ToLower() == Name.ToLower())
                            {
                                foundname = true;

                                break;
                            }
                        }
                    }
                    else if (OldNames.ToLower() == Name.ToLower())
                    {
                        foundname = true;
                    }
                }
            }

            if (!foundname)
            {
                com = new MySqlCommand("UPDATE Logins SET LastDevice=@LastDevice, Names=@Names WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);
                com.Parameters.AddWithValue("@LastDevice", LastDevice);
                com.Parameters.AddWithValue("@Names", ((OldNames != "") ? "," : "") + Name);
                com.ExecuteNonQuery();
            }
            else
            {
                com = new MySqlCommand("UPDATE Logins SET LastDevice=@LastDevice WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);
                com.Parameters.AddWithValue("@LastDevice", LastDevice);
                com.ExecuteNonQuery();
            }

            return true;
        }

        /// <summary>
        /// Check if Password is awaiting to be reset
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public bool PasswordReset(string Username)
        {
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            if ((int)com.ExecuteScalar() > 0)
            {
                com = new MySqlCommand("SELECT PWReset FROM Logins WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);

                if ((int)com.ExecuteScalar() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region DiscordBot API Requests
        /// <summary>
        /// Checks if the user owns a key already
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="DiscordID"></param>
        /// <returns></returns>
        public bool LicenseCheck(int ProductID, string DiscordID, out string LicenseKey)
        {
            //TODO: Implement LicenseKeys Fully
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM LicenseKeys WHERE ProductID=@ProductID AND DiscordID=@DiscordID", connection);
            com.Parameters.AddWithValue("@ProductID", ProductID);
            com.Parameters.AddWithValue("@DiscordID", DiscordID);

            Int32 count = (Int32)com.ExecuteScalar();

            if (count > 0)
            {
                com = new MySqlCommand("SELECT LicenseKey FROM LicenseKeys WHERE ProductID=@ProductID AND DiscordID=@DiscordID", connection);
                com.Parameters.AddWithValue("@ProductID", ProductID);
                com.Parameters.AddWithValue("@DiscordID", DiscordID);

                LicenseKey = (string)com.ExecuteScalar();

                Console.WriteLine("Product Key exists");

                return true;
            }
            else
            {
                LicenseKey = "";

                Console.WriteLine("Product Key doesnt exist");

                return false;
            }
        }

        public bool GenerateKey(int ProductID, string DiscordID, string DiscordName, string LicenseKey)
        {
            //TODO: Implement LicenseKeys Fully
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM LicenseKeys WHERE LicenseKey=@LicenseKey", connection);
            com.Parameters.AddWithValue("@LicenseKey", LicenseKey);

            Int32 count = (Int32)com.ExecuteScalar();

            if (count > 0)
            {
                Console.WriteLine("LicenseKey already exists");

                return false;
            }
            else
            {
                //Create an entry
                com = new MySqlCommand("INSERT INTO LicenseKeys (ProductID, DiscordID, DiscordName, LicenseKey) VALUES (@ProductID, @DiscordID, @DiscordName, @LicenseKey)", connection);
                com.Parameters.AddWithValue("@ProductID", ProductID);
                com.Parameters.AddWithValue("@DiscordID", DiscordID);
                com.Parameters.AddWithValue("@DiscordName", DiscordName);
                com.Parameters.AddWithValue("@LicenseKey", LicenseKey);
                com.ExecuteNonQuery();
            }

            return true;
        }

        public string HWIDReset(string Username)
        {
            string Response = "";

            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            if ((int)com.ExecuteScalar() > 0)
            {
                com = new MySqlCommand("UPDATE Logins SET HWReset=1 WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);

                if (com.ExecuteNonQuery() > 0)
                {
                    Response = "HWID Reset";
                }
                else
                {
                    Response = "Failed to Reset HWID";
                }
            }
            else
            {
                Response = "DiscordID not Found";
            }

            return Response;
        }

        public string PWReset(string Username)
        {
            string Response = "";

            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            if ((int)com.ExecuteScalar() > 0)
            {
                com = new MySqlCommand("UPDATE Logins SET PWReset=1 WHERE Username=@Username", connection);
                com.Parameters.AddWithValue("@Username", Username);

                if (com.ExecuteNonQuery() > 0)
                {
                    Response = "Password Reset";
                }
                else
                {
                    Response = "Failed to Reset Password";
                }
            }
            else
            {
                Response = "DiscordID not Found";
            }

            return Response;
        }

        public bool CheckForAccount(string Username)
        {
            int Users = 0;

            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins WHERE Username=@Username", connection);
            com.Parameters.AddWithValue("@Username", Username);

            Users = (int)com.ExecuteScalar();

            if (Users > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Server GUI Functions
        public bool UpdateLogin(int ID, string Column, string OriginalValue, string NewValue)
        {
            Console.WriteLine("Column: {0} - OriginalValue: {1} NewValue: {2}", Column, OriginalValue, NewValue);

            MySqlCommand com = new MySqlCommand("UPDATE Logins SET " + Column + "=@NewValue WHERE ID=@MemberID", connection);
            com.Parameters.AddWithValue("@NewValue", NewValue);
            com.Parameters.AddWithValue("@MemberID", ID);
            com.ExecuteNonQuery();

            return true;
        }

        public bool UpdateLicenseKeys(int ID, string Column, string OriginalValue, string NewValue)
        {
            Console.WriteLine("Column: {0} - OriginalValue: {1} NewValue: {2}", Column, OriginalValue, NewValue);

            MySqlCommand com = new MySqlCommand("UPDATE LicenseKeys SET " + Column + "=@NewValue WHERE ID=@LicenseKeyID", connection);
            com.Parameters.AddWithValue("@NewValue", NewValue);
            com.Parameters.AddWithValue("@LicenseKeyID", ID);
            com.ExecuteNonQuery();

            return true;
        }

        public bool UpdateLicensing(int ID, string Column, string OriginalValue, string NewValue)
        {
            Console.WriteLine("Column: {0} - OriginalValue: {1} NewValue: {2}", Column, OriginalValue, NewValue);

            MySqlCommand com = new MySqlCommand("UPDATE Licensing SET " + Column + "=@NewValue WHERE ID=@LicensingID", connection);
            com.Parameters.AddWithValue("@NewValue", NewValue);
            com.Parameters.AddWithValue("@LicensingID", ID);
            com.ExecuteNonQuery();

            return true;
        }

        public bool UpdateProducts(int ID, string Column, string OriginalValue, string NewValue)
        {
            Console.WriteLine("Column: {0} - OriginalValue: {1} NewValue: {2}", Column, OriginalValue, NewValue);

            MySqlCommand com = new MySqlCommand("UPDATE Products SET " + Column + "=@NewValue WHERE ID=@ProductID", connection);
            com.Parameters.AddWithValue("@NewValue", NewValue);
            com.Parameters.AddWithValue("@ProductID", ID);
            com.ExecuteNonQuery();

            return true;
        }

        public bool UpdateNews(int ID, string Column, string OriginalValue, string NewValue)
        {
            Console.WriteLine("Column: {0} - OriginalValue: {1} NewValue: {2}", Column, OriginalValue, NewValue);

            MySqlCommand com = new MySqlCommand("UPDATE Newsfeed SET " + Column + "=@NewValue WHERE ID=@NewsID", connection);
            com.Parameters.AddWithValue("@NewValue", NewValue);
            com.Parameters.AddWithValue("@NewsID", ID);
            com.ExecuteNonQuery();

            return true;
        }
        #endregion

        #region Databasewide Queries
        public int LoginCount()
        {
            int Users = 0;

            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM Logins", connection);

            Users = (int)com.ExecuteScalar();

            return Users;
        }

        /*
        public int OnlineCount()
        {
            int Users = 0;

            MySqlCommand com = new MySqlCommand("SELECT * FROM Logins", connection);

            using (MySqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateTime db_datetime = reader.GetDateTime(12);

                    //If the request was recent and we can surpass the current time by adding a bit of time, they must have recently called our server
                    if (db_datetime.AddMinutes(5) > DateTime.Now)
                    {
                        Users++;
                    }
                }
            }

            return Users; //Users;
        }
        */

        public string QueryUsers(string Username, bool strict = false)
        {
            string Users = "";

            MySqlCommand com = new MySqlCommand("SELECT * FROM Logins WHERE Username " + (strict ? "=" : "LIKE") + " @Username", connection);
            com.Parameters.AddWithValue("@Username", (strict ? Username : "%" + Username + "%"));

            using (MySqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Users += reader.GetInt32(0) + "&" + (reader.IsDBNull(1) ? "No Username" : reader.GetString(1)) + "&" + (reader.IsDBNull(3) ? "No HWID" : reader.GetString(3)) + "&" + (reader.IsDBNull(5) ? "No DiscordID" : reader.GetString(5)) + "&" + (reader.IsDBNull(6) ? "No DiscordName" : reader.GetString(6)) + "|";
                }
            }

            if (Users.Length > 0)
            {
                Users = Users.Remove(Users.Length - 1);
            }
            else
            {
                Users = "No Enteries Found";
            }

            return Users;
        }
        #endregion
    }
}
