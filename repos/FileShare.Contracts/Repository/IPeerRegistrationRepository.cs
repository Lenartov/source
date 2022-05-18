using System.Net.PeerToPeer;


namespace FileShare.Contracts.Repository
{
    public interface IPeerRegistrationRepository
    {
        bool IsPeerRegistered { get; }

        string PeerUrl { get; }
        PeerName PeerName { get; set; }

        void StartPeerRegistration(string username, int port);
        void StopPeerRegistration();
    }
}
