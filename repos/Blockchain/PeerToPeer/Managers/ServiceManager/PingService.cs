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
        public void SendBack(HostInfo sender, OperationType ot, string content = "", HostInfo reciver = null)
        {
            if (reciver?.Uri != CurrentHost.Instance.Info.Uri)
                return;

            Chain.Instance.Blocks = JsonConvert.DeserializeObject<List<Block>>(content);

            IPHostEntry host = Dns.GetHostEntry(sender.Uri);

            IPEndPointCollection iPEndPoints = new IPEndPointCollection();
            host.AddressList.ToList().ForEach(p =>
            { iPEndPoints.Add(new IPEndPoint(p, sender.Port)); });

            PeerEndPointInfo peerInfo = new PeerEndPointInfo
            {
                LastUpdate = DateTime.Now,
                PeerUri = sender.Uri,
                PeerIpCollection = iPEndPoints
            };
            PeerEndPointInformation?.Invoke(sender);
        }

        public void PingContent(HostInfo sender, OperationType ot, string content = "", HostInfo reciver = null)
        {
            switch (ot)
            {
                case OperationType.GetBlocks:
                {
                        if (CurrentHost.Instance?.Info.Uri == sender.Uri)
                            break;

                        string json = JsonConvert.SerializeObject(Chain.Instance.Blocks);

                        Chain.Instance.json = json;
                        Chain.Instance.reciver = sender;
                        Chain.Instance.sendreg = true;

                        //SendBack(CurrentHost.Instance.Info, OperationType.SendBlocks, json, sender);

                        break;
                }
                case OperationType.SendBlocks:
                {
                        if (reciver?.Uri != CurrentHost.Instance.Info.Uri)
                            break;

                        Chain.Instance.Blocks = JsonConvert.DeserializeObject<List<Block>>(content);

                        break;
                }
                case OperationType.SendBlock:
                {

                    break;
                }
                default:
                    {
                        break;
                    }
            }
            
            IPHostEntry h = Dns.GetHostEntry(sender.Uri);

            IPEndPointCollection iPEndPo = new IPEndPointCollection();
            h.AddressList.ToList().ForEach(p =>
            { iPEndPo.Add(new IPEndPoint(p, sender.Port)); });

            PeerEndPointInfo peerI = new PeerEndPointInfo
            {
                LastUpdate = DateTime.Now,
                PeerUri = sender.Uri,
                PeerIpCollection = iPEndPo
            };
            PeerEndPointInformation?.Invoke(sender);
        }

        public void SearchFiles(string searchTerm, string peerId)
        {
           /* ObservableCollection<FileMetaData> result =(ObservableCollection<FileMetaData>) (from file in AvailableFileMetaData
                          where searchTerm == file.Name
                            || file.Name.Contains(searchTerm)
                          select file);

            if(result.Any())
            {
                FileSerchResultModel serchResultModel = new FileSerchResultModel {
                    PeerId = peerId,
                    Files = result
                };
                FileSearchResult?.Invoke(serchResultModel);
            }*/
        }
    }
}
