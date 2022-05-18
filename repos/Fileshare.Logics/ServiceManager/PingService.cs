using FileShare.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fileshare.Logics.ServiceManager
{
    public class PingService : IPingService
    {
        public void Ping(int port, string peerUri)
        {
            Console.WriteLine($"Msg from {peerUri}");
        }
    }
}
