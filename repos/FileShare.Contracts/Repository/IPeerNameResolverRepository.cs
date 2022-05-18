using FileShare.Domains;
using System.Net.PeerToPeer.Collaboration;

namespace FileShare.Contracts.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvPeerName(string peerId);
        PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
