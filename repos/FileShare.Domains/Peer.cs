using System.Net.PeerToPeer;


namespace FileShare.Domains
{
    public class Peer<T>
    {
        public string Username { get; set; }
        public PeerName PeerName { get; set; }

        public T Channel { get; set; }
        public T Host { get; set; }
    }
}
