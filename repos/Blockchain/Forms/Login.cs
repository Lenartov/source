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
        public bool loginStatus = false;
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Username = textBox1.Text;
            Password = textBox2.Text;

            if(!CheckAuth(Username, Password))
            {
                return;
            }
            loginStatus = true;
            Close();
        }

        public string GetUsername()
        {
            return Username;
        }

        private bool CheckAuth(string login, string password)
        {
            foreach(User user in Chain.Instance.Users)
            {
                if (user.Login == login && user.Password == password.GetHash())
                    return true;
            }
            MessageBox.Show("Incorrect login or password");

            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Registration reg = new Registration();
            reg.ShowDialog();
            Show();
        }
    }
}
