using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public class CurrentPeer
    {
        private static CurrentPeer instance = null;
        public static CurrentPeer Instance { 
            get 
            {
                if(instance == null)
                {
                    instance = new CurrentPeer();
                }
                return instance;
            }
        }


        public string Uri { get; set; }
        public int Port { get; set; }
        public string Id { get; set; }
    }
}
