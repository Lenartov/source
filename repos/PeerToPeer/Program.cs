﻿using Fileshare.Logics.PnrpManager;
using Fileshare.Logics.ServiceManager;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domains;
using System;
using System.Diagnostics;
using System.Net;

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
            Peer<IPingService> peer = new Peer<IPingService>() { Username = Guid.NewGuid().ToString().Split('-')[4] };
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new Fileshare.Logics.PnrpManager.PeerNameResolver(peer.Username);
            IPeerConfigurationService peerConfigurationService = new PeerConfigurationService(peer);

            peerRegistration.StartPeerRegistration(peer.Username, peerConfigurationService.Port);

            Console.WriteLine($"Peer Uri: {peerRegistration.PeerUrl} \nPort: {peerConfigurationService.Port}");
            var host = Dns.GetHostEntry(peerRegistration.PeerUrl);

            foreach(var address in host.AddressList)
            {
                Console.WriteLine($"\t\t IP: {address}");
            }

            Console.ReadLine();
        }
    }
}
