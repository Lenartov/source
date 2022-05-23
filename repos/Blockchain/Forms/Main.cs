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
    public delegate void UpdateDelegate();

    public partial class Form1 : Form
    {
        public ConnectDelegate peerConnectDel;
        public UpdateDelegate listUpdateDel;

        public Chain Chain;
        public PeerServiceHost peerService;
        public string username;

        private Login login;
        private bool isPeerConected = false;
        
        public Form1()
        {
            listUpdateDel += () =>
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(Chain.Blocks.ToArray());
            };


            peerConnectDel += (port, uri) =>
            {
                PortView.Text = port;
                UriView.Text = uri;
                isPeerConected = true;
                Chain = new Chain(peerService);

                login = new Login();
                var res = login.ShowDialog(); // ???

                Chain.RequestChainInfo();

                UsernameView.Text = login.Username;
                ListBoxUpdate();

                Chain.OnBlocksListChange += ListBoxUpdate;
            };
            InitializeComponent();

            Thread PeeringThread = new Thread(new ThreadStart(ThreadConnect));
            PeeringThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isPeerConected)
            {
                User user = new User(login.Username, login.Password.GetHash(), UserRole.User);
                Data data = new Data(textBox1.Text);
                Block block = new Block(user, data, Chain.LastBlock, BlockType.STR);

                Chain.AddBlock(block);
                textBox1.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ListBoxUpdate()
        {
            ThreadListUpdate();

        }

        private void ThreadConnect()
        {
            ThreadPeerConnection myThreadClassObject = new ThreadPeerConnection(this);
            myThreadClassObject.Run();
        }

        private void ThreadListUpdate()
        {
            ThreadPeerConnection myThreadClassObject = new ThreadPeerConnection(this);
            myThreadClassObject.ListUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Chain.RequestChainInfo();
        }
    }
}
