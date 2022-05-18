using Fileshare.Logics.PnrpManager;
using Fileshare.Logics.ServiceManager;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domains;
using System;
using System.Diagnostics;


namespace FileShare.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            if(Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length <= 1)
            {
                Process.Start("FIleShare.Test.exe");
            }

            new Program().Run();
        }

        private void Run()
        {
            Peer<IPingService> peer = new Peer<IPingService>() { Username = Guid.NewGuid().ToString().Split('-')[4] };
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.Username);
            IPeerConfigurationService peerConfigurationService = new PeerConfigurationService(peer);

            peerRegistration.StartPeerRegistration(peer.Username, peerConfigurationService.Port);

            Console.WriteLine("Peer information");
            Console.WriteLine($"Peer Uri: {peerRegistration.PeerUrl} \n Port: {peerConfigurationService.Port}");
            Console.ReadLine();
        }
    }
}
