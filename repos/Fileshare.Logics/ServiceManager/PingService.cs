using FileShare.Contracts.Services;
using FileShare.Domains;
using FileShare.Domains.FIleSearch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fileshare.Logics.ServiceManager
{
    public delegate void OnPeerInfo(HostInfo endPointInfo);
    public delegate void FileSearchResult(FileSerchResultModel fileSerch);

    public class PingService : IPingService
    {
        public event OnPeerInfo PeerEndPointInformation;
        public event FileSearchResult FileSearchResult;

        public HostInfo FileServiceHost { get; set; }
        public ObservableCollection<FileMetaData> AvailableFileMetaData { get; set; }
        public ObservableCollection<HostInfo> ClientHostDetails { get; set; }


        public PingService()
        {
            ClientHostDetails = new ObservableCollection<HostInfo>();
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
            host.AddressList.ToList().ForEach(p => { iPEndPoints.Add(new IPEndPoint(p, hostInfo.Port)); });

            PeerEndPointInfo peerInfo = new PeerEndPointInfo { LastUpdate = DateTime.Now, PeerUri = hostInfo.Uri, PeerIpCollection = iPEndPoints };
            ClientHostDetails.Add(hostInfo);
            PeerEndPointInformation?.Invoke(hostInfo);
        }

        public void SearchFiles(string searchTerm, string peerId)
        {
            if(ClientHostDetails.Any())
            {
                HostInfo info = ClientHostDetails.First(p => p.Id == peerId);
                ObservableCollection<FileMetaData> result = (ObservableCollection<FileMetaData>) from fileMeta in AvailableFileMetaData where searchTerm == fileMeta.Name select fileMeta;
                
                if(info != null)
                {
                    if(result.Any())
                    {
                        FileSerchResultModel fileSerch = new FileSerchResultModel()
                        {
                            ServiceHost = FileServiceHost,
                            Files = result
                        };
                    }
                }
            }
        }
    }
}
