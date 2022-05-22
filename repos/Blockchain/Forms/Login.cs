using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain
{
    public partial class Login : Form
    {
        public string Username { get; private set; }

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Username = textBox1.Text;

            if(CheckUsername())
            {
                if(CheckPassword())
                {
                    
                    Close();
                }
            }

        }

        public string GetUsername()
        {
            return Username;
        }

        private bool CheckUsername()
        {
            return true;
        }
        private bool CheckPassword()
        {
            return true;
        }
    }
}
