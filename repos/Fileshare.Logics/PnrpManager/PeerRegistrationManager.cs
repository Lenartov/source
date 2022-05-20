using System;
using System.Net.PeerToPeer;

namespace FileShare
{ 
    public class PeerRegistrationManager : IPeerRegistrationRepository
    {
        private PeerNameRegistration peerNameRegistration = null;

        public bool IsPeerRegistered => peerNameRegistration != null && peerNameRegistration.IsRegistered();

        public string PeerUri => peerNameRegistration?.PeerName.PeerHostName;

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
