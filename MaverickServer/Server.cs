using MaverickServer.Database;
using MaverickServer.HandleClients;
using MaverickServer.HandleClients.Tokens;
using MaverickServer.Logs;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using MaverickServer.HandleClients.Types;
using MaverickServer.Types;
using System.Text;
using NetFwTypeLib;
using System.Linq;

namespace MaverickServer
{
    public partial class Server : Form
    {
        public static List<Connection> TcpClients = new List<Connection>();
        public static List<AntiSpam> Connections = new List<AntiSpam>();
        public static List<Request> Requests = new List<Request>();

        //MaxRequests per Minute
        public static int RequestMax = 25;
        //MaxConnections per Minutes
        public static int ConnectionsMax = 25;

        public static DateTime CacheTime = DateTime.Now;
        public static string Version = new StreamReader("Version.txt").ReadLine();

        public Server()
        {
            handler = new ConsoleEventDelegate(ConsoleCallback);
            SetConsoleCtrlHandler(handler, true);


            AppDomain.CurrentDomain.UnhandledException += (sender, arg) => HandleUnhandledException(arg.ExceptionObject as Exception);

            InitializeComponent();

            if (!Directory.Exists(Environment.CurrentDirectory + "\\Logs\\"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Logs\\");

            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory + "\\");

            new Thread(serverThread).Start();

            new Thread(cacheThread).Start();
        }

        //Handle Server Shutdown - Cache Tokens into Text File ( Yes or No ) [Forced Udpate or Silent Update]
        private static bool ConsoleCallback()
        {
            DialogResult dialogResult = MessageBox.Show("Is this a Silent Update?", "Server Cache", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Server.CacheTokens();
            }
            else if (dialogResult == DialogResult.No)
            {
                //Immediate Exit - Make sure cache is clear
                try
                {
                    File.Delete(Environment.CurrentDirectory + "\\Cache\\Tokens.txt");
                }
                catch
                {

                }
            }
            else
            {
                //Cache Tokens and Assume Silent Update
                Server.CacheTokens();
            }

            Environment.Exit(0);

            return true;
        }

        private void serverThread()
        {
#if DEBUG
            TcpListener serverSocket = new TcpListener(6969);
#else
            TcpListener serverSocket = new TcpListener(6969);
#endif
            TcpClient clientSocket = default(TcpClient); //ClientSocket
            serverSocket.Start();
            Console.Write(" >> " + "Server Started");

            int counter = 0;
            while (true)
            {
                try
                {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();

                    string IPAddress = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString();

                    Console.WriteLine("IPAddress: " + IPAddress);

                    if (IPAddress != "127.0.0.1" && IPAddress != "109.199.125.101")
                    {
                        bool found = false;
                        AntiSpam antispam = new AntiSpam();
                        foreach (AntiSpam cachedconnection in new List<AntiSpam>(Connections))
                        {
                            if (cachedconnection.IP == IPAddress)
                            {
                                found = true;

                                antispam = cachedconnection;
                            }
                        }

                        if (found)
                        {
                            Console.WriteLine("IP Found -> " + antispam.Attempts + " IP= " + antispam.IP);

                            if (antispam.Attempts > ConnectionsMax)
                            {
                                //Check if it exceeds double the connections max
                                if (antispam.Attempts == (ConnectionsMax * 2))
                                {
                                    Console.WriteLine("Blocking IP");

                                    INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                                    INetFwRule firewallRule = firewallPolicy.Rules.OfType<INetFwRule>().Where(x => x.Name == "Blocked: " + IPAddress).FirstOrDefault();

                                    if (firewallRule == null)
                                    {
                                        firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                                        firewallRule.Name = "Blocked: " + IPAddress;
                                        firewallRule.Description = "Blocking Traffic";
                                        firewallRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                                        firewallRule.LocalPorts = "6969";
                                        firewallRule.RemoteAddresses = IPAddress;
                                        firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                                        firewallRule.Enabled = true;
                                        firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                                        firewallPolicy.Rules.Add(firewallRule);
                                    }

                                    Log.WriteLine("Blocked IP (Connection Quota) [" + IPAddress + "]", "Blocked");
                                }

                                foreach (AntiSpam cachedconnection in new List<AntiSpam>(Connections))
                                {
                                    if (cachedconnection.IP == antispam.IP)
                                    {
                                        cachedconnection.Attempts = cachedconnection.Attempts + 1;
                                        cachedconnection.Attempted = DateTime.Now;
                                    }
                                }

                                Console.WriteLine("Blocked Connection {0} Times -> " + IPAddress, antispam.Attempts);

                                clientSocket.NoDelay = true;

                                NetworkStream networkStream = clientSocket.GetStream();

                                //Byte Streams
                                byte[] outStream = Encoding.ASCII.GetBytes("Connection Quota Reached");

                                byte[] outSize = BitConverter.GetBytes(outStream.Length);

                                //Network Streams
                                networkStream.Write(outSize, 0, outSize.Length);
                                networkStream.Write(outStream, 0, outStream.Length);
                                networkStream.Flush();

                                Console.Write(">> Size=" + BitConverter.ToInt32(outSize, 0) + " Response: Connection Quota Reached");

                                clientSocket.Close();

                                continue;
                            }
                            else
                            {
                                foreach (AntiSpam cachedconnection in new List<AntiSpam>(Connections))
                                {
                                    if (cachedconnection.IP == antispam.IP)
                                    {
                                        cachedconnection.Attempts = cachedconnection.Attempts + 1;
                                        cachedconnection.Attempted = DateTime.Now;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("IP Not Found (Adding IP) -> IP= " + IPAddress);

                            Connections.Add(new AntiSpam(IPAddress, 1, DateTime.Now));
                        }
                    }

                    clientSocket.NoDelay = true;

                    clientSocket.ReceiveTimeout = (60000 * 5);

                    Console.Write(" >> " + "Client No:" + Convert.ToString(counter) + " started!");

                    Connection connection = new Connection(counter, clientSocket);
                    HandleClient client = new HandleClient();
                    client.startClient(connection);

                    lock (TcpClients)
                    {
                        TcpClients.Add(connection);
                    }

                    Log.WriteLine("Client Connected [" + IPAddress + "]", "Connections");
                }
                catch (Exception ex)
                {
                    Log.WriteLine("Client Connection Failed", "DEBUG");
                    Log.WriteLine(ex.ToString(), "DEBUG");
                }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.Write(" >> " + "exit");
            Console.ReadLine();
        }

        private void cacheThread()
        {
            DateTime date = DateTime.Now;

            while (true)
            {
                List<Connection> TcpClientsTemp;

                lock (TcpClients)
                {
                    TcpClientsTemp = new List<Connection>(TcpClients);
                }

                if (TcpClientsTemp.Count > 0)
                {
                    int index = 0;
                    int removed = 0;
                    foreach (Connection client in new List<Connection>(TcpClientsTemp))
                    {
                        if (client == null || client.clientHandle == null || client.clientSocket == null || !client.clientSocket.Connected || client.disconnected || client.timeout.AddMinutes(5) < DateTime.Now)
                        {
                            //Dispose Object???
                            try
                            {
                                if (client.clientSocket != null)
                                {
                                    client.clientSocket.Close();

                                    client.clientSocket = null;
                                }

                                if (client.thread != null)
                                {
                                    client.thread.Abort();

                                    client.thread = null;
                                }

                                if (client.networkStream != null)
                                {
                                    client.networkStream = null;
                                }

                                if (client.clientHandle != null)
                                {
                                    client.clientHandle = null;
                                }

                                if ((index - removed) <= TcpClientsTemp.Count)
                                {
                                    Console.WriteLine("Removing TCPClient Index {0} Calculated Index: {1} List Length: {2}", index, (index - removed), TcpClientsTemp.Count);

                                    Log.WriteLine("Disposing Socket", "TCPCleanUp");

                                    TcpClientsTemp.RemoveAt((index - removed));

                                    removed++;
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.WriteLine("Disposing Socket Failed" + " Failed", "ERROR");
                                Log.WriteLine(ex.ToString(), "ERROR");
                            }
                        }

                        index++;
                    }

                    lock (TcpClients)
                    {
                        TcpClients = TcpClientsTemp;
                    }
                }

                if (date.AddMinutes(1) < DateTime.Now)
                {
                    date = DateTime.Now;

                    Connections.Clear();
                    Requests.Clear();
                }

                /*
                int index = 0;
                int count = new List<Connection>(Connections).Count;
                if (count > 0)
                {
                    foreach (Connection conenction in new List<Connection>(Connections))
                    {
                        //If key is older than 5 minutes / hasnt been touched in 5 mins, delete it
                        if (conenction.Time.AddMinutes(1) < DateTime.Now)
                        {
                            if (index <= count && count != 0)
                            {
                                Console.WriteLine("Removing Connection Index at {0} (List Count {1})", index, count);

                                Connections.RemoveAt(index);
                            }
                            else
                            {
                                Console.WriteLine("Failed to remove Connection Index {0} (List Count {1})", index, count);
                            }
                        }

                        index++;
                    }
                }

                index = 0;
                count = new List<Request>(Requests).Count;

                if (count > 0)
                {
                    foreach (Request request in new List<Request>(Requests))
                    {
                        //If key is older than x minutes / hasnt been touched in x mins, delete it
                        if (request.Time.AddMinutes(1) < DateTime.Now)
                        {
                            if (index <= count && count != 0)
                            {
                                Console.WriteLine("Removing Request Index at {0} (List Count {1})", index, count);

                                Requests.RemoveAt(index);
                            }
                            else
                            {
                                Console.WriteLine("Failed to remove Request Index {0} (List Count {1})", index, count);
                            }
                        }

                        index++;
                    }
                }
                */

                List<Token> AuthTokensTemp;

                lock (Tokens.AuthTokens)
                {
                    AuthTokensTemp = new List<Token>(Tokens.AuthTokens);
                }

                if (AuthTokensTemp.Count > 0)
                {
                    int index = 0;
                    int removed = 0;
                    foreach (Token token in new List<Token>(AuthTokensTemp))
                    {
                        //If key is older than 5 minutes / hasnt been touched in 5 mins, delete it
                        if (token.LastRequest.AddMinutes(5) < DateTime.Now)
                        {
                            Console.Write(token.Username + "'s token is too old and is being removed!");

                            if ((index - removed) <= AuthTokensTemp.Count)
                            {
                                Console.WriteLine("Removing Token Index {0} New Index: {1} List Length: {2}", index, (index - removed), AuthTokensTemp.Count);

                                Log.WriteLine("Disposing Token [" + token.AuthToken + "]", "TokenCleanUp");

                                AuthTokensTemp.RemoveAt((index - removed));

                                removed++;
                            }
                        }

                        index++;
                    }

                    lock (Tokens.AuthTokens)
                    {
                        Tokens.AuthTokens = AuthTokensTemp;
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private void Server_Load(object sender, EventArgs e)
        {
            //Saves Memory until we need it???

            /*
            // TODO: This line of code loads data into the 'maverick_Logins.Logins' table. You can move, or remove it, as needed.
            this.loginsTableAdapter.Fill(this.maverick_Logins.Logins);
            // TODO: This line of code loads data into the 'maverick_Products.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.maverick_Products.Products);
            // TODO: This line of code loads data into the 'maverick_Newsfeed.Newsfeed' table. You can move, or remove it, as needed.
            this.newsfeedTableAdapter.Fill(this.maverick_Newsfeed.Newsfeed);
            // TODO: This line of code loads data into the 'maverick_LicenseKeys.LicenseKeys' table. You can move, or remove it, as needed.
            this.licenseKeysTableAdapter.Fill(this.maverick_LicenseKeys.LicenseKeys);
            // TODO: This line of code loads data into the 'maverick_Licensing.Licensing' table. You can move, or remove it, as needed.
            this.licensingTableAdapter.Fill(this.maverick_Licensing.Licensing);
            */

            LoadTokens();

            gridView2.CellValueChanged += GridView2_CellValueChanged;

            flatComboBox1.SelectedIndex = flatComboBox1.Items.IndexOf("Logins");

            gridView2.PopulateColumns();

            foreach (GridColumn item in gridView2.Columns)
            {
                if (item == gridView2.Columns["LastRequest"])
                {
                    item.DisplayFormat.FormatType = FormatType.DateTime;
                    item.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                }
            }
        }

        private void GridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (flatComboBox1.Text == "Logins")
            {
                int ID;

                Console.WriteLine("Updating Logins");

                Connect connect = new Connect();

                try
                {
                    ID = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    Console.WriteLine("Login ID: " + ID);
                }
                catch
                {
                    Console.WriteLine("Member ID Conversion Failed: " + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    return;
                }

                connect.UpdateLogin(ID, e.Column.FieldName, gridView2.ActiveEditor.OldEditValue.ToString(), gridView2.ActiveEditor.EditValue.ToString());

                connect.Close();

                Console.WriteLine("Updated Logins");
            }
            else if (flatComboBox1.Text == "LicenseKeys")
            {
                int ID;

                Console.WriteLine("Updating LicenseKeys");

                Connect connect = new Connect();

                try
                {
                    ID = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    Console.WriteLine("LicenseKey ID: " + ID);
                }
                catch
                {
                    Console.WriteLine("LicenseKey ID Conversion Failed: " + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    return;
                }

                connect.UpdateLicenseKeys(ID, e.Column.FieldName, gridView2.ActiveEditor.OldEditValue.ToString(), gridView2.ActiveEditor.EditValue.ToString());

                connect.Close();

                Console.WriteLine("Updated LicenseKeys");
            }
            else if (flatComboBox1.Text == "Licensing")
            {
                int ID;

                Console.WriteLine("Updating Licensing");

                Connect connect = new Connect();

                try
                {
                    ID = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    Console.WriteLine("Licensing ID: " + ID);
                }
                catch
                {
                    Console.WriteLine("Licensing ID Conversion Failed: " + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    return;
                }

                connect.UpdateLicensing(ID, e.Column.FieldName, gridView2.ActiveEditor.OldEditValue.ToString(), gridView2.ActiveEditor.EditValue.ToString());

                connect.Close();

                Console.WriteLine("Updated Licensing");
            }
            else if (flatComboBox1.Text == "Products")
            {
                int ID;

                Console.WriteLine("Updating Products");

                Connect connect = new Connect();

                try
                {
                    ID = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    Console.WriteLine("Products ID: " + ID);
                }
                catch
                {
                    Console.WriteLine("Products ID Conversion Failed: " + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    return;
                }

                connect.UpdateProducts(ID, e.Column.FieldName, gridView2.ActiveEditor.OldEditValue.ToString(), gridView2.ActiveEditor.EditValue.ToString());

                connect.Close();

                Console.WriteLine("Updated Products");
            }
            else if (flatComboBox1.Text == "News")
            {
                int ID;

                Console.WriteLine("Updating News");

                Connect connect = new Connect();

                try
                {
                    ID = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    Console.WriteLine("News ID: " + ID);
                }
                catch
                {
                    Console.WriteLine("News ID Conversion Failed: " + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id"));

                    return;
                }

                connect.UpdateNews(ID, e.Column.FieldName, gridView2.ActiveEditor.OldEditValue.ToString(), gridView2.ActiveEditor.EditValue.ToString());

                connect.Close();

                Console.WriteLine("Updated News");
            }
            else
            {
                Console.WriteLine("Unknown Table: " + flatComboBox1.Text);
            }
        }

        private void HandleUnhandledException(Exception ex)
        {
            if (AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe"))
                return;

            Console.WriteLine(ex.ToString());
        }

        private void gridView2_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            Console.WriteLine("Showing");

            gridView2Strip.Show(MousePosition);
        }

        private void gridView2Strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Console.WriteLine(e.ClickedItem.Text);

            if (e.ClickedItem.Text == "Refresh")
            {
                gridControl2.RefreshDataSource();
                gridControl2.Refresh();
            }

            if (e.ClickedItem.Text == "Owned Licenses")
            {
                object row = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Username");

                Console.WriteLine(row);
            }
        }

        #region unmanaged
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
                                               // Pinvoke
        private delegate bool ConsoleEventDelegate();
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void flatComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Console.WriteLine(flatComboBox1.Text);

            gridControl2.DataSource = null;

            if (flatComboBox1.Text == "Tokens")
            {
                lock (Tokens.AuthTokens)
                {
                    gridControl2.DataSource = Tokens.AuthTokens;
                }

                gridView2.PopulateColumns();

                foreach (GridColumn item in gridView2.Columns)
                {
                    if (item == gridView2.Columns["lastrequest"])
                    {
                        item.DisplayFormat.FormatType = FormatType.DateTime;
                        item.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    }
                }
            }

            gridControl2.Refresh();
        }

        private void flatClose1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Is this a Silent Update?", "Server Cache", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                CacheTokens();
            }
            else if (dialogResult == DialogResult.No)
            {
                //Immediate Exit - Make sure cache is clear
                try
                {
                    File.Delete(Environment.CurrentDirectory + "\\Cache\\Tokens.txt");
                }
                catch
                {

                }
            }
            else
            {
                //Cache Tokens and Assume Silent Update
                CacheTokens();
            }

            Environment.Exit(0);
        }

        private static void CacheTokens()
        {
            try
            {
                File.Delete(Environment.CurrentDirectory + "\\Cache\\Tokens.txt");
            }
            catch
            {

            }

            if (!Directory.Exists(Environment.CurrentDirectory + "\\Cache\\"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Cache\\");

            using (StreamWriter stream = File.AppendText(Environment.CurrentDirectory + "\\Cache\\Tokens.txt"))
            {
                //Run Cache - Cache all Token
                lock (Tokens.AuthTokens)
                {
                    foreach (Token token in Tokens.AuthTokens)
                    {
                        string IP = token.IP;
                        int ID = token.ID;
                        string Username = token.Username;
                        string AuthToken = token.AuthToken;
                        string LastDevice = token.LastDevice;
                        DateTime LastRequest = token.LastRequest;

                        stream.WriteLine(ID + "+" + IP + "+" + Username + "+" + AuthToken + "+" + LastDevice + "+" + LastRequest.ToString());
                    }
                }
            }
        }

        private static void LoadTokens()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\Cache\\Tokens.txt"))
                return;

            List<Token> tokens = new List<Token>();

            string Line;
            using (StreamReader stream = new StreamReader(Environment.CurrentDirectory + "\\Cache\\Tokens.txt"))
            {
                //Null Terminator and Empty String
                while ((Line = stream.ReadLine()) != null)
                {
                    string IP = Line.Split('+')[1];
                    int ID = Convert.ToInt32(Line.Split('+')[0]);
                    string Username = Line.Split('+')[2];
                    string AuthToken = Line.Split('+')[3];
                    string LastDevice = Line.Split('+')[4];

                    tokens.Add(new Token(IP, ID, Username, AuthToken, LastDevice));
                }
            }

            lock (Tokens.AuthTokens)
            {
                Tokens.AuthTokens = tokens;
            }
        }
    }
}