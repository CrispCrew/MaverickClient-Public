using System;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Updater
{
    public partial class Download : Form
    {
        private BackgroundWorker _worker;

        //File Details
        NetworkStream networkStream;
        string path;
        long size;
        bool done;

        //File Stats
        long bytesReadTotal;

        #region Get/Set
        public bool Done
        {
            get
            {
                return done;
            }
        }
        #endregion

        public Download(NetworkStream networkStream, string path, long size)
        {
            this.networkStream = networkStream;
            this.path = path;
            this.size = size;

            Console.WriteLine(path + " -> " + size);

            InitializeComponent();
        }

        private void Download_Load(object sender, EventArgs e)
        {
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;

            _worker.RunWorkerCompleted += worker_WorkCompleted;
            _worker.DoWork += worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;

            _worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadFile(networkStream, path, size);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            flatProgressBar1.Value = e.ProgressPercentage;
            flatLabel1.Text = "Download: " + path;
            flatLabel2.Text = bytesReadTotal + "/" + size;
        }

        private void worker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Hide();
        }

        public void DownloadFile(NetworkStream networkStream, string path, long size)
        {
            byte[] buffer = new byte[4096];

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    bytesReadTotal = 0;

                    int bytesRead = 0;
                    while (size > bytesReadTotal && (bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                        bytesReadTotal += bytesRead;

                        Console.Write("\rNetwork Size: {0} Bytes Read: {1}", bytesRead, bytesReadTotal);

                        double pctComplete = ((double)bytesReadTotal / size) * 100;

                        Console.WriteLine("Completed: {0}%", pctComplete);

                        _worker.ReportProgress((int)pctComplete);
                    }

                    fileStream.Dispose();
                }

                Console.WriteLine("Downloaded");

                done = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(ex.ToString());

                done = false;
            }
        }
    }
}
