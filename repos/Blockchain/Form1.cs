using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain
{
    public partial class Form1 : Form
    {
        //private Servise Roma;

        private Chain chain;
        private User user;

        public Form1()
        {
            //Roma = new Servise();
//Roma.Abort();
            //Database.SetInitializer<BlockchainContext>(null);
            //Database.SetInitializer<UserContext>(null);

            user = new User("Penis", "qweqweqwe", UserRole.User);
            chain = new Chain();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            Block block = new Block(user, new Data(textBox1.Text, DataType.STR), chain.LastBlock);

            chain.AddBlock(block);
            button1.Text = chain.Blocks.Count().ToString();
            listBox1.Items.AddRange(chain.Blocks.ToArray());

            textBox1.Clear();
            textBox1.Select();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(chain.Blocks.ToArray());
        }
    }
}
