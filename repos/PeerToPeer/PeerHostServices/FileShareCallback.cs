using FileShare.Contracts.FileShareServices;
using FileShare.Domains.FIleSearch;
using System;

namespace PeerToPeer.PeerHostServices
{
    public class FileShareCallback : IFileShareServiceCallback
    {
        public bool ForwardSearchResult(FileSerchResultModel serchResultModel)
        {
            throw new NotImplementedException();
        }

        public bool isConnected(string replyMessage)
        {
            if(!string.IsNullOrEmpty(replyMessage))
            {
                Console.WriteLine(replyMessage);
                return true;
            }
            return false;
        }
    }
}
