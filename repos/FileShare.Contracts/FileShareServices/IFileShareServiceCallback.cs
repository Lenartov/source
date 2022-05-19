using FileShare.Domains.FIleSearch;
using System.ServiceModel;

namespace FileShare.Contracts.FileShareServices
{
    public interface IFileShareServiceCallback
    {
        [OperationContract(IsOneWay = false)]
        bool isConnected(string replyMessage);

        [OperationContract(IsOneWay = false)]
        bool ForwardSearchResult(FileSerchResultModel serchResultModel);
    }
}
