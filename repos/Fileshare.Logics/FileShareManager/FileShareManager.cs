using FileShare.Contracts.FileShareServices;
using FileShare.Domains;
using FileShare.Domains.FIleSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Fileshare.Logics.FileShareManager
{
    public delegate void CurrentHostInfo(HostInfo info);

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class FileShareManager : IFileShareService
    {
        private Dictionary<string, HostInfo> currentHost = new Dictionary<string, HostInfo>();

        public event CurrentHostInfo CurrentHostUpdate;

        public void ForwardResult(FileSerchResultModel resultModel)
        {
            throw new NotImplementedException();
        }

        public FilePartModel GetAllFileByte(FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public void PingHostService(HostInfo info)
        {
            Console.WriteLine($"Peer: {info.Id}   Server: {info.Uri}:{info.Port}\n");
            IFileShareServiceCallback callback = OperationContext.Current.GetCallbackChannel<IFileShareServiceCallback>();
            
            if(callback != null)
            {
                if(callback.isConnected($"Message from server at: {DateTime.UtcNow:D}"))
                {
                    currentHost.Add(info.Id, info);
                    CurrentHostUpdate?.Invoke(info);
                }
            }
        }
    }
}
