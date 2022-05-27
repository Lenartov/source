using Blockchain;
using FileShare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctoral_accounting
{
    class ThreadConnectionManager
    {
        private Main mainForm;
        public ThreadConnectionManager(Main myForm)
        {
            mainForm = myForm;
        }

        public void Run()
        {
            Peer<IPingService> peer = new Peer<IPingService>()
            {
                Id = Guid.NewGuid().ToString().Split('-')[4],
                Username = mainForm.Login.Username
            };

            IPeerConfigurationService<PingService> peerConfigurationService = new PeerConfigurationService(peer);
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.Id);
            mainForm.peerService = new PeerServiceHost(peerRegistration, peerNameResolver, peerConfigurationService);

            mainForm.peerService.RunPeerServiceHost(peer, () =>
            {
                string port = mainForm.peerService.ConfigurPeer.Port.ToString();
                string uri = mainForm.peerService.RegistrPeer.PeerUri;

                mainForm?.Invoke(mainForm.peerConnectDel, port, uri);
            });

        }

        public void ListUpdate()
        {
            try
            {
                mainForm?.Invoke(mainForm.listUpdateDel);
            }
            catch
            {
            }
        }
    }
}
