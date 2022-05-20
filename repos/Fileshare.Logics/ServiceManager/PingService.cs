using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public enum OperationType
    {
        GetBlocks,
        SendBlocks,
        SendBlock
    }

    public delegate void OnPeerInfo(HostInfo endPointInfo);
    public delegate void FileSearchResult(FileSerchResultModel fileSerch);

    public class PingService : IPingService
    {
        public event OnPeerInfo OnPeerEndPointInfoShow;
        public event FileSearchResult FileSearchResult;

        private Random rnd;
        private int count = new FileSample().GetFileMetaDatas().Count;

        public HostInfo FileServiceHost { get; set; }
        public IList<FileMetaData> AvailableFileMetaData { get; set; }
        public ObservableCollection<HostInfo> ClientHostDetails { get; set; }


        public PingService()
        {
            rnd = new Random();

            ClientHostDetails = new ObservableCollection<HostInfo>();
            AvailableFileMetaData = new FileSample().GetFileMetaDatas().Take(rnd.Next(1, count)).ToList();
        }

        public PingService(HostInfo info)
        {
            FileServiceHost = info;
            ClientHostDetails = new ObservableCollection<HostInfo>();

            rnd = new Random();
            AvailableFileMetaData = new FileSample().GetFileMetaDatas().Take(rnd.Next(1, count)).ToList();
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
            OnPeerEndPointInfoShow?.Invoke(hostInfo);
        }

        public void PingContent2(HostInfo sender, OperationType ot, string content = "", HostInfo reciver = null)
        {

            if(OperationType.GetBlocks == ot)
            {
                if (sender.Uri == CurrentPeer.Instance.Uri)
                    return;

                CurrentPeer i = CurrentPeer.Instance;
                HostInfo me = new HostInfo
                {
                    Id = i.Id,
                    Port = i.Port,
                    Uri = i.Uri
                };

                string cont = " "; // fill content

                PingContent2(me, OperationType.SendBlocks, cont, sender);
            }
            else if (OperationType.SendBlocks == ot)
            {
                if (CurrentPeer.Instance.Uri != reciver?.Uri)
                    return;

                //set content 

            }
            else if(OperationType.SendBlock == ot)
            {
                if (CurrentPeer.Instance.Uri == sender?.Uri)
                    return;

                //set content
            }

        }

        public void PingContent(HostInfo hostInfo, string content)
        {

            if (CurrentPeer.Instance.Uri != hostInfo.Uri)
                Console.WriteLine(content);
        }

        public void SearchFiles(string searchTerm, string peerId)
        {
            ObservableCollection<FileMetaData> result =(ObservableCollection<FileMetaData>) (from file in AvailableFileMetaData
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
            }
        }
    }
}
