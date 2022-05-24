using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Blockchain;
using Newtonsoft.Json;

namespace FileShare
{
    public delegate void OnPeerInfo(HostInfo endPointInfo);
    public delegate void FileSearchResult(FileSerchResultModel fileSerch);

    public class PingService : IPingService
    {
        public event OnPeerInfo PeerEndPointInformation;
        //public event FileSearchResult FileSearchResult;

        private Random rnd;
       // private int count = new FileSample().GetFileMetaDatas().Count;

        public HostInfo FileServiceHost { get; set; }
        public IList<FileMetaData> AvailableFileMetaData { get; set; }
        public ObservableCollection<HostInfo> ClientHostDetails { get; set; }


        public PingService()
        {
            rnd = new Random();

            ClientHostDetails = new ObservableCollection<HostInfo>();
           // AvailableFileMetaData = new FileSample().GetFileMetaDatas().Take(rnd.Next(1, count)).ToList();
        }

        public PingService(HostInfo info)
        {
            FileServiceHost = info;
            ClientHostDetails = new ObservableCollection<HostInfo>();

        }

        public void Ping(HostInfo hostInfo)
        {
            IPHostEntry host = Dns.GetHostEntry(hostInfo.Uri);

            IPEndPointCollection iPEndPoints = new IPEndPointCollection();
            host.AddressList.ToList().ForEach(p => 
            { iPEndPoints.Add(new IPEndPoint(p, hostInfo.Port)); });

            PeerEndPointInfo peerInfo = new PeerEndPointInfo 
            { 
                LastUpdate = DateTime.Now,
                PeerUri = hostInfo.Uri,
                PeerIpCollection = iPEndPoints 
            };
            //ClientHostDetails.Add(hostInfo);
            PeerEndPointInformation?.Invoke(hostInfo);
        }
        
        public void SearchFiles(string searchTerm, string peerId)
        {
        }

        public void RequestBlocks(HostInfo sender, HostInfo reciver)
        {
            if (CurrentHost.Instance.Info.Uri != reciver.Uri)
                return;

            Chain.Instance.SendBlocks(sender);
        }

        public void SendBlocks(HostInfo sender, HostInfo reciver, Block[] blocks)
        {
            if (CurrentHost.Instance.Info.Uri != reciver.Uri)
                return;

            SynchronizedCollection<Block> blockList = new SynchronizedCollection<Block>();

            foreach (var b in blocks)
                blockList.Add(b);

            if (Chain.Instance.CompareBlocks(blockList))
            {
                Chain.Instance.SetBlocksFromGlobal(blockList);
            }
        }

        public void RequestChainInfo(HostInfo sender)
        {
            if (CurrentHost.Instance.Info.Uri == sender.Uri)
                return;

            Chain.Instance.SendChainInfo(sender);
        }

        public void SendChainInfo(HostInfo sender, HostInfo reciver, int chainLength, bool chainStatus)
        {
            if (CurrentHost.Instance.Info.Uri != reciver.Uri)
                return;

            if (!chainStatus)
                return;

            if (Chain.Instance.Blocks.Count() < chainLength)
                Chain.Instance.RequestBlocks(sender);
        }

        public void SendBlock(HostInfo sender, Block block)
        {
            if (CurrentHost.Instance.Info.Uri == sender.Uri)
                return;

            if(!Chain.Instance.Blocks.Contains(block))
                Chain.Instance.AddBlock(block);
        }
    }
}
