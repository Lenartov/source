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
using Blockchain.Forms;
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
            InitializeComponent(); 
            login = new Login();

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

                Hide();
                var res = login.ShowDialog();
                Show();

                if(!login.loginStatus)
                {
                    MessageBox.Show("You quit the login page");
                    Close();
                }

                Chain.RequestChainInfo();

                UsernameView.Text = login.Username;
                ThreadListUpdate();

                Chain.OnBlocksListChange += ThreadListUpdate;

                Thread loadKostyl = new Thread(() => 
                {
                    Thread.Sleep(3000);
                    Chain.RequestChainInfo();
                    ThreadListUpdate();
                });

                loadKostyl.Start();
            };


            try
            {
                Thread PeeringThread = new Thread(new ThreadStart(ThreadConnect));
                PeeringThread.Start();
            }
            catch 
            {
                MessageBox.Show("Connection error");
                Close();
            }
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

            //right click
          /*  if (e.Button != MouseButtons.Right)
                return;*/

            dataGridView1.ClearSelection();
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                dataGridView1.Rows[e.RowIndex].Selected = true;

            ContextMenu m = new ContextMenu();
            var mousePos = dataGridView1.PointToClient(Cursor.Position);

            if (e.RowIndex >=0 && e.RowIndex < Chain.Blocks.Count)
            {
                MenuItem copyHash = new MenuItem("Copy hash");
                copyHash.Click += (s, elent) =>
                {
                    Chain.Blocks[e.RowIndex].CopyHashToBuffer();
                };
                m.MenuItems.Add(copyHash);

                MenuItem copyLogin = new MenuItem("Copy login");
                copyLogin.Click += (s, elent) =>
                {
                    Chain.Blocks[e.RowIndex].CopyLoginToBuffer();
                };
                m.MenuItems.Add(copyLogin);

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
                    else if(content.Length > 50000)
                    {
                        MessageBox.Show("size of file is too big");
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


        //search by block hash
        private void button3_Click(object sender, EventArgs e)
        {
            List<Block> resBlockList = new List<Block>();
            foreach (Block b in Chain.Blocks)
            {
                if(b.Hash == search.Text)
                {
                    resBlockList.Add(b);
                }
            }
            ShowSearchResult(resBlockList);
            search.Clear();
        }

        //search by username
        private void button5_Click(object sender, EventArgs e)
        {
            List<Block> resBlockList = new List<Block>();
            foreach (Block b in Chain.Blocks)
            {
                if (b.User.Login == search.Text)
                {
                    resBlockList.Add(b);
                }
            }
            ShowSearchResult(resBlockList);
            search.Clear();
        }

        private void ShowSearchResult(List<Block> blocks)
        {
            if(blocks.Count < 1)
            {
                MessageBox.Show("No results");
                return;
            }

            SearchResult searchResultForm = new SearchResult(blocks);
            searchResultForm.Show();
        }

        //copy username
        private void UsernameView_Clicked(object sender, EventArgs e)
        {
            ContextMenu m = new ContextMenu();
            Point mousePos = UsernameView.PointToClient(Cursor.Position);

            MenuItem copyHash = new MenuItem("Copy username");
            copyHash.Click += (s, elent) =>
            {
                Clipboard.SetText(login.Username);
            };
            m.MenuItems.Add(copyHash);

            m.Show(UsernameView, mousePos);
        }

        private void search_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            ContextMenu m = new ContextMenu();
            Point mousePos = UsernameView.PointToClient(Cursor.Position);

            MenuItem copyHash = new MenuItem("Past");
            copyHash.Click += (s, elent) =>
            {
                search.Text = Clipboard.GetText();
            };
            m.MenuItems.Add(copyHash);

            m.Show(UsernameView, mousePos);
        }
    }
}
