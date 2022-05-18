using FileShare.Domains;
using System.Net.PeerToPeer.Collaboration;

namespace FileShare.Contracts.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvPeerName();
        PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
