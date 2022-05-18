using System;
using System.Net.PeerToPeer;
using FileShare.Contracts.Repository;

namespace Fileshare.Logics.PnrpManager
{
    public class PeerRegistrationManager : IPeerRegistrationRepository
    {
        private PeerNameRegistration peerNameRegistration = null;

        public bool IsPeerRegistered => peerNameRegistration != null && peerNameRegistration.IsRegistered();

        public string PeerUrl => peerNameRegistration?.PeerName.PeerHostName;

        public PeerName PeerName { get; set; }

        public void StartPeerRegistration(string username, int port)
        {
            PeerName = new PeerName(username, PeerNameType.Unsecured);
            peerNameRegistration = new PeerNameRegistration(PeerName, port);
            peerNameRegistration.Start();
        }

        public void StopPeerRegistration()
        {
            peerNameRegistration?.Stop();
            peerNameRegistration = null;
        }
    }
}
