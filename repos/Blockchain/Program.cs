using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain
{
    public class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

              if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length <= 1)
              {
                  var DBCS = ConfigurationManager.ConnectionStrings[1];
                  var writable = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                  writable.SetValue(DBCS, false);
                  DBCS.ConnectionString = "data source=(localdb)\\MSSQLLocalDB;Initial Catalog=store2;Integrated Security=True;";

                  Process.Start("Blockchain.exe");
              }

            new Program().Run();
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}
