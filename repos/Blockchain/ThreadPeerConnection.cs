using System;
using FileShare;

namespace Blockchain
{
    public class ThreadPeerConnection
    {
        Form1 mainForm;
        public ThreadPeerConnection(Form1 myForm)
        {
            mainForm = myForm;
        }

        public void Run()
        {
            Peer<IPingService> peer = new Peer<IPingService>()
            {
                Id = Guid.NewGuid().ToString().Split('-')[4],
                Username = mainForm.username
            };

            IPeerConfigurationService<PingService> peerConfigurationService = new PeerConfigurationService(peer);
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.Id);
            mainForm.peerService = new PeerServiceHost(peerRegistration, peerNameResolver, peerConfigurationService);

            mainForm.peerService.RunPeerServiceHost(peer, () =>
            {
                string port = mainForm.peerService.ConfigurPeer.Port.ToString();
                string uri = mainForm.peerService.RegistrPeer.PeerUri;

                mainForm.Invoke(mainForm.peerConnectDel, port, uri);
            });

        }
    }
}
