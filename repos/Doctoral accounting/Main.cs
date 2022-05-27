using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using Blockchain;
using System.Threading;

namespace Doctoral_accounting
{
    public partial class Main : MaterialForm
    {
        public delegate void ConnectDelegate(String arr, String arr2);
        public delegate void UpdateDelegate();

        public List<History> histories = new List<History>();

        public ConnectDelegate peerConnectDel;
        public UpdateDelegate listUpdateDel;

        public Chain Chain;
        public PeerServiceHost peerService;
        public Login Login;

        private bool isPeerConected = false;

        public Main()
        {
            InitializeComponent();
            Login = new Login();

            listUpdateDel += () =>
            {
                histories = SearchDoctorHistories();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = histories;
                dataGridView1.Update();
            };


            peerConnectDel += (port, uri) =>
            {
                isPeerConected = true;
                Chain = new Chain(peerService);

                Hide();
                var res = Login.ShowDialog();
                Show();

                if (!Login.loginStatus)
                {
                    MessageBox.Show("You quit the login page");
                    Close();
                }

                Chain.RequestChainInfo();

                UsernameView.Text = Login.Username;
                ThreadListUpdate();

                Chain.OnBlocksListChange += ThreadListUpdate;

                Thread loadKostyl = new Thread(() =>
                {
                    Thread.Sleep(3000);
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

        private void ThreadConnect()
        {
            ThreadConnectionManager myThreadClassObject = new ThreadConnectionManager(this);
            myThreadClassObject.Run();
        }

        private void ThreadListUpdate()
        {
            ThreadConnectionManager myThreadClassObject = new ThreadConnectionManager(this);
            myThreadClassObject.ListUpdate();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                dataGridView1.Rows[e.RowIndex].Selected = true;

            ContextMenu m = new ContextMenu();
            var mousePos = dataGridView1.PointToClient(Cursor.Position);

            if (e.RowIndex >= 0 && e.RowIndex < Chain.Blocks.Count)
            {
                MenuItem openFileItem = new MenuItem("Open doctoral history");
                openFileItem.Click += (s, elent) =>
                {
                    var resForm = new SelectViewForm(histories[e.RowIndex]);
                    resForm.Show();

                };
                m.MenuItems.Add(openFileItem);
            }

            m.Show(dataGridView1, mousePos);
        }

        //search all user histories
        private List<History> SearchDoctorHistories()
        {
            List<History> resBlockList = new List<History>();
            foreach (Block b in Chain.Blocks)
            {
                if (b.User.Login == Login.Username)
                {
                    try 
                    {
                        History h = History.FromJson(b.Data.Content);
                        if (string.IsNullOrEmpty(h.PatientName))
                            continue;

                        resBlockList.Add(History.FromJson(b.Data.Content));
                    }
                    catch
                    {

                    }
                }
            }
            return resBlockList;
        }

        //save history
        private void button1_Click_2(object sender, EventArgs e)
        {
            if (isPeerConected)
            {
                History history = new History(patientText.Text, patientSurNameText.Text, DateTime.Now, dateTimePicker1.Value, diagnosesText.Text, comentaryText.Text, analizesText.Text, treatmentText.Text);
                histories.Add(history);

                Data data = new Data(history.ToJson());
                User user = new User(Login.Username, Login.Password.GetHash(), UserRole.User);
                Block block = new Block(user, data, Chain.LastBlock, BlockType.STR);

                if (!Chain.Blocks.Contains(block))
                {
                    Chain.AddBlock(block);
                    patientSurNameText.Clear();
                }
            }
        }

        /*
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

                    if (content == "")
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
                catch (IOException)
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
                if (b.Hash == search.Text)
                {
                    resBlockList.Add(b);
                }
            }
            ShowSearchResult(resBlockList);
            search.Clear();
        }



        private void ShowSearchResult(List<Block> blocks)
        {
            if (blocks.Count < 1)
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
        }*/
    }
}

