using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Chain.Instance.Blocks;
                dataGridView1.Update();
            };


            peerConnectDel += (port, uri) =>
            {
                PortView.Text = port;
                UriView.Text = uri;
                isPeerConected = true;
                Chain = new Chain(peerService);


                login = new Login();
                var res = login.ShowDialog();

                Chain.RequestChainInfo();

                UsernameView.Text = login.Username;
                ThreadListUpdate();

                Chain.OnBlocksListChange += ThreadListUpdate;

                Thread loadKostyl = new Thread(() => 
                {
                    Thread.Sleep(3000);
                    ThreadListUpdate();
                });

                loadKostyl.Start();
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

                if (!Chain.Blocks.Contains(block))
                {
                    Chain.AddBlock(block);
                    textBox1.Clear();
                }
            }
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
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Chain.Instance.Blocks;
            dataGridView1.Update();
            //Chain.RequestChainInfo();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenu m = new ContextMenu();

            var mousePos = dataGridView1.PointToClient(Cursor.Position);

            if (e.RowIndex < Chain.Blocks.Count)
            {
                MenuItem openFileItem = new MenuItem("Open block content");
                openFileItem.Click += (s, elent) =>
                {
                    Chain.Blocks[e.RowIndex].ViewContent();
                };
                m.MenuItems.Add(openFileItem);

                MenuItem downloadFileItem = new MenuItem("Download block content");
                downloadFileItem.Click += (s, elent) =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    DialogResult result = saveFileDialog.ShowDialog();

                    if (result != DialogResult.OK)
                        return;

                    Chain.Blocks[e.RowIndex].DownLoadContentTo(saveFileDialog.FileName);
                };
                m.MenuItems.Add(downloadFileItem);
            }

            m.Show(dataGridView1, mousePos);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                
                string path = openFileDialog1.FileName;
                try
                {
                    string content = path.ParseFileToBinary();
                    string extension = Path.GetExtension(path);

                    if(content == "")
                    {
                        MessageBox.Show("empty file");
                    }

                    User user = new User(login.Username, login.Password.GetHash(), UserRole.User);
                    Data data = new Data(content, extension);
                    Block block = new Block(user, data, Chain.LastBlock, BlockType.FILE);

                    if (!Chain.Blocks.Contains(block))
                    {
                        Chain.AddBlock(block);
                    }
                }
                catch (IOException )
                {
                    MessageBox.Show("Something went wrong while adding file");
                }
            }
        }
    }
}
