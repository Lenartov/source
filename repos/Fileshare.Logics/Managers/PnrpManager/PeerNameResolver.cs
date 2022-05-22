using System;
using System.Linq;
using System.Net.PeerToPeer;
using System.Net.PeerToPeer.Collaboration;
using FileShare.Contracts.Repository;
using FileShare.Domains;

namespace Fileshare.Logics.PnrpManager
{
    public class PeerNameResolver : IPeerNameResolverRepository
    {
        private PeerEndPointCollection peers;
        private string username;

        public PeerNameResolver(string username)
        {
            this.username = username;
        }

        public PeerEndPointsCollection PeerEndPointCollection { get; set; }

        public void ResolvPeerName(string peerId)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            System.Net.PeerToPeer.PeerNameResolver resolver = new System.Net.PeerToPeer.PeerNameResolver();
            var result = resolver.Resolve(new PeerName(peerId, PeerNameType.Unsecured), Cloud.AllLinkLocal);
            
           /* if(result.Any())
            {
                PeerEndPointCollection = new PeerEndPointsCollection(result[0].PeerName, result[0].EndPointCollection);
            }*/
        }
    }
}
