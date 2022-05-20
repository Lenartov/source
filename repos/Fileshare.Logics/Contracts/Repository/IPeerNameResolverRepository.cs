using System.Net.PeerToPeer.Collaboration;

namespace FileShare
{
    public interface IPeerNameResolverRepository
    {
        void ResolvPeerName(string peerId);
        PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
