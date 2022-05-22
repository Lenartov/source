using System.Collections.ObjectModel;
using System.Net;
using System.Net.PeerToPeer;

namespace FileShare.Domains
{
    public class PeerEndPointsCollection
    {
        public PeerName PeerHostName { get; private set; }
        public ObservableCollection<PeerEndPointInfo> PeerEndPoints { get; set; }

        public PeerEndPointsCollection(PeerName peer)
        {
            PeerHostName = peer;
            PeerEndPoints = new ObservableCollection<PeerEndPointInfo>();
        }

    }
}
