using Fileshare.Logics.ServiceManager;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeerToPeer.PeerHostServices
{
    public class PeerServiceHost
    {
        private AutoResetEvent resetEvent = new AutoResetEvent(false);

        public IPeerRegistrationRepository RegistrPeer { get; set; }
        public IPeerNameResolverRepository ResolverPeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurPeer { get; set; }

        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfiguration)
        {
            RegistrPeer = peerRegistration;
            ResolverPeer = peerNameResolver;
            ConfigurPeer = peerConfiguration;
        }

        public void RunPeerServiceHost(Peer<IPingService> peer)
        {
            if (peer == null)
                throw new ArgumentNullException(nameof(peer));

            RegistrPeer.StartPeerRegistration(peer.Id, ConfigurPeer.Port);

            if(RegistrPeer.IsPeerRegistered)
            {
                Console.WriteLine($"Peer {peer.Username} registration complited");
                Console.WriteLine($"Uri: {RegistrPeer.PeerUri}\nPort: {ConfigurPeer.Port}");
            }

            if(ResolverPeer != null)
            {
                Console.WriteLine($"Resolving peer {peer.Username}");
                
                ResolverPeer.ResolvPeerName(peer.Id);
                var result = ResolverPeer.PeerEndPointCollection;

                Console.WriteLine($"\t\t EndPoints for {RegistrPeer.PeerUri}");
                
                if(ConfigurPeer.StartPeerService())
                {
                    Console.WriteLine("Peer services started");
                    peer.Channel.Ping(new HostInfo()
                    {
                        Id = peer.Id,
                        Port = ConfigurPeer.Port,
                        Uri = RegistrPeer.PeerUri
                    });

                    if(ConfigurPeer.PingService != null)
                    {
                        ConfigurPeer.PingService.PeerEndPointInformation += PingServiceOnPeerEndPointInformation;
                    }
                }
            }
        }

        public void PingServiceOnPeerEndPointInformation(HostInfo hostInfo)
        {
            if(hostInfo != null)
            {
                Console.WriteLine($"New peer EndPoint ******** {hostInfo.Uri}");
            }
        }
    }
}
