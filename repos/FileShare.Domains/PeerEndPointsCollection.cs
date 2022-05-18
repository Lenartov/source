using System.Net;
using System.Net.PeerToPeer;

namespace FileShare.Domains
{
    public class PeerEndPointsCollection
    {
        public PeerEndPointsCollection(PeerName peer, IPEndPointCollection ipEndPoint)
        {
            PeerHostName = peer;
            PeerEndPoints = ipEndPoint;
        }

        public PeerName PeerHostName { get; private set; }
        public IPEndPointCollection PeerEndPoints { get; private set; }

    }

}
