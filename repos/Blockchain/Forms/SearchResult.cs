using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain.Forms
{
    public partial class SearchResult : Form
    {
        private List<Block> Blocks;

        public SearchResult(List<Block> blocks)
        {
            InitializeComponent();
            Blocks = blocks;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = blocks;
            dataGridView1.Update();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //right click
           // if (e.Button != MouseButtons.Right)
            //    return;

            ContextMenu m = new ContextMenu();

            var mousePos = dataGridView1.PointToClient(Cursor.Position);

            if (e.RowIndex >= 0 && e.RowIndex < Blocks.Count)
            {
                MenuItem copyHash = new MenuItem("Copy hash");
                copyHash.Click += (s, elent) =>
                {
                    Blocks[e.RowIndex].CopyHashToBuffer();
                };
                m.MenuItems.Add(copyHash);

                MenuItem copyLogin = new MenuItem("Copy login");
                copyLogin.Click += (s, elent) =>
                {
                    Blocks[e.RowIndex].CopyLoginToBuffer();
                };
                m.MenuItems.Add(copyLogin);

                MenuItem openFileItem = new MenuItem("Open block content");
                openFileItem.Click += (s, elent) =>
                {
                    Blocks[e.RowIndex].ViewContent();
                };
                m.MenuItems.Add(openFileItem);

                MenuItem downloadFileItem = new MenuItem("Download block content");
                downloadFileItem.Click += (s, elent) =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    DialogResult result = saveFileDialog.ShowDialog();

                    if (result != DialogResult.OK)
                        return;

                    Blocks[e.RowIndex].DownLoadContentTo(saveFileDialog.FileName);
                };
                m.MenuItems.Add(downloadFileItem);


            }

            m.Show(dataGridView1, mousePos);
        }
    }
}
