using Fileshare.Logics.PnrpManager;
using Fileshare.Logics.ServiceManager;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domains;
using PeerToPeer.PeerHostServices;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace PeerToPeer
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length <= 1)
            {
                Process.Start("PeerToPeer.exe");
            }
            
            new Program().Run();
        }

        private void Run()
        {
            Console.WriteLine("Enter name");
            string username = Console.ReadLine();
            Console.Clear();

            Peer<IPingService> peer = new Peer<IPingService>() 
            {
                Id = Guid.NewGuid().ToString().Split('-')[4],
                Username = username
            };

            IPeerConfigurationService<PingService> peerConfigurationService = new PeerConfigurationService(peer);
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.Id);

            PeerServiceHost peerHostServices = new PeerServiceHost(peerRegistration, peerNameResolver, peerConfigurationService);
            Thread thread = new Thread(() => 
            {
                peerHostServices.RunPeerServiceHost(peer);
            }) { IsBackground = true };
            thread.Start();

            Console.ReadLine();
        }
    }
}
