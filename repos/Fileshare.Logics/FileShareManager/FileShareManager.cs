using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public delegate void CurrentHostInfo(HostInfo info, bool isCallback = false);
    public delegate void CurrentClientInfo(string peerId, IFileShareServiceCallback callback);

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class FileShareManager : IFileShareService
    {
        private Dictionary<string, HostInfo> currentHosts = new Dictionary<string, HostInfo>();

        public event CurrentHostInfo CurrentHostUpDate;

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

        public void PingHostService(HostInfo info, bool isCallback)
        {
           // Console.WriteLine($"Peer: {info.Id}");
           // Console.WriteLine($"Server Info {info.Uri} {info.Port}");

            IFileShareServiceCallback callback = OperationContext.Current.GetCallbackChannel<IFileShareServiceCallback>();
            
            if(callback != null)
            {
                if(isCallback)
                {
                    if (callback.isConnected($"Ping back direct connection: {DateTime.UtcNow:T}")) ;
                    {
                        info.CallBack = callback;
                        CurrentHostUpDate?.Invoke(info, true);
                    }
                }
                else if(callback.isConnected($"Direct peer conection established from server at: {DateTime.UtcNow:D}"))
                {
                    currentHosts.Add(info.Id, info);
                    info.CallBack = callback;
                    CurrentHostUpDate?.Invoke(info);
                }
            }
        }
    }
}
