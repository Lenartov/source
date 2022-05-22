using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare;

namespace Blockchain
{
    public class CurrentHost
    {
        private static CurrentHost instance;

        public HostInfo Info { get; set; }

        public static CurrentHost Instance
        {
            get
            {
                if (instance == null)
                    instance = new CurrentHost();

                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private CurrentHost() { }
    }
}
