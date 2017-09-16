using AuthLib.Functions.Client;
using System;
using System.Windows.Forms;

namespace MaverickClient
{
    static class Program
    {
        public static Client client = new Client();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
