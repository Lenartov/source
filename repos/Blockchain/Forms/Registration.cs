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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            if (!CheckUsername(login) || !CheckPassword(password))
            {
                textBox1.Clear();
                textBox2.Clear();

                return;
            }

            User user = new User(login, password, UserRole.User);
            Chain.Instance.AddUser(user);

            Close();
        }

        private bool CheckForExistingLogin(string login)
        {
            foreach (User user in Chain.Instance.Users)
            {
                if (user.Login.Equals(login))
                {
                    MessageBox.Show("Chain already contain this login");
                    return false;
                }
            }

            return true;
        }

        private bool CheckUsername(string login)
        {
             List<string> invalidChars = new List<string>() { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-" };

             if (login.Length < 8)
             {
                 MessageBox.Show("Login length is less than 8");
                 return false;
             }

             if (!(!login.Equals(login.ToLower())))
             {
                 MessageBox.Show("Login have to contain at least 1 uppercase letter");

                 return false;
             }

             foreach (string s in invalidChars)
             {
                 if (login.Contains(s))
                 {
                     MessageBox.Show("Login contain invalid char");

                     return false;
                 }
             }
            
            return CheckForExistingLogin(login);

            return true;
        }

        private bool CheckPassword(string password)
        {
          /*  if (password.Length < 8)
            {
                MessageBox.Show("Password length is less than 8");
                return false;
            }

            if (!(!password.Equals(password.ToLower())))
            {
                MessageBox.Show("Password have to contain at least 1 uppercase letter");

                return false;
            }
          */
            return true;
        }
    }
}
