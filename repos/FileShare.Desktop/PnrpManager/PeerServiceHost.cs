using Fileshare.Logics.ServiceManager;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileShare.Desktop.PnrpManager
{
    public class PeerServiceHost
    {
        public IPeerRegistrationRepository RegistrPeer { get; set; }
        public IPeerNameResolverRepository ResolverPeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurPeer { get; set; }

        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfiguration)
        {
            RegistrPeer = peerRegistration;
            ResolverPeer = peerNameResolver;
            ConfigurPeer = peerConfiguration;
        }

        public void RunPeerServices()
        {
            if(ConfigurPeer.Peer == null)
                return;

            
        }
    }
}
