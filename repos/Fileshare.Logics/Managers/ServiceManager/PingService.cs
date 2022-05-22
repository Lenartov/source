using FileShare.Contracts.Services;
using FileShare.Domains;
using FileShare.Domains.FIleSearch;
using FileShare.SampleData;
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
