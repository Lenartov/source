using System;
using System.Net;

namespace FileShare.Domains
{
    public class PeerEndPointInfo
    {
        public string PeerUri { get; set; }
        public IPEndPointCollection PeerIpCollection { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
