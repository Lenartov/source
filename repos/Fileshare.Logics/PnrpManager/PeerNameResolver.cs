using System;
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

        public void ResolvPeerName()
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            System.Net.PeerToPeer.PeerNameResolver resolver = new System.Net.PeerToPeer.PeerNameResolver();
            var result = resolver.Resolve(new PeerName(username, PeerNameType.Unsecured), Cloud.AllLinkLocal);
            
            // ???
            if(result.Count > 0)
            {
                PeerEndPointCollection = new PeerEndPointsCollection(result[0].PeerName, result[0].EndPointCollection);
            }
        }
    }
}
