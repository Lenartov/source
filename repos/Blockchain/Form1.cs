using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileShare;

namespace Blockchain
{
    public delegate void ConnectDelegate(String arr, String arr2);
    public partial class Form1 : Form
    {
        public ConnectDelegate peerConnectDel;

        public Chain Chain;
        public PeerServiceHost peerService;
        public string username;

        private Login login;
        private bool isPeerConected = false;
        
        public Form1()
        {
            peerConnectDel += (port, uri) =>
            {
                PortView.Text = port;
                UriView.Text = uri;
                isPeerConected = true;
                Chain = new Chain(peerService);
                listBox1.Items.AddRange(Chain.Blocks.ToArray());
            };

            InitializeComponent();

            login = new Login();
            var res = login.ShowDialog();


            UsernameView.Text = login.Username;

            Thread PeeringThread = new Thread(new ThreadStart(ThreadConnect));
            PeeringThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isPeerConected)
            {
                Chain.Ping();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ThreadConnect()
        {
            ThreadPeerConnection myThreadClassObject = new ThreadPeerConnection(this);
            myThreadClassObject.Run();
        }
    }
}
