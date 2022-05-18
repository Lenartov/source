using FileShare.Contracts.Services;
using FileShare.Domains;
using System;
using System.Net;
using System.Net.PeerToPeer;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Fileshare.Logics.ServiceManager
{
    public class PeerConfigurationService : IPeerConfigurationService
    {
        private ICommunicationObject Communication;
        private DuplexChannelFactory<IPingService> factory;
        private bool isServiceStarted;

        public PeerConfigurationService(Peer<IPingService> peer)
        {
            Peer = peer;
        }

        public int Port => FindFreePort();

        public Peer<IPingService> Peer { get; private set; }

        public bool StartPeerService()
        {
#pragma warning disable 618
            NetPeerTcpBinding binding = new NetPeerTcpBinding() { Security = { Mode = SecurityMode.None } };
#pragma warning restore 618
            ServiceEndpoint endPoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(IPingService)), binding, new EndpointAddress("net.p2p://FileShare"));
            
            Peer.Host = new PingService();
            factory = new DuplexChannelFactory<IPingService>(new InstanceContext(Peer.Host), endPoint);
            Peer.Channel = factory.CreateChannel();

            Communication = (ICommunicationObject)Peer.Channel;
            if(Communication != null)
            {
                Communication.Opened += CommunicationOnOpened;

                try
                {
                    Communication.Open();
                    if (isServiceStarted)
                        return isServiceStarted;
                }
                catch(PeerToPeerException)
                {
                    throw new PeerToPeerException("error establishing peer services");
                }
            }
            return isServiceStarted;
        }

        public bool StopPeerService()
        {
            if (Communication != null)
            {
                Communication.Close();
                Communication = null;
                factory = null;

                return true;
            }

            return false;
        }

        private void CommunicationOnOpened(object sender, EventArgs e)
        {
            isServiceStarted = true;
        }

        private int FindFreePort()
        {
            int port;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
            {
                socket.Bind(endPoint);
                IPEndPoint local = (IPEndPoint)socket.LocalEndPoint;
                port = local.Port;
            }

            if (port == 0)
            {
                throw new ArgumentNullException(nameof(port));
            }
            return port;
        }
    }
}
