using System.Net.PeerToPeer;


namespace FileShare
{
    public interface IPeerRegistrationRepository
    {
        bool IsPeerRegistered { get; }

        string PeerUri { get; }
        PeerName PeerName { get; set; }

        void StartPeerRegistration(string username, int port);
        void StopPeerRegistration();
    }
}
